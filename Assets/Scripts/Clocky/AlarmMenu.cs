using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Clocky
{
    public class AlarmMenu : MonoBehaviour
    {
        [SerializeField] private ScriptableTime _timeSO;
        [SerializeField] private Slider _secondsSlider;
        [SerializeField] private Slider _minutesSlider;
        [SerializeField] private Slider _hoursSlider;
        
        private TMP_Text _secondsText;
        private TMP_Text _minutesText;
        private TMP_Text _hoursText;

        public void Awake()
        {
            _secondsText = _secondsSlider.GetComponentInChildren<TMP_Text>();
            _minutesText = _minutesSlider.GetComponentInChildren<TMP_Text>();
            _hoursText = _hoursSlider.GetComponentInChildren<TMP_Text>();
        }

        public void Start()
        {
            Update();
        }

        public void Update()
        {
            _secondsText.text = _timeSO.seconds.ToString();
            _minutesText.text = _timeSO.minutes.ToString();
            _hoursText.text = _timeSO.hours.ToString();

            _secondsSlider.value = (int)_timeSO.seconds;
            _minutesSlider.value = (int)_timeSO.minutes;
            _hoursSlider.value = (int)_timeSO.hours;
        }
    }
}
