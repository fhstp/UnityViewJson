using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace At.Ac.FhStp.ViewJson
{
    public class TextFormatTests
    {
        private static readonly JToken testText = new JValue("text");


        [Test]
        public void Text_Is_Horizontally_Aligned_To_Start_By_Default()
        {
            var format = DataFormat.DefaultText;

            var alignment = DataFormat.HorizontalTextAlignmentOf(testText, format);

            Assert.That(alignment, Is.EqualTo(TextAlignment.Start));
        }

        [Test]
        [TestCase(TextAlignment.Start)]
        [TestCase(TextAlignment.Center)]
        [TestCase(TextAlignment.End)]
        public void Horizontal_Text_Alignment_Can_Be_Configured(TextAlignment expected)
        {
            var format = DataFormat.DefaultText.WithHorizontalTextAlignment(expected);

            var alignment = DataFormat.HorizontalTextAlignmentOf(testText, format);

            Assert.That(alignment, Is.EqualTo(expected));
        }

        [Test]
        public void Text_Is_Vertically_Aligned_To_Start_By_Default()
        {
            var format = DataFormat.DefaultText;

            var alignment = DataFormat.VerticalTextAlignmentOf(testText, format);

            Assert.That(alignment, Is.EqualTo(TextAlignment.Start));
        }

        [Test]
        [TestCase(TextAlignment.Start)]
        [TestCase(TextAlignment.Center)]
        [TestCase(TextAlignment.End)]
        public void Vertical_Text_Alignment_Can_Be_Configured(TextAlignment expected)
        {
            var format = DataFormat.DefaultText.WithVerticalTextAlignment(expected);

            var alignment = DataFormat.VerticalTextAlignmentOf(testText, format);

            Assert.That(alignment, Is.EqualTo(expected));
        }
    }
}