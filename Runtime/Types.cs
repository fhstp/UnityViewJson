#nullable enable

namespace At.Ac.FhStp.ViewJson
{
    /// <summary>
    /// Specifies how to style various types of Json elements
    /// </summary>
    public interface IDataStyle
    {
    }


    /// <summary>
    /// Contains options for viewing json
    /// </summary>
    public struct ViewJsonOptions
    {
        /// <summary>
        /// The style to use
        /// </summary>
        public IDataStyle Style { get; }

        /// <summary>
        /// The format to use
        /// </summary>
        public DataFormat Format { get; }

        public ViewJsonOptions(IDataStyle style, DataFormat format)
        {
            Style = style;
            Format = format;
        }
    }

    /// <summary>
    /// Represents the possible results of trying to view json
    /// </summary>
    public enum ViewJsonResultCode
    {
        Ok = 0,
        InvalidJson = 1,
        UnsupportedToken = 2
    }
}