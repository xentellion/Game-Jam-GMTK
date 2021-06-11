using UnityEngine;

public class CamerBorders : MonoBehaviour
{
    //hamdles to set up room borders
    [HideInInspector] public Vector3[] handles = new Vector3[2] { new Vector3(1,1), new Vector3(-1,-1)};
    //borders for camera
    Vector3[] corners = new Vector3[2];
    //camera
    Camera camera;

    private void Start()
    {
        //set up borders for camera
        camera = Camera.main;
        float height = 2f * camera.orthographicSize;
        float width = height * camera.aspect;
        corners[0] = handles[0] - new Vector3(width, height) / 2f;
        corners[1] = handles[1] + new Vector3(width, height) / 2f;
    }

    private void OnLevelWasLoaded(int level)
    {
        camera = Camera.main;
    }

    private void LateUpdate()
    {
        //Keep camera inside borders
        var camPos = camera.transform.position;
        var newPos = new Vector3(
            Mathf.Clamp(camPos.x, corners[1].x, corners[0].x), 
            Mathf.Clamp(camPos.y, corners[1].y, corners[0].y));
        camera.transform.position = newPos + new Vector3(0, 0, -10);
    }
}
