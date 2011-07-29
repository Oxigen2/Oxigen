using System;
using System.Net;
using NUnit.Framework;
using OxigenIIAdvertising.ContentExchanger;

namespace Oxigen.Tests32
{
    [TestFixture]
    public class EmptyRequestMakerTests
    {
        [Test]
        [ExpectedException(typeof(WebException))]
        public void ThrowsWebExceptionWithGET()
        {
            EmptyRequestMaker requestMaker = new EmptyRequestMaker();
            requestMaker.MakeRequest("http://notavalidurl", HttpMethod.GET);
        }

        [Test]
        [ExpectedException(typeof(WebException))]
        public void ThrowsWebExceptionWithPOST()
        {
            EmptyRequestMaker requestMaker = new EmptyRequestMaker();
            requestMaker.MakeRequest("http://notavalidurl", HttpMethod.POST);
        }
    }
}
