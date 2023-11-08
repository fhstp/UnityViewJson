using System;

namespace At.Ac.FhStp.ViewJson
{
    public static class FormatBuildingExt
    {
        public static DataFormat WithHorizontalTextAlignment(this DataFormat format, TextAlignment alignment)
        {
            return format switch
            {
                DataFormat.Text _ =>
                    new DataFormat.Text(alignment),
                _ => throw new ArgumentException("Invalid format type")
            };
        }
    }
}