using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;

    public void Initialize(Transform cameraPosition) => this.cameraPosition = cameraPosition;

    private void Update()
    {
        if(cameraPosition == null)
            return;  

        transform.position = cameraPosition.position;
    }
}
