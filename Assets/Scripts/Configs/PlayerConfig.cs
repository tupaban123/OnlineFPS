using UnityEngine;

namespace OnlineFPS.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float SlopeRayLength { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; }
    } 
}