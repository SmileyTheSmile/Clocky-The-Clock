using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private Clock _clock;
    [SerializeField] private TMP_Text _alarmClock;
    [SerializeField] private TMP_Text _timezone;

    [SerializeField] private string[] _apiUrls; //http://worldtimeapi.org/api/timezone/Europe/Kaliningrad

    private TimeParameters _jsonResult = new TimeParameters();

    private void Start()
    {
        _clock.Setup(DateTime.Now);
        //StartCoroutine("GetRequest", worldTimeApiUrl);
    }

    private void Update()
    {
        DateTime time = DateTime.Now;

        UpdateTimezoneText();
        UpdateAlarmClock(time);
        _clock.UpdateHands(time);
    }

    private void UpdateTimezoneText()
    {
        _timezone.text = TimeZoneInfo.Local.ToString();
    }

    private void UpdateAlarmClock(DateTime time)
    {
        string hours = $"{(time.Hour < 10 ? 0 : "")}{time.Hour}";
        string minutes = $"{(time.Minute < 10 ? 0 : "")}{time.Minute}";
        string seconds = $"{(time.Second < 10 ? 0 : "")}{time.Second}";
        string milliseconds = $"{(time.Millisecond < 100 ? 0 : "")}{time.Millisecond}";
        
        _alarmClock.text = $"{hours}.{minutes}.{seconds}.{milliseconds}";
    }
    
    private IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

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
                    _jsonResult = JsonUtility.FromJson<TimeParameters>(webRequest.downloadHandler.text);
                    Debug.Log($"Received: {webRequest.downloadHandler.text}");
                    break;
            }
        }
    }

    private class TimeParameters
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
}
