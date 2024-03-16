using UnityEngine;
using OnlineFPS.GunBase;

namespace OnlineFPS.Configs
{
    [CreateAssetMenu(fileName = "GunConfig", menuName = "Configs/GunConfig")]
    public class GunConfig : ScriptableObject
    {
        [field: SerializeField] public string GunName { get; private set; }
        [field: SerializeField] public string GunID { get; private set; }
        [field: SerializeField] public int MaxAmmo { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float ReloadingTime { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; }
        [field: SerializeField] public GunType GunType { get; private set; }
    }
}