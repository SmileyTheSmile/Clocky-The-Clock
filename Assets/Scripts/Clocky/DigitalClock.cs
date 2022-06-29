using UnityEngine;
using TMPro;

namespace Clocky
{
    public class DigitalClock : MonoBehaviour
    {
        [SerializeField] private ScriptableTime _timeSO;
        [SerializeField] private TMP_Text _text;

        public void Start()
        {
            Update();
        }

        public void Update()
        {
            string hoursStr = string.Format("{0:00}", _timeSO.hours);
            string minutesStr = string.Format("{0:00}", _timeSO.minutes);
            string secondsStr = string.Format("{0:00}", _timeSO.seconds);
            string millisecondsStr = string.Format("{0:000}", _timeSO.milliseconds);

            _text.text = $"{hoursStr}.{minutesStr}.{secondsStr}.{millisecondsStr}";
        }
    }
}
