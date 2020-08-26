using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopedWorld : MonoBehaviour
{
    [SerializeField]
    private Transform _leftEndPoint;
    [SerializeField]
    private Transform _rightEndPoint;
    [SerializeField]
    private float _worldWidth = -1;
    [SerializeField]
    private float _screenSize = -1;
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Camera _secondaryCamera;

    public Transform LeftEndPoint { get { return _leftEndPoint; } set { _leftEndPoint = value; } }
    public Transform RightEndPoint { get { return _rightEndPoint; } set { _rightEndPoint = value; } }
    public float WorldWidth { get { return _worldWidth; } set { _worldWidth = value; } }
    public float ScreenSize { get { return _screenSize; } set { _screenSize = value; } }
    public Camera SecondaryCamera { get { return _secondaryCamera; } set { _secondaryCamera = value; } }
    public Camera MainCamera { get { return _mainCamera; } set { _mainCamera = value; } }
    private void Start()
    {
        if (MainCamera == null)
            MainCamera = Camera.main;
        if (SecondaryCamera == null)
        {
            SecondaryCamera = GetComponent<Camera>();
            SecondaryCamera.transform.parent = MainCamera.transform;
            SecondaryCamera.clearFlags = CameraClearFlags.Nothing;
        }
        if (WorldWidth < 0)
            WorldWidth = RightEndPoint.position.x - LeftEndPoint.position.x;
        if (ScreenSize < 0)
        {
            var p = MainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, -MainCamera.transform.position.z));
            ScreenSize = (MainCamera.transform.position - p).x * 2;
        }
    }

    private void LateUpdate()
    {
        float worldMiddle = (RightEndPoint.transform.position.x + LeftEndPoint.transform.position.x) / 2;
        float mainCameraX = MainCamera.transform.position.x;
        float secondaryCameraX = SecondaryCamera.transform.position.x;
        float maxX = Mathf.Max(mainCameraX, secondaryCameraX);
        float minX = Mathf.Min(mainCameraX, secondaryCameraX);

        if (!(maxX >= worldMiddle && minX <= worldMiddle))
        {
            print($"{maxX} >= {worldMiddle} && {minX} <= {worldMiddle}");
            SwapPositions(_mainCamera, _secondaryCamera);
        }

        var diametr = MainCamera.transform.position.x - LeftEndPoint.position.x;
        if (diametr < ScreenSize)
        {
            SecondaryCamera.enabled = true;
            SecondaryCamera.transform.localPosition = Vector3.right * WorldWidth;
        }
        else if (diametr > WorldWidth - ScreenSize)
        {
            SecondaryCamera.enabled = true;
            SecondaryCamera.transform.localPosition = Vector3.left * WorldWidth;
        }
        else
            SecondaryCamera.enabled = false;
    }

    private void SwapPositions(Camera main, Camera second)
    {
        print("From");
        print(main.ToString() + " || " + second.ToString());
        if (main.transform.position.x > second.transform.position.x)
        {
            second.transform.localPosition = Vector3.left * WorldWidth;
        }
        else
        {
            second.transform.localPosition = Vector3.right * WorldWidth;
        }
        main.transform.position = SecondaryCamera.transform.position;
        print(main.ToString() + " || " + second.ToString());
        print("To");
        //Vector3 tempPosition = first.transform.position;
        //first.transform.position = second.transform.position;
        //second.transform.position = tempPosition;
        //print($"({second.transform.position.x}, {second.transform.position.y}, {second.transform.position.z}) -> ({first.transform.position.x}, {first.transform.position.y}, {first.transform.position.z})");
    }
}
