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
                    new DataFormat.Text(alignment, text.VerticalAlignment),
                _ => throw new ArgumentException("Invalid format type")
            };
        }

        public static DataFormat WithVerticalTextAlignment(this DataFormat format, TextAlignment alignment)
        {
            return format switch
            {
                DataFormat.Text text =>
                    new DataFormat.Text(text.HorizontalAlignment, alignment),
                _ => throw new ArgumentException("Invalid format type")
            };
        }
    }
}