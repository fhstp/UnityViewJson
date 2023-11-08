using System;
using Newtonsoft.Json.Linq;

namespace At.Ac.FhStp.ViewJson
{
    /// <summary>
    /// Contains format information about a json element
    /// </summary>
    public abstract class DataFormat
    {
        internal DataFormat()
        {
        }


        internal class Text : DataFormat
        {
            public TextAlignment HorizontalAlignment { get; }


            public Text(TextAlignment horizontalAlignment)
            {
                HorizontalAlignment = horizontalAlignment;
            }
        }

        internal class Container : DataFormat
        {
        }

        private const TextAlignment DefaultTextAlignment = TextAlignment.Start;

        public static DataFormat DefaultText = new Text(DefaultTextAlignment);

        public static DataFormat EmptyContainer = new Container();

        public static DataFormat Default = EmptyContainer;

        public static TextAlignment HorizontalTextAlignmentOf(JToken token, DataFormat format)
        {
            return format switch
            {
                Text text => text.HorizontalAlignment,
                _ => throw new ArgumentException("Invalid format type")
            };
        }
    }
}