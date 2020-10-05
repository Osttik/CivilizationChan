﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopedWorld : MonoBehaviour
{
    [SerializeField]
    private Transform _leftEndPoint;
    [SerializeField]
    private Transform _rightEndPoint;
    [SerializeField]
    private Transform _upEndPoint;
    [SerializeField]
    private Transform _downEndPoint;
    [SerializeField]
    private float _worldWidth = -1;
    [SerializeField]
    private float _worldHeight = -1;
    [SerializeField]
    private float _screenHeight = -1;
    [SerializeField]
    private float _screenWidth = -1;
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Camera _secondaryCameraX;
    [SerializeField]
    private Camera _secondaryCameraY;

    public Transform UpEndPoint { get { return _upEndPoint; } set { _upEndPoint = value; } }
    public Transform DownEndPoint { get { return _downEndPoint; } set { _downEndPoint = value; } }
    public Transform LeftEndPoint { get { return _leftEndPoint; } set { _leftEndPoint = value; } }
    public Transform RightEndPoint { get { return _rightEndPoint; } set { _rightEndPoint = value; } }
    public float WorldWidth { get { return _worldWidth; } set { _worldWidth = value; } }
    public float WorldHeight { get { return _worldHeight; } set { _worldHeight = value; } }
    public float ScreenWidth { get { return _screenWidth; } set { _screenWidth = value; } }
    public float ScreenHeight { get { return _screenHeight; } set { _screenHeight = value; } }
    public Camera SecondaryCameraX { get { return _secondaryCameraX; } set { _secondaryCameraX = value; } }
    public Camera SecondaryCameraY { get { return _secondaryCameraY; } set { _secondaryCameraY = value; } }
    public Camera MainCamera { get { return _mainCamera; } set { _mainCamera = value; } }

    private void Start()
    {
        if (MainCamera == null)
            MainCamera = Camera.main;
        if (SecondaryCameraX == null)
        {
            SecondaryCameraX = GetComponent<Camera>();
            SecondaryCameraX.transform.parent = MainCamera.transform;
            SecondaryCameraX.clearFlags = CameraClearFlags.Nothing;
        }
        if (WorldWidth < 0)
            WorldWidth = RightEndPoint.position.x - LeftEndPoint.position.x;
        if (WorldHeight < 0)
            WorldWidth = UpEndPoint.position.y - DownEndPoint.position.y;
        if (ScreenWidth < 0)
        {
            var p = MainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, -MainCamera.transform.position.z));
            ScreenWidth = (MainCamera.transform.position - p).x * 2;
        }
        if (ScreenHeight < 0)
        {
            var p = MainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, -MainCamera.transform.position.z));
            ScreenHeight = (MainCamera.transform.position - p).y * 2;
        }
    }

    private void LateUpdate()
    {
        float worldMiddle = (RightEndPoint.transform.position.x + LeftEndPoint.transform.position.x) / 2;
        float mainCameraX = MainCamera.transform.position.x;
        float secondaryCameraX = SecondaryCameraX.transform.position.x;
        float maxX = Mathf.Max(mainCameraX, secondaryCameraX);
        float minX = Mathf.Min(mainCameraX, secondaryCameraX);

        if (!(maxX >= worldMiddle && minX <= worldMiddle))
        {
            print($"{maxX} >= {worldMiddle} && {minX} <= {worldMiddle}");
            SwapPositions(_mainCamera, _secondaryCameraX);
        }

        var diametr = MainCamera.transform.position.x - LeftEndPoint.position.x;
        if (diametr < ScreenWidth)
        {
            SecondaryCameraX.enabled = true;
            SecondaryCameraX.transform.localPosition = Vector3.right * WorldWidth;
        }
        else if (diametr > WorldWidth - ScreenWidth)
        {
            SecondaryCameraX.enabled = true;
            SecondaryCameraX.transform.localPosition = Vector3.left * WorldWidth;
        }
        else
            SecondaryCameraX.enabled = false;
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
        main.transform.position = SecondaryCameraX.transform.position;
        print(main.ToString() + " || " + second.ToString());
        print("To");
        //Vector3 tempPosition = first.transform.position;
        //first.transform.position = second.transform.position;
        //second.transform.position = tempPosition;
        //print($"({second.transform.position.x}, {second.transform.position.y}, {second.transform.position.z}) -> ({first.transform.position.x}, {first.transform.position.y}, {first.transform.position.z})");
    }
}
