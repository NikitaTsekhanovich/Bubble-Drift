using GameControllers.GameLogic;
using GameControllers.UIControllers;
using PlayerData;
using UnityEngine;

namespace GameControllers.PlayerControllers
{
    public class CoinsHandler : MonoBehaviour
    {
        [SerializeField] private CoinsTextUpdater _coinsTextUpdater;
        private int _amountCoins;
        private int _currentCoins;

        private void Start()
        {
            _amountCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsAmountKey);
            _coinsTextUpdater.UpdateCoinsText(_currentCoins, _amountCoins);
        }

        private void OnEnable()
        {
            WaveController.OnIncreaseCoins += IncreaseCoins;
        }

        private void OnDestroy()
        {
            WaveController.OnIncreaseCoins -= IncreaseCoins;
        }

        private void IncreaseCoins(int coinsAmount)
        {
            _currentCoins += coinsAmount;
            _amountCoins += coinsAmount;
            PlayerPrefs.SetInt(PlayerDataKeys.CoinsAmountKey, _amountCoins);
            _coinsTextUpdater.UpdateCoinsText(_currentCoins, _amountCoins);
        }
    }
}
