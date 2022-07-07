using System;
using UnityEngine;

namespace Clocky
{
    [CreateAssetMenu(fileName = "Scriptable Time", menuName = "Scriptable Objects/Time")]

    public class ScriptableTime : ScriptableObject
    {
        public DateTime CurrentTime;
        public DateTime CurrentTimeSolid;
        public float Milliseconds => CurrentTime.Millisecond;
        public float Seconds => CurrentTime.Second;
        public float Minutes => CurrentTime.Minute;
        public float Hours => CurrentTime.Hour;
        public string Timezone;

        public void UpdateValues(DateTime time, string timezone)
        {
            CurrentTime = time;
            CurrentTimeSolid = time;
            Timezone = timezone;
        }
    }
}
