using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using OnlineFPS.Constants;

namespace OnlineFPS.UI.PlayerStats
{
    public class RespawnPanelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private string[] _counterTexts;
        [SerializeField] private Animator _respawnPanelBGAnimator;
        [SerializeField] private float _respawnPanelAnimationLength;

        private int _currentCounterText = 0;

        public async UniTask StartRespawnPanelAnimation()
        {
            _respawnPanelBGAnimator.SetTrigger(UIAnimatorsParameters.Respawn);
            await UniTask.WaitForSeconds(_respawnPanelAnimationLength);
        }

        private void ChangeCounterText()
        {
            _currentCounterText = _currentCounterText == _counterTexts.Length - 1 ? 0 : _currentCounterText + 1;
            var newText = _counterTexts[_currentCounterText];

            _counterText.text = newText;
        }
    }
}