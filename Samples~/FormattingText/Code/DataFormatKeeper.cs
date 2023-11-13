using System;
using UnityEngine;

namespace At.Ac.FhStp.ViewJson.Samples.TextFormatting
{
    public class DataFormatKeeper : MonoBehaviour
    {
        public event Action<DataFormat> FormatChanged;


        [SerializeField] private TextAlignmentInput horizontalAlignmentInput;
        [SerializeField] private TextAlignmentInput verticalAlignmentInput;


        private void UpdateFormat()
        {
            var horizontalAlignment = horizontalAlignmentInput.Value;
            var verticalAlignment = verticalAlignmentInput.Value;

            var format = DataFormat.DefaultText
                                   .WithHorizontalTextAlignment(horizontalAlignment)
                                   .WithVerticalTextAlignment(verticalAlignment);
            FormatChanged?.Invoke(format);
        }

        private void Awake()
        {
            horizontalAlignmentInput.ValueChanged += _ => UpdateFormat();
            verticalAlignmentInput.ValueChanged += _ => UpdateFormat();
        }
    }
}