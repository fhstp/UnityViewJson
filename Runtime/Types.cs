namespace At.Ac.FhStp.ViewJson
{

    /// <summary>
    /// Specifies how to style various types of Json elements
    /// </summary>
    public interface IViewJsonStyle
    {
        
    }

    /// <summary>
    /// Specifies the structure of a json document
    /// </summary>
    public interface IViewJsonSchema
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
        public IViewJsonStyle Style { get; }
        
        /// <summary>
        /// The schema to use
        /// </summary>
        public IViewJsonSchema Schema { get; }

        public ViewJsonOptions(IViewJsonStyle style, IViewJsonSchema schema)
        {
            Style = style;
            Schema = schema;
        }
    }

    /// <summary>
    /// Represents the possible results of trying to view json
    /// </summary>
    public enum ViewJsonResultCode
    {
        
    }
    
}