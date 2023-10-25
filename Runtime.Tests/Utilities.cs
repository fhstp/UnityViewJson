#nullable enable

using UnityEngine;

namespace At.Ac.FhStp.ViewJson
{
    internal static class Utilities
    {

        public static GameObject MakeTestGameObject() => 
            new GameObject( "TestGameObject", typeof(RectTransform));

        public static Canvas MakeTestCanvas()
        {
            var gameObject = MakeTestGameObject();
            gameObject.AddComponent<CanvasRenderer>();
            return gameObject.AddComponent<Canvas>();
        }

        public static RectTransform MakeTestTransform() => 
            MakeTestCanvas().GetComponent<RectTransform>();
    }
}