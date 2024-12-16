using TMPro;
using UnityEngine;

namespace GameControllers.UIControllers
{
    public class WaveTextUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _waveText;
        [SerializeField] private TMP_Text _timeText;

        public void UpdateNumberWaveText(int indexWave)
        {
            _waveText.text = $"{indexWave + 1} wave";
        }

        public void UpdateTimeWaveText(float currentTime)
        {
            if (currentTime <= 0) return;
            
            var integerPart = Mathf.FloorToInt(currentTime);
            var floatPart = Mathf.FloorToInt((currentTime - integerPart) * 10);
            
            _timeText.text = $"{integerPart}.{floatPart}";
        }
    }
}
