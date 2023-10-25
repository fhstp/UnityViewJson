using System;
using UnityEngine;

namespace At.Ac.FhStp.ViewJson
{
    /// <summary>
    /// This class contains functions for converting json to Unity UGUI
    /// </summary>
    public static class ViewJson
    {
        /// <summary>
        /// Attempts to view json data inside of a rect-transform
        /// </summary>
        /// <param name="transform">The instantiated UI elements will be placed inside/as a child of this transform</param>
        /// <param name="json">The json to view</param>
        /// <param name="options">Options of how to view the json</param>
        /// <returns>A result code representing the success or failure of the conversion</returns>
        public static ViewJsonResultCode TryViewJsonIn(RectTransform transform, string json, ViewJsonOptions options)
        {
            throw new NotImplementedException();
        }
        
    }
}