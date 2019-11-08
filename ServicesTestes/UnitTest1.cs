using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASAS.Services.Deployment;
using ASAS.Domain.Services;

namespace ServicesTestes
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            DateTime inicio = DateTime.Now;

            for (int i = 0; i < 140; i++)
            {
                IServiceManagerRele serviceRele = new ServiceManagerRele();

                serviceRele.CreateReportRele();
            }

            DateTime fim = DateTime.Now;
        }
    }
}
