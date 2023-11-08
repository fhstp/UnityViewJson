#nullable enable

using Newtonsoft.Json;
using NUnit.Framework;
using TMPro;
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
                DataFormat.Default);

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
                DataFormat.Default);

            var code = ViewJson.TryViewJsonIn(transform, json, options);

            Assert.That(code, Is.Not.EqualTo(ViewJsonResultCode.InvalidJson));
        }

        [Test]
        [TestCase("123", "123")]
        [TestCase("1.23", "1.23")]
        [TestCase("\"hello\"", "hello")]
        [TestCase("true", "True")]
        [TestCase("false", "False")]
        [TestCase("null", "null")]
        public void Primitives_Are_Converted_To_Texts(string json, string content)
        {
            var options = new ViewJsonOptions(
                MockStyle.MakeDefault(),
                DataFormat.Default);

            var code = ViewJson.TryViewJsonIn(transform, json, options);

            Assert.That(code, Is.EqualTo(ViewJsonResultCode.Ok), "Should convert ok");

            Assert.That(transform.childCount, Is.EqualTo(1), "Incorrect child count");
            var child = transform.GetChild(0)!;

            var text = child.GetComponent<TMP_Text>();
            Assert.That(text, Is.Not.Null, "Child should have text");

            Assert.That(text.text, Is.EqualTo(content), "Content should match");
        }
    }
}