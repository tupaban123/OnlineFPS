using OnlineFPS.CodeBase;
using OnlineFPS.Configs;

namespace OnlineFPS.AssetsManagment
{
    public interface IAssetProvider : IService
    {
        public GunConfig GetGunConfigByID(string id);

        public PlayerConfig GetPlayerConfig();
    }
}