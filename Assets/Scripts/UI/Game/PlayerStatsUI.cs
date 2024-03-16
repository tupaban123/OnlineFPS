using UnityEngine;
using UnityEngine.UI;
using OnlineFPS.CodeBase;
using Cysharp.Threading.Tasks;

namespace OnlineFPS.UI.PlayerStats
{
    public class PlayerStatsUI : MonoBehaviour, IService
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private RespawnPanelUI _respawnPanel;

        public async UniTask ChangeHealth(int maxHealth, int currentHealth)
        {
            var amount = Mathf.InverseLerp(0, maxHealth, currentHealth);

            _healthBar.fillAmount = amount;

            if (currentHealth <= 0)
            {
                await _respawnPanel.StartRespawnPanelAnimation();
                ChangeHealth(maxHealth, maxHealth);
            }
        }        
    }
}