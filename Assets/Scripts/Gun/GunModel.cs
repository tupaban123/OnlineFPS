using OnlineFPS.CodeBase;
using OnlineFPS.Configs;
using System.Collections;
using UnityEngine;

namespace OnlineFPS.GunBase
{
    public class GunModel
    {
        private GunView _gunView;
        private GunConfig _gunConfig;

        private int _currentAmmo;

        private bool _canFire = true;
        private bool _isReloading = false;

        private ICoroutineRunner _coroutineRunner;

        public bool IsReloading => _isReloading;

        public GunModel(GunView gunView, GunConfig gunConfig, ICoroutineRunner coroutineRunner)
        {
            _gunView = gunView;
            _gunConfig = gunConfig;

            _coroutineRunner = coroutineRunner;

            _currentAmmo = gunConfig.MaxAmmo;
        }

        public void OnFire()
        {
            if (!_canFire)
                return;

            if (_isReloading)
                return;

            _currentAmmo--;

            if (_currentAmmo <= 0)
                _coroutineRunner.StartCoroutine(Reloading());
            

            Debug.Log($"fire! ammo: {_currentAmmo}/{_gunConfig.MaxAmmo}");
            _gunView.CmdFire();
            _coroutineRunner.StartCoroutine(TogglingShootingAbility());
        }

        public IEnumerator Reloading()
        {
            if (_isReloading || _currentAmmo == _gunConfig.MaxAmmo)
                yield break;

            Debug.Log("reloading...");
            _isReloading = true;
            yield return new WaitForSeconds(_gunConfig.ReloadingTime);
            _currentAmmo = _gunConfig.MaxAmmo;
            _isReloading = false;

            Debug.Log($"reloading ended. ammo: {_currentAmmo}/{_gunConfig.MaxAmmo}");
        }

        private IEnumerator TogglingShootingAbility()
        {
            if (_canFire == false)
                yield break;

            _canFire = false;
            yield return new WaitForSeconds(_gunConfig.FireRate);
            _canFire = true;
        }
    }
}