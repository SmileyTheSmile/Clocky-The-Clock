using UnityEngine;
using TMPro;

namespace Clocky
{
    public class DigitalClock : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Setup(int hours, int minutes, int seconds, int milliseconds)
        {
            UpdateTime(hours, minutes, seconds, milliseconds);
        }

        public void UpdateTime(int hours, int minutes, int seconds, int milliseconds)
        {
            string hoursStr = $"{(hours < 10 ? "0" : "")}{hours}";
            string minutesStr = $"{(minutes < 10 ? "0" : "")}{minutes}";
            string secondsStr = $"{(seconds < 10 ? "0" : "")}{seconds}";
            string millisecondsStr = $"{(milliseconds < 10 ? "00" : milliseconds < 100 ? "0" : "")}{milliseconds}";

            _text.text = $"{hoursStr}.{minutesStr}.{secondsStr}.{millisecondsStr}";
        }
    }
}
