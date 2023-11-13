using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace At.Ac.FhStp.ViewJson.Samples.TextFormatting
{
    public class TextAlignmentInput : MonoBehaviour
    {
        public event Action<TextAlignment> ValueChanged;


        [SerializeField] private Color highlightColor;

        private Button[] buttons;
        private TextAlignment value;


        public TextAlignment Value
        {
            get => value;
            private set
            {
                if (value == this.value) return;
                this.value = value;
                UpdateButtonStyles();
                ValueChanged?.Invoke(Value);
            }
        }


        private void UpdateButtonStyles()
        {
            for (var i = 0; i < 3; i++)
            {
                var isHighlighted = i == (int) Value;
                buttons[i].GetComponent<Image>().color = isHighlighted ? highlightColor : Color.white;
            }
        }

        private void Awake()
        {
            buttons = GetComponentsInChildren<Button>();
            Assert.AreEqual(3, buttons.Length);

            for (var i = 0; i < buttons.Length; i++)
            {
                var alignment = (TextAlignment) i;
                buttons[i].onClick.AddListener(() => Value = alignment);
            }
        }

        private void Start()
        {
            UpdateButtonStyles();
            ValueChanged?.Invoke(Value);
        }
    }
}