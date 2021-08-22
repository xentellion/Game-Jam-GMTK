using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] Vector2 parallaxMultiplier;
    Transform cameraTransform;
    Vector3 lastCamPos;
    CamerBorders cb;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCamPos = cameraTransform.position;
        cb = cameraTransform.GetComponent<CamerBorders>();
    }

    private void LateUpdate()
    {
        if (cameraTransform.position.x < cb.corners[0].x && cameraTransform.position.x > cb.corners[1].x)
            if (cameraTransform.position.x < cb.corners[0].x && cameraTransform.position.x > cb.corners[1].x) 
            {
                Vector3 delta = cameraTransform.position - lastCamPos;

                transform.position += new Vector3(delta.x * parallaxMultiplier.x, delta.y * parallaxMultiplier.y);
                lastCamPos = cameraTransform.position;
            }
    }
}
