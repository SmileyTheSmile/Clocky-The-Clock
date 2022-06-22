using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    [Header("Hands")]
    [SerializeField] private RectTransform secondsHand;
    [SerializeField] private RectTransform minutesHand;
    [SerializeField] private RectTransform hoursHand;

    [SerializeField] private RectTransform handsPivot;

    [SerializeField] private string[] apiUrls; //http://worldtimeapi.org/api/timezone/Europe/Kaliningrad

    private TimeParameters jsonResult = new TimeParameters();

    private void Awake()
    {
        SetHandPivots();
    }

    private void Start()
    {
        //StartCoroutine("GetRequest", worldTimeApiUrl);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateHands();
    }

    private void SetHandPivots()
    {
        secondsHand.pivot = new Vector2(secondsHand.pivot.x, handsPivot.anchoredPosition.y);
        minutesHand.pivot = new Vector2(minutesHand.pivot.x, handsPivot.anchoredPosition.y);
        hoursHand.pivot = new Vector2(hoursHand.pivot.x, handsPivot.anchoredPosition.y);
    }

    private void UpdateHands()
    {
        DateTime currentTime = DateTime.Now;

        secondsHand.rotation = Quaternion.Euler(0, 0, 360f * -((float)currentTime.Second / 60f));
        minutesHand.rotation = Quaternion.Euler(0, 0, 360f * -((float)currentTime.Minute / 60f));
        hoursHand.rotation = Quaternion.Euler(0, 0, 360f * -((float)currentTime.Hour / 12f));
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
                    jsonResult = JsonUtility.FromJson<TimeParameters>(webRequest.downloadHandler.text);
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
