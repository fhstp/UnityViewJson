using UnityEngine;

namespace At.Ac.FhStp.ViewJson
{
    public class DataStyle : IDataStyle
    {
        public Color TextColor { get; }


        public DataStyle(Color textColor)
        {
            TextColor = textColor;
        }


        public static DataStyle DefaultLight => new DataStyle(Color.black);
    }
}