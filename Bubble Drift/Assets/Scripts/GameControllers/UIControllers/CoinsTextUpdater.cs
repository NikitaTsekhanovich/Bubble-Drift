using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GameControllers.UIControllers
{
    public class CoinsTextUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsMainScreenText;
        [SerializeField] private TMP_Text[] _coinsOtherScreensTexts;
        private const float AnimationInterval = 0.5f;

        public void UpdateCoinsText(int currentCoins, int coinsAmount)
        {
            _coinsMainScreenText.text = coinsAmount.ToString();
            
            DOTween.Sequence()
                .Append(_coinsMainScreenText.DOColor(Color.green, AnimationInterval))
                .Append(_coinsMainScreenText.DOColor(Color.white, AnimationInterval));
            
            foreach (var coinsText in _coinsOtherScreensTexts)
            {
                coinsText.text = $"+{currentCoins}";
            }
        }
    }
}
