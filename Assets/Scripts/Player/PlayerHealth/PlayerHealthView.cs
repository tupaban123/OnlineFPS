using Cysharp.Threading.Tasks;
using Mirror;
using OnlineFPS.CodeBase;
using OnlineFPS.UI.PlayerStats;
using UnityEngine;

namespace OnlineFPS.Player.Health
{
    public class PlayerHealthView : NetworkBehaviour, IDamagable
    {
        [SyncVar (hook = nameof(OnHealthChange))]
        private int _currentHealth;

        private int _maxHealth;

        private PlayerStatsUI _playerStatsUI;
        private PlayerView _playerView;

        public void Initialize(int maxHealth, PlayerView playerView)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
            _playerView = playerView;

            _playerStatsUI = ServiceLocator.Instance.Get<PlayerStatsUI>();
        }

        private void OnHealthChange(int oldValue, int newValue)
        {
            if(_playerStatsUI && isLocalPlayer)
                _playerStatsUI.ChangeHealth(_maxHealth, _currentHealth);
        }

        [Command(requiresAuthority = false)]
        public void Damage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
                Die();

            Debug.Log($"{transform.root.name} damaged. current health: {_currentHealth}");
        }

        [ClientRpc]
        private void Die()
        {
            Debug.Log($"player {transform.root.name} died");

            _playerView.EnableCharacter(false);
            RespawnDelay();
        }

        private async UniTask RespawnDelay()
        {
            await UniTask.WaitForSeconds(3);

            Respawn();
        }

        private void Respawn()
        {
            Debug.Log($"player {transform.root.name} respawned");

            _playerView.EnableCharacter(true);
            Heal();
        }

        private void Heal() => _currentHealth = _maxHealth;
    }
}