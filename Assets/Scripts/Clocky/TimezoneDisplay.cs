using UnityEngine;
using TMPro;

namespace Clocky
{
    public class TimezoneDisplay : MonoBehaviour
    {
        [SerializeField] private ScriptableTime _timeSO;
        [SerializeField] private TMP_Text _timezoneText;

        public void Start()
        {
            _timezoneText.text = _timeSO.Timezone;
        }
    }
}
