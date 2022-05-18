using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    void Update()
    {
        transform.position = target.position + offset;
    }
}
