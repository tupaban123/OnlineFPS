using OnlineFPS.Configs;
using UnityEngine;

namespace OnlineFPS.AssetsManagment
{
    public class AssetProvider : IAssetProvider
    {
        public GunConfig GetGunConfigByID(string id)
        {
            var path = AssetsPath.ConfigsPath + AssetsPath.GunsConfigsAssemblyPath;
            var gunsConfigs = Resources.Load<GunsConfigsAssembly>(path);
            var configItem = gunsConfigs.GetConfigByID(id);
            return configItem;
        }

        public PlayerConfig GetPlayerConfig()
        {
            return Resources.Load<PlayerConfig>(AssetsPath.ConfigsPath + AssetsPath.PlayerConfigPath);
        }
    }
}