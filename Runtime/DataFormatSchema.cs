#nullable enable

using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
using UnityEngine;
using UnityEngine.Networking;

namespace At.Ac.FhStp.ViewJson
{
    public static class DataFormatSchema
    {
        private static readonly string path = Path.Combine(Application.streamingAssetsPath, "format.schema.json");

        private static readonly Uri uri = new Uri($"file://{path}");

        public static async Task<JSchema> LoadAsync()
        {
            var request = UnityWebRequest.Get(uri);
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            var schemaJson = request.downloadHandler.text!;
            return JSchema.Parse(schemaJson);
        }
    }
}