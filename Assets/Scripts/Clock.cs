using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    [SerializeField] private RectTransform _secondsHand;
    [SerializeField] private RectTransform _minutesHand;
    [SerializeField] private RectTransform _hoursHand;
    [SerializeField] private RectTransform _handsPivot;

    public void Setup(DateTime currentTime)
    {
        SetHandPivots();
        UpdateHands(currentTime);
    }

    public void UpdateHands(DateTime currentTime)
    {
        _secondsHand.rotation = Quaternion.Euler(0, 0, 360f * -((float)currentTime.Second / 60f + (float)currentTime.Millisecond / 60000f));
        _minutesHand.rotation = Quaternion.Euler(0, 0, 360f * -((float)currentTime.Minute / 60f));
        _hoursHand.rotation = Quaternion.Euler(0, 0, 360f * -((float)currentTime.Hour / 12f));
    }

    private void SetHandPivots()
    {
        _secondsHand.pivot = new Vector2(_secondsHand.pivot.x, _handsPivot.anchoredPosition.y);
        _minutesHand.pivot = new Vector2(_minutesHand.pivot.x, _handsPivot.anchoredPosition.y);
        _hoursHand.pivot = new Vector2(_hoursHand.pivot.x, _handsPivot.anchoredPosition.y);
    }
}
