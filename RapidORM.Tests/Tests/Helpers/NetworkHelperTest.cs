using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RapidORM.Helpers;

namespace RapidORM.Tests.Tests.Helpers
{
    [TestClass]
    public class NetworkHelperTest
    {
        [TestMethod]
        public void DownloadFileFromFtpTest()
        {
            var result = NetworkHelper.DownloadFileFromFtp("ftp://{domain}:{port}/public/images/profile/sample.jpg",
                            @"{local_pc_location}/sample.jpg", "{ftp_username}", "ftp_password");

            Assert.AreEqual("test", result);
        }
    }
}
