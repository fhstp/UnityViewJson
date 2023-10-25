#nullable enable

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static At.Ac.FhStp.ViewJson.Utilities;

namespace At.Ac.FhStp.ViewJson
{
    [TestFixture]
    [RequiresPlayMode]
    internal class ViewJsonTests
    {
        private RectTransform transform = null!;


        [SetUp]
        public void Setup()
        {
            transform = MakeTestTransform();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(transform.gameObject);
        }


        [Test]
        [TestCase("")]
        [TestCase("wow")] // Missing quotes 
        [TestCase("{a; 123}")] // Invalid syntax in object
        public void Viewing_Invalid_Json_Results_In_InvalidJson_Code(string json)
        {
            var options = new ViewJsonOptions(
                MockStyle.MakeDefault(),
                MockSchema.MakeDefault());

            var code = ViewJson.TryViewJsonIn(transform, json, options);

            Assert.That(code, Is.EqualTo(ViewJsonResultCode.InvalidJson));
        }

        [Test]
        [TestCase("{}")]
        [TestCase("\"wow\"")]
        [TestCase("[123]")]
        [TestCase("{\"x\":123}")]
        public void Viewing_Valid_Json_Does_Not_Result_In_InvalidJson_Code(string json)
        {
            var options = new ViewJsonOptions(
                MockStyle.MakeDefault(),
                MockSchema.MakeDefault());

            var code = ViewJson.TryViewJsonIn(transform, json, options);

            Assert.That(code, Is.Not.EqualTo(ViewJsonResultCode.InvalidJson));
        }
    }
}