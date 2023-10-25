#nullable enable

using System;
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
                transform.name = "Text";
                var text = transform.gameObject.AddComponent<TextMeshProUGUI>();
                text.text = s;

                return ViewJsonResultCode.Ok;
            }

            ViewJsonResultCode Convert(JToken token, RectTransform container)
            {
                if (token.Type == JTokenType.String)
                    return ConvertString(token.Value<string>()!, container);

                return ViewJsonResultCode.UnsupportedToken;
            }

            var parsed = TryParse(json);
            if (parsed == null) return ViewJsonResultCode.InvalidJson;

            return Convert(parsed, root);
        }
    }
}