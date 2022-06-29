using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

namespace Clocky
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private ScriptableTime _timeSO;
        [SerializeField] private TextAsset _apiFile;

        private string _worldApiByIP = "http://worldtimeapi.org/api/ip";
        private string _timeApiByIP = "https://timeapi.io/api/Time/current/ip";
        private string _timeApiByCoordinate = "https://timeapi.io/api/Time/current/coordinate";

        private string _jsonResult = null;

        private void Awake()
        {
            StartCoroutine("GetRequest", _worldApiByIP);
        }

        private void Update()
        {
            DateTime time = DateTime.Now;
            string timezone = TimeZoneInfo.Local.ToString();

            var jo = new JObject();
            if (_jsonResult != null)
                jo = JObject.Parse(_jsonResult);

            Debug.Log(jo);

            _timeSO.hours = time.Hour;
            _timeSO.minutes = time.Minute;
            _timeSO.seconds = time.Second;
            _timeSO.milliseconds = time.Millisecond;
            _timeSO.timezone = timezone;
        }

        private IEnumerator GetRequest(string url)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();
                while (!webRequest.isDone)
                {
                    Debug.Log($"Connecting to {url}");
                    yield return null;
                }

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError($"Error: {webRequest.error}");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError($"HTTP Error: {webRequest.error}");
                        break;
                    case UnityWebRequest.Result.Success:
                        _jsonResult = webRequest.downloadHandler.text;
                        Debug.Log($"Received: {webRequest.downloadHandler.text}");
                        break;
                }
            }
        }

        private class WorldTimeAPI
        {
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

        private class TimeAPI
        {
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
