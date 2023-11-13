#nullable enable

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
                DataStyle.DefaultLight,
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
                DataStyle.DefaultLight,
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
                DataStyle.DefaultLight,
                DataFormat.Default);

            var code = ViewJson.TryViewJsonIn(transform, json, options);

            Assert.That(code, Is.EqualTo(ViewJsonResultCode.Ok), "Should convert ok");

            Assert.That(transform.childCount, Is.EqualTo(1), "Incorrect child count");
            var child = transform.GetChild(0)!;

            var text = child.GetComponent<TMP_Text>();
            Assert.That(text, Is.Not.Null, "Child should have text");

            Assert.That(text.text, Is.EqualTo(content), "Content should match");
        }

        [Test]
        [TestCase("123")]
        [TestCase("1.23")]
        [TestCase("\"hello\"")]
        [TestCase("true")]
        [TestCase("null")]
        public void Text_Transform_Fills_Container(string json)
        {
            var options = new ViewJsonOptions(
                DataStyle.DefaultLight,
                DataFormat.DefaultText);

            var code = ViewJson.TryViewJsonIn(transform, json, options);
            Assert.That(code, Is.EqualTo(ViewJsonResultCode.Ok), "Should convert ok");

            var child = (transform.GetChild(0) as RectTransform)!;
            Assert.That(child, Is.Not.Null, "Child should exist");

            Assert.That(child.anchorMin, Is.EqualTo(Vector2.zero));
            Assert.That(child.anchorMax, Is.EqualTo(Vector2.one));
            Assert.That(child.sizeDelta, Is.EqualTo(Vector2.zero));
            Assert.That(child.offsetMin, Is.EqualTo(Vector2.zero));
            Assert.That(child.offsetMax, Is.EqualTo(Vector2.zero));
            Assert.That(child.localScale, Is.EqualTo(Vector3.one));
        }

        [Test]
        [TestCase("123")]
        [TestCase("1.23")]
        [TestCase("\"hello\"")]
        [TestCase("true")]
        [TestCase("null")]
        public void Text_Has_Correct_Color(string json)
        {
            var style = DataStyle.DefaultLight;
            var color = style.TextColor;
            var options = new ViewJsonOptions(
                style,
                DataFormat.DefaultText);

            var code = ViewJson.TryViewJsonIn(transform, json, options);
            Assert.That(code, Is.EqualTo(ViewJsonResultCode.Ok), "Should convert ok");

            var text = transform.GetChild(0)?.GetComponent<TMP_Text>()!;
            Assert.That(text, Is.Not.Null, "Child should exist");

            Assert.That(text.color, Is.EqualTo(color));
        }

        [Test]
        [TestCase(TextAlignment.Start, HorizontalAlignmentOptions.Left)]
        [TestCase(TextAlignment.Center, HorizontalAlignmentOptions.Center)]
        [TestCase(TextAlignment.End, HorizontalAlignmentOptions.Right)]
        public void Text_Has_Correct_Horizontal_Alignment(TextAlignment alignment, HorizontalAlignmentOptions expected)
        {
            var options = new ViewJsonOptions(
                DataStyle.DefaultLight,
                DataFormat.DefaultText.WithHorizontalTextAlignment(alignment));

            var code = ViewJson.TryViewJsonIn(transform, "123", options);
            Assert.That(code, Is.EqualTo(ViewJsonResultCode.Ok), "Should convert ok");

            var text = transform.GetChild(0)?.GetComponent<TMP_Text>()!;
            Assert.That(text, Is.Not.Null, "Child should exist");

            Assert.That(text.horizontalAlignment, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(TextAlignment.Start, VerticalAlignmentOptions.Top)]
        [TestCase(TextAlignment.Center, VerticalAlignmentOptions.Middle)]
        [TestCase(TextAlignment.End, VerticalAlignmentOptions.Bottom)]
        public void Text_Has_Correct_Vertical_Alignment(TextAlignment alignment, VerticalAlignmentOptions expected)
        {
            var options = new ViewJsonOptions(
                DataStyle.DefaultLight,
                DataFormat.DefaultText.WithVerticalTextAlignment(alignment));

            var code = ViewJson.TryViewJsonIn(transform, "123", options);
            Assert.That(code, Is.EqualTo(ViewJsonResultCode.Ok), "Should convert ok");

            var text = transform.GetChild(0)?.GetComponent<TMP_Text>()!;
            Assert.That(text, Is.Not.Null, "Child should exist");

            Assert.That(text.verticalAlignment, Is.EqualTo(expected));
        }
    }
}