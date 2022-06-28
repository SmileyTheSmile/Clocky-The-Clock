using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

namespace Clocky
{
    public class WebManager : MonoBehaviour
    {
        private WorldTimeAPI _jsonResult = new WorldTimeAPI();

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
                        _jsonResult = JsonUtility.FromJson<WorldTimeAPI>(webRequest.downloadHandler.text);
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
