using UnityEngine;
using TMPro;

namespace Clocky
{
    public class TimezoneDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timezoneText;

        public void Setup(string timezone)
        {
            _timezoneText.text = timezone;
        }
    }
}
