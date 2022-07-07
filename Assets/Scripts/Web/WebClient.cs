using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Threading.Tasks;

namespace Web
{
    public class WebClient
    {
        private readonly ISerializationOption _serializationOption;

        public WebClient(ISerializationOption serializationOption)
        {
            _serializationOption = serializationOption;
        }

        [ContextMenu("Test Get")]
        public async Task<TResultType> Get<TResultType>(string url)
        {
            try
            {
                using var www = UnityWebRequest.Get(url);

                www.SetRequestHeader("Content-Type", _serializationOption.ContentType);

                var operation = www.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error: {www.error}");
                    return default;
                }

                var result = _serializationOption.Deserialize<TResultType>(www.downloadHandler.text);

                return result;
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
                return default;
            }
        }
        
        /*private IEnumerator GetRequest(string url)
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
                        Debug.Log($"Received: {webRequest.downloadHandler.text}");
                        break;
                }
            }
        }*/
    }
}
