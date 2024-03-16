using UnityEngine;

namespace OnlineFPS.Configs
{
    [CreateAssetMenu(fileName = "GunsConfigsAssembly", menuName = "Configs/GunsConfigsAssembly")]
    public class GunsConfigsAssembly : ScriptableObject
    {
        [field: SerializeField] private GunConfig[] gunsConfigs;   
        
        public GunConfig GetConfigByID(string id)
        {
            foreach (var gunConfig in gunsConfigs)
                if (gunConfig.GunID == id)
                    return gunConfig;

            return null;
        }
    }
}