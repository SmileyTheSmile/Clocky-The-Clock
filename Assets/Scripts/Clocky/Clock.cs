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

        public void Start()
        {
            SetHandPivots();
            Update();
        }

        public void Update()
        {
            float millisecondsNormalized = _timeSO.milliseconds / 1000f;
            float secondsNormalized = (_timeSO.seconds + millisecondsNormalized) / 60f;
            float minutesNormalized = (_timeSO.minutes + secondsNormalized) / 60f;
            float hoursNormalized = (_timeSO.hours + minutesNormalized) / 12f;

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