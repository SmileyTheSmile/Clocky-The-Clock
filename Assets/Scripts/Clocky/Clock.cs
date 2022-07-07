using UnityEngine;

namespace Clocky
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private ScriptableTime _timeSO;
        [SerializeField] private RectTransform _secondsHand;
        [SerializeField] private RectTransform _minutesHand;
        [SerializeField] private RectTransform _hoursHand;
        [SerializeField] private RectTransform _handsPivot;

        private void Start()
        {
            SetHandPivots();
            Update();
        }

        private void Update()
        {
            float millisecondsNormalized = _timeSO.Milliseconds / 1000f;
            float secondsNormalized = (_timeSO.Seconds + millisecondsNormalized) / 60f;
            float minutesNormalized = (_timeSO.Minutes + secondsNormalized) / 60f;
            float hoursNormalized = (_timeSO.Hours + minutesNormalized) / 12f;

            _secondsHand.rotation = Quaternion.Euler(0, 0, 360f * -secondsNormalized);
            _minutesHand.rotation = Quaternion.Euler(0, 0, 360f * -minutesNormalized);
            _hoursHand.rotation = Quaternion.Euler(0, 0, 360f * -hoursNormalized);
        }

        private void SetHandPivots()
        {
            _secondsHand.pivot = new Vector2(_secondsHand.pivot.x, _handsPivot.anchoredPosition.y);
            _minutesHand.pivot = new Vector2(_minutesHand.pivot.x, _handsPivot.anchoredPosition.y);
            _hoursHand.pivot = new Vector2(_hoursHand.pivot.x, _handsPivot.anchoredPosition.y);
        }
    }
}