using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Net;
using GHIElectronics.NETMF.Net.NetworkInformation;
using GHIElectronics.NETMF.Net.Sockets;
using System.Text;
using Socket = GHIElectronics.NETMF.Net.Sockets.Socket;
using TechCorp.Supervisory.ControlFEZPanda;
using TechCorp.Supervisory.ControlFEZPanda.Component;

namespace TechCorp.Supervisory.ClientTCP
{
    public class Program
    {
        public static void Main()
        {


            const Int32 c_port = 2000;
            byte[] ip = { 192, 168, 1, 100 };
            byte[] subnet = { 255, 255, 255, 0 };
            byte[] gateway = { 192, 168, 1, 1 };
            byte[] mac = { 0x00, 0x26, 0x1C, 0x7B, 0x29, 0xE8 };
            WIZnet_W5100.Enable(SPI.SPI_module.SPI1, (Cpu.Pin)FEZ_Pin.Digital.Di10,
            (Cpu.Pin)FEZ_Pin.Digital.Di7, true);
            NetworkInterface.EnableStaticIP(ip, subnet, gateway, mac);
            NetworkInterface.EnableStaticDns(new byte[] { 192, 168, 1, 1 });
            Socket server = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, c_port);
            server.Bind(localEndPoint);
            server.Listen(1);
            while (true)
            {
                // Wait for a client to connect.
                Socket clientSocket = server.Accept();
                // Process the client request. true means asynchronous.
                new ProcessClientRequest(clientSocket, true);
            }
        }

    }

    /// <summary>
    /// Processes a client request.
    /// </summary>
    internal sealed class ProcessClientRequest
    {
        private Socket m_clientSocket;
        /// <summary>
        /// The constructor calls another method to handle the request, but can
        /// optionally do so in a new thread.
        /// </summary>
        /// /// <param name="clientSocket"></param>
        /// <param name="asynchronously"></param>
        public ProcessClientRequest(Socket clientSocket, Boolean asynchronously)
        {
            m_clientSocket = clientSocket;
            if (asynchronously)
                // Spawn a new thread to handle the request.
                new Thread(ProcessRequest).Start();
            else ProcessRequest();
        }
        /// <summary>
        /// Processes the request.
        /// </summary>
        private void ProcessRequest()
        {
            try
            {
                const Int32 c_microsecondsPerSecond = 1000000;
                // 'using' ensures that the client's socket gets closed.
                using (m_clientSocket)
                {
                    // Wait for the client request to start to arrive.
                    Byte[] buffer = new Byte[1024];
                    if (m_clientSocket.Poll(5 * c_microsecondsPerSecond,
                    SelectMode.SelectRead))
                    {
                        // If 0 bytes in buffer, then the connection has been closed,
                        // reset, or terminated.
                        if (m_clientSocket.Available == 0)
                            return;
                        // Read the first chunk of the request (we don't actually do
                        // anything with it).
                        Int32 bytesRead = m_clientSocket.Receive(buffer,
                        m_clientSocket.Available, SocketFlags.None);
                        // Return a static HTML document to the client.

                        var comandoRecebido = new string(Encoding.UTF8.GetChars(buffer));
                        Debug.Print("Receive data from Server: " + comandoRecebido);
                        string ret = PressButton(comandoRecebido);
                        //String s = "Comando realizado com Sucesso";

                        byte[] buf = Encoding.UTF8.GetBytes(ret);
                        int result = m_clientSocket.Send(buf);
                        Debug.Print("Send data from Server: " + ret);
                        m_clientSocket.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public string PressButton(string command)
        {
            var commands = Utility.UnmountCommand.Unmount(command);
            string typeEquipament = commands[0];
            int part = Convert.ToInt32(commands[2]);

            string result = string.Empty;

            switch (typeEquipament)
            {
                case MicroFrameworkCommands.TypesClass.Rele:
                  result =  PressButtonRele(commands[1], part);
                    break;

                case MicroFrameworkCommands.TypesClass.Switch:
                    result = PressButtonSwitch(commands[1], part);
                    break;

                default:
                    result = "PressButton not found.";
                    break;
            }
            return result;
        }

        private string PressButtonRele(string command, int part)
        {
           
            Controller controller = new Controller();
            Rele rele = new Rele();

            ICommand typeCommand = null;

            if (command == "ON")
            {
                //typeCommand how releOn
                typeCommand = new ReleOnCommand(rele);
                controller.setCommand(typeCommand);
            }
            else
            {
                //typeCommand how releOff
                typeCommand = new ReleOffCommand(rele);
                controller.setCommand(typeCommand);
            }
           return controller.SendCommand(part);
        }

        private string PressButtonSwitch(string command, int part)
        {
            Controller controller = new Controller();
            Switch switchClass = new Switch();

            ICommand typeCommand = new SwitchGetStateCommand(switchClass);
            controller.setCommand(typeCommand);
            return controller.SendCommand(part);
        }
    }
}
