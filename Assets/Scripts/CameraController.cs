using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    [SerializeField] private float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (Target == null) return;
        Vector3 getPos = Target.position;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, getPos, smoothSpeed);
        transform.position = smoothPosition;
    }

}
