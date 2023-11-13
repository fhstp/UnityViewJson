using System;
using TMPro;
using UnityEngine;

namespace At.Ac.FhStp.ViewJson.Samples.TextFormatting
{
    public class DataFormatKeeper : MonoBehaviour
    {
        public event Action<DataFormat> FormatChanged;


        [SerializeField] private TextAlignmentInput horizontalAlignmentInput;
        [SerializeField] private TextAlignmentInput verticalAlignmentInput;
        [SerializeField] private TMP_InputField postfixInput;


        public DataFormat Format { get; private set; } = DataFormat.Default;


        private void UpdateFormat()
        {
            var horizontalAlignment = horizontalAlignmentInput.Value;
            var verticalAlignment = verticalAlignmentInput.Value;

            Format = DataFormat.DefaultText
                               .WithHorizontalTextAlignment(horizontalAlignment)
                               .WithVerticalTextAlignment(verticalAlignment);
            if (postfixInput.text != string.Empty)
                Format = Format.WithPostfix(postfixInput.text);
            FormatChanged?.Invoke(Format);
        }

        private void Awake()
        {
            horizontalAlignmentInput.ValueChanged += _ => UpdateFormat();
            verticalAlignmentInput.ValueChanged += _ => UpdateFormat();
            postfixInput.onValueChanged.AddListener(_ => UpdateFormat());
        }
    }
}