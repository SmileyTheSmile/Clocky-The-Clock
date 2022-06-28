using UnityEngine;

namespace Clocky
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private RectTransform _secondsHand;
        [SerializeField] private RectTransform _minutesHand;
        [SerializeField] private RectTransform _hoursHand;
        [SerializeField] private RectTransform _handsPivot;

        public void Setup(int hours, int minutes, int seconds, int milliseconds)
        {
            SetHandPivots();
            UpdateTime(hours, minutes, seconds, milliseconds);
        }

        public void UpdateTime(int hours, int minutes, int seconds, int milliseconds)
        {
            float millisecondsNormalized = (float)milliseconds / 1000f;
            float secondsNormalized = ((float)seconds + millisecondsNormalized) / 60f;
            float minutesNormalized = ((float)minutes + secondsNormalized) / 60f;
            float hoursNormalized = ((float)hours + minutesNormalized) / 12f;

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