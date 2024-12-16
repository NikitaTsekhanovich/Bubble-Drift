using System;
using DG.Tweening;
using PlayerData;
using SceneLoaderControllers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EducationsControllers
{
    public class EducationClick : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject _educationClickPanel;
        [SerializeField] private Image _clickFrameImage;
        private Sequence _animationFrameClick;
        
        private void Awake()
        {
            CheckEducationCompleted();
        }

        private void StartAnimationClickFrame()
        {
            _animationFrameClick = DOTween.Sequence()
                .Append(_clickFrameImage.DOFade(0, 0.5f))
                .Append(_clickFrameImage.DOFade(1, 0.5f))
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void CheckEducationCompleted()
        {
            if (PlayerPrefs.GetInt(PlayerDataKeys.IsEducationClickCompletedKey) == (int)ModeLoadScene.IsFirstLaunch)
            {
                StartAnimationClickFrame();
                PlayerPrefs.SetInt(PlayerDataKeys.IsEducationClickCompletedKey, (int)ModeLoadScene.IsNotFirstLaunch);
            }
            else
            {
                _educationClickPanel.SetActive(false);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _animationFrameClick.Kill();
            _educationClickPanel.SetActive(false);
        }
    }
}
