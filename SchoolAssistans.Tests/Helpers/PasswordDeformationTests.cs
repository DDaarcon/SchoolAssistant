using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using SchoolAssistant.Logic.General.Other;
using SchoolAssistant.Logic.General.Other.Help;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.Helpers
{
    public class PasswordDeformationTests
    {
        private readonly ISession _session = new MockedSession();
        private IHttpContextAccessor _ctxAcc = null!;
        private ITextCryptographicService _deformationSvc;

        private string _id = 31232.ToString();

        public PasswordDeformationTests()
        {

            var ctxAccMock = new Mock<IHttpContextAccessor>();
            var ctxMock = new Mock<HttpContext>();

            ctxMock.Setup(x => x.Session).Returns(_session);
            ctxAccMock.Setup(x => x.HttpContext).Returns(ctxMock.Object);
            _ctxAcc = ctxAccMock.Object;
        }

        [Test]
        public void Should_deform_and_parse()
        {
            Assert.Pass();
        }

        [Test]
        public async Task ShouldEncrypt()
        {
            _deformationSvc = new TextCryptographicService(_ctxAcc, new CryptographicTools());
            string text = "Ala_ma_kota_kota_malego21412";

            (bool savedIsSession, string? deformed) = await _deformationSvc.GetEncryptedAsync(text, _id);

            Assert.IsTrue(savedIsSession);

            _deformationSvc = new TextCryptographicService(_ctxAcc, new CryptographicTools());

            string? readable = await _deformationSvc.GetDecryptedAsync(deformed, _id);

            Assert.AreEqual(text, readable);
        }
    }
}
