#nullable enable

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

            ViewJsonResultCode ConvertString(string s, RectTransform container)
            {
                var transform = AddChild(container, true);
                FillParent(transform);

                transform.name = "Text";
                var text = transform.gameObject.AddComponent<TextMeshProUGUI>();
                text.text = s;
                text.color = options.Style.TextColor;

                return ViewJsonResultCode.Ok;
            }

            ViewJsonResultCode ConvertInteger(int i, RectTransform container) =>
                ConvertString(i.ToString(), container);

            ViewJsonResultCode ConvertFloat(float f, RectTransform container) =>
                ConvertString(f.ToString(CultureInfo.InvariantCulture), container);

            ViewJsonResultCode ConvertBoolean(bool b, RectTransform container) =>
                ConvertString(b.ToString(), container);

            ViewJsonResultCode ConvertNull(RectTransform container) =>
                ConvertString("null", container);

            ViewJsonResultCode Convert(JToken token, RectTransform container)
            {
                return token.Type switch
                {
                    JTokenType.String => ConvertString(token.Value<string>()!, container),
                    JTokenType.Integer => ConvertInteger(token.Value<int>(), container),
                    JTokenType.Float => ConvertFloat(token.Value<float>(), container),
                    JTokenType.Boolean => ConvertBoolean(token.Value<bool>(), container),
                    JTokenType.Null => ConvertNull(container),
                    _ => ViewJsonResultCode.UnsupportedToken
                };
            }

            var parsed = TryParse(json);
            if (parsed == null) return ViewJsonResultCode.InvalidJson;

            return Convert(parsed, root);
        }
    }
}