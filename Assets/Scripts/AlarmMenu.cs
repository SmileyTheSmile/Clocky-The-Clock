using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Clocky
{
    public class AlarmMenu : MonoBehaviour
    {
        [SerializeField] private Slider _secondsSlider;
        [SerializeField] private Slider _minutesSlider;
        [SerializeField] private Slider _hoursSlider;
        [SerializeField] private TMP_Text _secondsText;
        [SerializeField] private TMP_Text _minutesText;
        [SerializeField] private TMP_Text _hoursText;

        public void Setup(int hours, int minutes, int seconds, int milliseconds)
        {
            UpdateTime(hours, minutes, seconds, milliseconds);
        }

        public void UpdateTime(int hours, int minutes, int seconds, int milliseconds)
        {
            _secondsText.text = seconds.ToString();
            _minutesText.text = minutes.ToString();
            _hoursText.text = hours.ToString();

            _secondsSlider.value = seconds;
            _minutesSlider.value = minutes;
            _hoursSlider.value = hours;
        }
    }
}
