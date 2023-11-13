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

            public TextAlignment VerticalAlignment { get; }


            public Text(TextAlignment horizontalAlignment, TextAlignment verticalAlignment)
            {
                HorizontalAlignment = horizontalAlignment;
                VerticalAlignment = verticalAlignment;
            }
        }

        internal class Container : DataFormat
        {
        }

        private const TextAlignment DefaultTextAlignment = TextAlignment.Start;

        public static DataFormat DefaultText = new Text(
            DefaultTextAlignment,
            DefaultTextAlignment);

        public static DataFormat EmptyContainer = new Container();

        public static DataFormat Default = EmptyContainer;

        public static TextAlignment HorizontalTextAlignmentOf(JToken token, DataFormat format)
        {
            return format switch
            {
                Text text => text.HorizontalAlignment,
                _ => HorizontalTextAlignmentOf(token, DefaultText)
            };
        }

        public static TextAlignment VerticalTextAlignmentOf(JToken token, DataFormat format)
        {
            return format switch
            {
                Text text => text.VerticalAlignment,
                _ => HorizontalTextAlignmentOf(token, DefaultText)
            };
        }

        public static JObject Serialize(DataFormat format)
        {
            JObject TextToJson(Text text) =>
                new JObject
                {
                    {"horizontalAlignment", JToken.FromObject(text.HorizontalAlignment)},
                    {"verticalAlignment", JToken.FromObject(text.VerticalAlignment)}
                };

            return format switch
            {
                Text text => TextToJson(text),
                _ => throw new ArgumentException("Invalid format type")
            };
        }
    }
}