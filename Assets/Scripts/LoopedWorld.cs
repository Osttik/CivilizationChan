using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopedWorld : MonoBehaviour
{
    public Transform LeftEndPoint;
    public Transform RightEndPoint;
    public float WorldWidth = -1;
    public float ScreenSize = -1;
    public Camera secCam;
    public Camera mainCam;
    private void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;
        if (secCam == null)
        {
            secCam = GetComponent<Camera>();
            secCam.transform.parent = mainCam.transform;
            secCam.clearFlags = CameraClearFlags.Nothing;
        }
        if (WorldWidth < 0)
            WorldWidth = RightEndPoint.position.x - LeftEndPoint.position.x;
        if (ScreenSize < 0)
        {
            var p = mainCam.ViewportToWorldPoint(new Vector3(0, 0.5f, -mainCam.transform.position.z));
            ScreenSize = (mainCam.transform.position - p).x * 2;
        }

    }
    private void LateUpdate()
    {
        var d = mainCam.transform.position.x - LeftEndPoint.position.x;
        if (d < ScreenSize)
        {
            secCam.enabled = true;
            secCam.transform.localPosition = Vector3.right * WorldWidth;
        }
        else if (d > WorldWidth - ScreenSize)
        {
            secCam.enabled = true;
            secCam.transform.localPosition = -Vector3.right * WorldWidth;
        }
        else
            secCam.enabled = false;
    }
}
