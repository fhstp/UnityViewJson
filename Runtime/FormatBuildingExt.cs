using System;

namespace At.Ac.FhStp.ViewJson
{
    public static class FormatBuildingExt
    {
        public static DataFormat WithHorizontalTextAlignment(this DataFormat format, TextAlignment alignment)
        {
            return format switch
            {
                DataFormat.Text text =>
                    new DataFormat.Text(
                        alignment,
                        text.VerticalAlignment,
                        text.Postfix),
                _ => throw new ArgumentException("Invalid format type")
            };
        }

        public static DataFormat WithVerticalTextAlignment(this DataFormat format, TextAlignment alignment)
        {
            return format switch
            {
                DataFormat.Text text =>
                    new DataFormat.Text(
                        text.HorizontalAlignment,
                        alignment,
                        text.Postfix),
                _ => throw new ArgumentException("Invalid format type")
            };
        }

        public static DataFormat WithPostfix(this DataFormat format, string postfix)
        {
            return format switch
            {
                DataFormat.Text text =>
                    new DataFormat.Text(
                        text.HorizontalAlignment,
                        text.VerticalAlignment,
                        postfix),
                _ => throw new ArgumentException("Invalid format type")
            };
        }
    }
}