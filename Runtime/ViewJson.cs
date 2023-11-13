#nullable enable

using System;
using System.Globalization;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;

namespace At.Ac.FhStp.ViewJson
{
    /// <summary>
    /// This class contains functions for converting json to Unity UGUI
    /// </summary>
    public static class ViewJson
    {
        private static JToken? TryParse(string json)
        {
            try
            {
                return JToken.Parse(json);
            }
            catch
            {
                return null;
            }
        }

        private static void FillParent(RectTransform transform)
        {
            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;
            transform.sizeDelta = Vector2.zero;
            transform.offsetMin = Vector2.zero;
            transform.offsetMax = Vector2.zero;
            transform.localScale = Vector3.one;
        }

        private static TextAlignmentOptions ToTMPAlignment(TextAlignment horizontal, TextAlignment vertical)
        {
            return (horizontal, vertical) switch
            {
                (TextAlignment.Start, TextAlignment.Start) => TextAlignmentOptions.TopLeft,
                (TextAlignment.Start, TextAlignment.Center) => TextAlignmentOptions.Left,
                (TextAlignment.Start, TextAlignment.End) => TextAlignmentOptions.BottomLeft,
                (TextAlignment.Center, TextAlignment.Start) => TextAlignmentOptions.Top,
                (TextAlignment.Center, TextAlignment.Center) => TextAlignmentOptions.Center,
                (TextAlignment.Center, TextAlignment.End) => TextAlignmentOptions.Bottom,
                (TextAlignment.End, TextAlignment.Start) => TextAlignmentOptions.TopRight,
                (TextAlignment.End, TextAlignment.Center) => TextAlignmentOptions.Right,
                (TextAlignment.End, TextAlignment.End) => TextAlignmentOptions.BottomRight,
                _ => throw new ArgumentOutOfRangeException()
            };
        }


        /// <summary>
        /// Attempts to view json data inside of a rect-transform
        /// </summary>
        /// <param name="root">The instantiated UI elements will be placed inside/as a child of this transform</param>
        /// <param name="json">The json to view</param>
        /// <param name="options">Options of how to view the json</param>
        /// <returns>A result code representing the success or failure of the conversion</returns>
        public static ViewJsonResultCode TryViewJsonIn(RectTransform root, string json, ViewJsonOptions options)
        {
            RectTransform AddChild(RectTransform container, bool withRenderer)
            {
                var child = new GameObject("", typeof(RectTransform));
                var childTransform = child.GetComponent<RectTransform>()!;
                if (withRenderer)
                    childTransform.gameObject.AddComponent<CanvasRenderer>();
                childTransform.SetParent(container);
                return childTransform;
            }

            ViewJsonResultCode Convert(JToken token, RectTransform container)
            {
                ViewJsonResultCode ConvertString(string s)
                {
                    var transform = AddChild(container, true);
                    FillParent(transform);

                    transform.name = "Text";
                    var text = transform.gameObject.AddComponent<TextMeshProUGUI>();
                    text.text = s;
                    text.color = options.Style.TextColor;
                    text.alignment = ToTMPAlignment(
                        DataFormat.HorizontalTextAlignmentOf(token, options.Format),
                        DataFormat.VerticalTextAlignmentOf(token, options.Format));

                    return ViewJsonResultCode.Ok;
                }

                ViewJsonResultCode ConvertInteger(int i) =>
                    ConvertString(i.ToString());

                ViewJsonResultCode ConvertFloat(float f) =>
                    ConvertString(f.ToString(CultureInfo.InvariantCulture));

                ViewJsonResultCode ConvertBoolean(bool b) =>
                    ConvertString(b.ToString());

                ViewJsonResultCode ConvertNull() =>
                    ConvertString("null");

                return token.Type switch
                {
                    JTokenType.String => ConvertString(token.Value<string>()!),
                    JTokenType.Integer => ConvertInteger(token.Value<int>()),
                    JTokenType.Float => ConvertFloat(token.Value<float>()),
                    JTokenType.Boolean => ConvertBoolean(token.Value<bool>()),
                    JTokenType.Null => ConvertNull(),
                    _ => ViewJsonResultCode.UnsupportedToken
                };
            }

            var parsed = TryParse(json);
            if (parsed == null) return ViewJsonResultCode.InvalidJson;

            return Convert(parsed, root);
        }
    }
}