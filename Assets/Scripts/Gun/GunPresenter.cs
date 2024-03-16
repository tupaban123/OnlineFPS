using OnlineFPS.CodeBase;

namespace OnlineFPS.GunBase
{
    public class GunPresenter
    {
        private GunModel _gunModel;

        private ICoroutineRunner _coroutineRunner;

        public GunPresenter(GunModel gunModel, ICoroutineRunner coroutineRunner)
        { 
            _gunModel = gunModel;
            _coroutineRunner = coroutineRunner;
        }

        public void OnFire() => _gunModel.OnFire();

        public void StartReloading() => _coroutineRunner.StartCoroutine(_gunModel.Reloading());
    }
}