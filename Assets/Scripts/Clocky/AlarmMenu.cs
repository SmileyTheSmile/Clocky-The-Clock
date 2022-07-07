using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Clocky
{
    public class AlarmMenu : MonoBehaviour
    {
        [SerializeField] private ScriptableTime _timeSO;
        [SerializeField] private Button _alarmButton;
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
            _secondsText.text = _timeSO.Seconds.ToString();
            _minutesText.text = _timeSO.Minutes.ToString();
            _hoursText.text = _timeSO.Hours.ToString();

            _secondsSlider.value = (int)_timeSO.Seconds;
            _minutesSlider.value = (int)_timeSO.Minutes;
            _hoursSlider.value = (int)_timeSO.Hours;
        }
    }
}
