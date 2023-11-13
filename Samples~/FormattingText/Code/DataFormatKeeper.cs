using System;
using UnityEngine;

namespace At.Ac.FhStp.ViewJson.Samples.TextFormatting
{
    public class DataFormatKeeper : MonoBehaviour
    {
        public event Action<DataFormat> FormatChanged;


        [SerializeField] private TextAlignmentInput horizontalAlignmentInput;
        [SerializeField] private TextAlignmentInput verticalAlignmentInput;


        public DataFormat Format { get; private set; } = DataFormat.Default;


        private void UpdateFormat()
        {
            var horizontalAlignment = horizontalAlignmentInput.Value;
            var verticalAlignment = verticalAlignmentInput.Value;

            Format = DataFormat.DefaultText
                               .WithHorizontalTextAlignment(horizontalAlignment)
                               .WithVerticalTextAlignment(verticalAlignment);
            FormatChanged?.Invoke(Format);
        }

        private void Awake()
        {
            horizontalAlignmentInput.ValueChanged += _ => UpdateFormat();
            verticalAlignmentInput.ValueChanged += _ => UpdateFormat();
        }
    }
}