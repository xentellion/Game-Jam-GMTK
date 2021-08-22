using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    Transform target;
    [SerializeField] [Range(0.01f,1f)] float smoothSpeed = 0.25f;
    [SerializeField] Vector3 offset = new Vector3(0, 1, -10);
    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothSpeed);
    }
}
