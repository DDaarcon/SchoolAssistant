using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SchoolAssistant.Logic.General.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string text = "Ala ma kota, kota małego";

            string deformed = _deformationSvc.GetDeformed(text);
            string readable = _deformationSvc.GetReadable(deformed);

            Assert.AreEqual(text, readable);
        }
    }
}
