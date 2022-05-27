using NUnit.Framework;
using SchoolAssistant.Logic.General.Other;
using System;
using System.Linq;

namespace SchoolAssistans.Tests.Helpers
{
    public class PasswordDeformationTests
    {
        private readonly IPasswordDeformationService _deformationSvc;

        public PasswordDeformationTests()
        {
            _deformationSvc = new PasswordDeformationService(null);
        }


        [Test]
        public void Should_deform_and_parse()
        {
            string text = "Ala_ma_kota_kota_malego21412";

            string deformed = _deformationSvc.GetDeformed(text);

            Assert.IsTrue(deformed.All(x => _deformationSvc.AllowedChars.Contains(x)));

            string readable = _deformationSvc.GetReadable(deformed);

            Assert.AreEqual(text, readable);
        }
    }
}
