using System;
using UnityEngine;
using Web;

namespace Clocky
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private ScriptableTime _timeSO;
        [SerializeField] private UpdatePeriod _whenToUpdate;

        private string _worldApiByIP = "http://worldtimeapi.org/api/ip";
        private string _timeApiByIP = "https://timeapi.io/api/Time/current/ip";
        private string _timeApiByCoordinate = "https://timeapi.io/api/Time/current/coordinate";

        private float _lastUpdate;
        private bool _currentlyUpdating;

        private void Start()
        {
            _timeSO.UpdateValues(DateTime.Now, TimeZoneInfo.Local.ToString());
            VerifyTime();
        }

        private void Update()
        {
            float timeSinceLastUpdate = Time.fixedTime - _lastUpdate;

            if (timeSinceLastUpdate >= (int)_whenToUpdate && !_currentlyUpdating)
                VerifyTime();

            TimeSpan timeElapsed = TimeSpan.FromSeconds(timeSinceLastUpdate);
            _timeSO.CurrentTime = _timeSO.CurrentTimeSolid.Add(timeElapsed);
        }

        [ContextMenu("Test Conection")]
        public async void VerifyTime()
        {
            _currentlyUpdating = true;
            WebClient httpClient = new WebClient(new JsonSerializationOption());

            WebAPI result = await httpClient.Get<TimeAPI>(_timeApiByIP);
            if (result == default)
                result = await httpClient.Get<WorldTimeAPI>(_worldApiByIP);
                if (result == default)
                    result = await httpClient.Get<TimeAPI>(_timeApiByCoordinate);

            DateTime time = (result != default) ? DateTime.Parse(result.DateTime) : DateTime.Now;
            string timezone = (result != default) ? result.TimeZone : TimeZoneInfo.Local.ToString();

            TimeSpan error = _timeSO.CurrentTime.Subtract(time);
            Debug.Log($"Time has been updated. The error was {error}.");

            _timeSO.UpdateValues(time, timezone);
            _lastUpdate = Time.fixedTime;
            _currentlyUpdating = false;
        }

        /*private UrlAndAPI _worldApiByIP2 = new UrlAndAPI("http://worldtimeapi.org/api/ip", new WorldTimeAPI());
        private UrlAndAPI _timeApiByIP2 = new UrlAndAPI("https://timeapi.io/api/Time/current/ip", new TimeAPI());
        private UrlAndAPI _timeApiByCoordinate2 = new UrlAndAPI("https://timeapi.io/api/Time/current/coordinate", new TimeAPI());
        
        [ContextMenu("Test Conection")]
        public async void VerifyCurrentInfoDeprecated() //TODO This a generalized function for however many APIs I want to add, but it doesn't work
        {
            List<UrlAndAPI> urlsAndApis = new List<UrlAndAPI>();
            var httpClient = new WebClient(new JsonSerializationOption());

            DateTime time = DateTime.Now;
            string timezone = TimeZoneInfo.Local.ToString();

            urlsAndApis.Add(_worldApiByIP2);
            urlsAndApis.Add(_timeApiByIP2);
            urlsAndApis.Add(_timeApiByCoordinate2);

            foreach (var pair in urlsAndApis)
            {
                Type api = pair.Api.GetType();
                var result = await httpClient.Get<WebAPI>(pair.Url);

                if (result != default)
                {
                    time = DateTime.Parse(result.DateTime);
                    timezone = result.TimeZone;
                    return;
                }
            }
        }

        private class UrlAndAPI
        {
            public string Url;
            public WebAPI Api;

            public UrlAndAPI(string url, WebAPI api)
            {
                Url = url;
                Api = api;
            }
        }*/

        private enum UpdatePeriod
        {
            tenSeconds = 10,
            halfAMinute = 30,
            minute = 60,
            tenMinutes = 600,
            hour = 3600,
        }

        private interface WebAPI
        {
            public string DateTime { get; }
            public string TimeZone { get; }
        }

        private class WorldTimeAPI : WebAPI
        {
            public string DateTime => datetime;
            public string TimeZone => timezone;

            public string abbreviation = "";
            public string client_ip = "";
            public string datetime = "";
            public string day_of_week = "";
            public string day_of_year = "";
            public string dst = "";
            public string dst_from = "";
            public string dst_offset = "";
            public string dst_until = "";
            public string raw_offset = "";
            public string timezone = "";
            public string unixtime = "";
            public string utc_datetime = "";
            public string utc_offset = "";
            public string week_number = "";
        }

        private class TimeAPI : WebAPI
        {
            public string DateTime => dateTime;
            public string TimeZone => timeZone;

            public string year = "";
            public string month = "";
            public string day = "";
            public string hour = "";
            public string minute = "";
            public string seconds = "";
            public string milliSeconds = "";
            public string dateTime = "";
            public string date = "";
            public string time = "";
            public string timeZone = "";
            public string dayOfWeek = "";
            public string dstActive = "";
        }
    }
}
