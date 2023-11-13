using Newtonsoft.Json;
using TMPro;
using UnityEngine;

namespace At.Ac.FhStp.ViewJson.Samples.TextFormatting
{
    public class FormatDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text jsonDisplay;

        private void OnFormatChanged(DataFormat format)
        {
            var json = DataFormat.Serialize(format);
            jsonDisplay.text = json.ToString(Formatting.Indented);
        }

        private void Awake()
        {
            FindObjectOfType<DataFormatKeeper>().FormatChanged += OnFormatChanged;
        }
    }
}