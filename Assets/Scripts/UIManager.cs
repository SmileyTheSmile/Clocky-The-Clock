using System;
using UnityEngine;

namespace Clocky
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextAsset _apiFile;

        private WebManager _webManager;

        private Clock _clock;
        private DigitalClock _digitalClock;
        private TimezoneDisplay _timezoneDisplay;
        private AlarmMenu _alarmMenu;

        private string[] _apiUrls;

        private int _hours;
        private int _minutes;
        private int _seconds;
        private int _milliseconds;

        private void Awake()
        {
            _apiUrls = _apiFile.text.Split("/n");

            _webManager = GetComponent<WebManager>();
            _clock = GetComponentInChildren<Clock>();
            _digitalClock = GetComponentInChildren<DigitalClock>();
            _timezoneDisplay = GetComponentInChildren<TimezoneDisplay>();
            _alarmMenu = GetComponentInChildren<AlarmMenu>();
        }

        private void Start()
        {
            UpdateTime();

            _timezoneDisplay.Setup(TimeZoneInfo.Local.ToString());
            _clock.Setup(_hours, _minutes, _seconds, _milliseconds);
            _digitalClock.Setup(_hours, _minutes, _seconds, _milliseconds);
            _alarmMenu.Setup(_hours, _minutes, _seconds, _milliseconds);
        }
        
        private void Update()
        {
            UpdateTime();

            _digitalClock.UpdateTime(_hours, _minutes, _seconds, _milliseconds);
            _clock.UpdateTime(_hours, _minutes, _seconds, _milliseconds);
            _alarmMenu.UpdateTime(_hours, _minutes, _seconds, _milliseconds);
        }

        private void UpdateTime()
        {
            DateTime time = DateTime.Now;
            //StartCoroutine("GetRequest", worldTimeApiUrl);

            _hours = time.Hour;
            _minutes = time.Minute;
            _seconds = time.Second;
            _milliseconds = time.Millisecond;
        }
    }
}