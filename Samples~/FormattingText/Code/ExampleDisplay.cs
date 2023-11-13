using Newtonsoft.Json;
using TMPro;
using UnityEngine;

namespace At.Ac.FhStp.ViewJson.Samples.TextFormatting
{
    public class ExampleDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;
        [SerializeField] private RectTransform outputTransform;
        [SerializeField] private DataFormatKeeper formatKeeper;

        private void UpdateDisplay()
        {
            for (var i = 0; i < outputTransform.childCount; i++)
                Destroy(outputTransform.GetChild(i).gameObject);

            var json = JsonConvert.SerializeObject(input.text);
            var format = formatKeeper.Format;
            var options = new ViewJsonOptions(
                DataStyle.DefaultLight,
                format);

            ViewJson.TryViewJsonIn(outputTransform, json, options);
        }

        private void Awake()
        {
            input.onValueChanged.AddListener(_ => UpdateDisplay());
            formatKeeper.FormatChanged += _ => UpdateDisplay();
        }
    }
}