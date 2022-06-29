using UnityEngine;

namespace Clocky
{
    [CreateAssetMenu(fileName = "Scriptable Time", menuName = "Scriptable Objects/Time")]

    public class ScriptableTime : ScriptableObject
    {
        public float milliseconds;
        public float seconds;
        public float minutes;
        public float hours;
        public string timezone;
    }
}
