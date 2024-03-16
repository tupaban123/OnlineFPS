using UnityEngine;

namespace OnlineFPS.CodeBase
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private BootstrapperMain bootstrapperMainPrefab;

        private void Awake()
        {
            var mainBootstrapper = FindObjectOfType<BootstrapperMain>();

            if (mainBootstrapper == null)
                Instantiate(bootstrapperMainPrefab);
        }
    }
}
