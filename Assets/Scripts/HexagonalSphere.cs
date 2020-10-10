using Assets.Scripts.Enums;
using Assets.Scripts.Generators.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexagonalSphere : MonoBehaviour
{
    [SerializeField]
    private GameObject _hexagonalTile;
    public GameObject HexagonalTile { get { return _hexagonalTile; } set { _hexagonalTile = value; } }

    [SerializeField]
    private Vector3 _additionHexRotation = Vector3.zero;
    public Vector3 AdditionalHexRotation { get { return _additionHexRotation; } set { _additionHexRotation = value; } }

    [SerializeField]
    private GameObject _pentagonalTile;
    public GameObject PentagonalTile { get { return _pentagonalTile; } set { _pentagonalTile = value; } }

    [SerializeField]
    private Vector3 _additionPenRotation = Vector3.zero;
    public Vector3 AdditionalPenRotation { get { return _additionPenRotation; } set { _additionPenRotation = value; } }

    private List<Face> _faces = new List<Face>();
    private readonly int _numberOfSectors = 5;
    private float _tau = 1.61803399f;
    private const int _numberOfPentagons = 12;
    private HashSet<Point> _points;

    private void Start()
    {
        GenerateSphere();
    }

    private void GenerateSphere()
    {
        int centerCircleNumber = 25;
        float r = _hexagonalTile.GetComponent<MeshRenderer>().bounds.size.x / (2 * Mathf.Tan(Mathf.PI / ((centerCircleNumber + 1) * _numberOfSectors)));

        _faces = AddFaces(r);
        _points = new HashSet<Point>();
        foreach (Face face in _faces)
        {
            foreach (Point point in face.SubdivideBy(centerCircleNumber + 1, r))
            {
                _points.Add(point);
            }
        }

        foreach (Point point in _points)
        {
            LookAt(point.LookToBySide, Instantiate(HexagonalTile, point.Position, new Quaternion(), transform), AdditionalHexRotation);
        }

        print(_points.Count);
        print($"Radius {r}");
    }

    private List<Face> AddFaces(float r)
    {
        Vector3[] vertices = new Vector3[_numberOfPentagons];
        vertices[0] = new Vector3(1, _tau * 1, 0);
        vertices[1] = new Vector3(-1, _tau * 1, 0);
        vertices[2] = new Vector3(1, -_tau * 1, 0);
        vertices[3] = new Vector3(-1, -_tau * 1, 0);
        vertices[4] = new Vector3(0, 1, _tau * 1);
        vertices[5] = new Vector3(0, -1, _tau * 1);
        vertices[6] = new Vector3(0, 1, -_tau * 1);
        vertices[7] = new Vector3(0, -1, -_tau * 1);
        vertices[8] = new Vector3(_tau * 1, 0, 1);
        vertices[9] = new Vector3(-_tau * 1, 0, 1);
        vertices[10] = new Vector3(_tau * 1, 0, -1);
        vertices[11] = new Vector3(-_tau * 1, 0, -1);

        for (int i = 0; i < vertices.Length; i++)
        {
            LookAtCenter(Instantiate(PentagonalTile, vertices[i] * Face.CorrectToRadius(r, vertices[i], transform.position), new Quaternion(), transform), AdditionalPenRotation);
        }

        List<Face> faces = new List<Face>()
        {
            new Face(points: new Vector3[]{ vertices[0], vertices[1], vertices[4] }),
            new Face(points: new Vector3[]{ vertices[1], vertices[9], vertices[4] }),
            new Face(points: new Vector3[]{ vertices[4], vertices[9], vertices[5] }),
            new Face(points: new Vector3[]{ vertices[5], vertices[9], vertices[3] }),
            new Face(points: new Vector3[]{ vertices[2], vertices[3], vertices[7] }),
            new Face(points: new Vector3[]{ vertices[3], vertices[2], vertices[5] }),
            new Face(points: new Vector3[]{ vertices[7], vertices[10], vertices[2] }),
            new Face(points: new Vector3[]{ vertices[0], vertices[8], vertices[10] }),
            new Face(points: new Vector3[]{ vertices[0], vertices[4], vertices[8] }),
            new Face(points: new Vector3[]{ vertices[8], vertices[2], vertices[10] }),
            new Face(points: new Vector3[]{ vertices[8], vertices[4], vertices[5] }),
            new Face(points: new Vector3[]{ vertices[8], vertices[5], vertices[2] }),
            new Face(points: new Vector3[]{ vertices[1], vertices[0], vertices[6] }),
            new Face(points: new Vector3[]{ vertices[11], vertices[1], vertices[6] }),
            new Face(points: new Vector3[]{ vertices[3], vertices[9], vertices[11] }),
            new Face(points: new Vector3[]{ vertices[6], vertices[10], vertices[7] }),
            new Face(points: new Vector3[]{ vertices[3], vertices[11], vertices[7] }),
            new Face(points: new Vector3[]{ vertices[11], vertices[6], vertices[7] }),
            new Face(points: new Vector3[]{ vertices[6], vertices[0], vertices[10] }),
            new Face(points: new Vector3[]{ vertices[9], vertices[1], vertices[11] })
        };

        return faces;
    }

    private Vector3 AddLineOfTile(GameObject tile, Vector3 additionalRotation, float step, Vector3 startPosition, int numberOfTile)
    {
        Vector3 lastPosition = Vector3.zero;
        for (int i = 0; i < numberOfTile; i++)
        {
            lastPosition = Quaternion.AngleAxis(i * step, Vector3.up) * startPosition;
            GameObject createdTile = Instantiate(tile, lastPosition, new Quaternion(), transform);

            LookAtCenter(createdTile, additionalRotation);
        }

        return lastPosition;
    }

    private void LookAtCenter(GameObject obj, Vector3 additionalRotation)
    {
        obj.transform.LookAt(transform);
        obj.transform.localRotation *= Quaternion.Euler(additionalRotation);
    }

    private void LookAt(Vector3 secondary, GameObject obj, Vector3 additionalRotation)
    {
        obj.transform.LookAt(transform);
        obj.transform.rotation *= Quaternion.Euler(additionalRotation);
        Vector3 currentUp = Vector3.Cross(secondary, obj.transform.position);
        Vector3 lookTo = Vector3.Cross(currentUp, obj.transform.position);

        obj.transform.LookAt(lookTo + obj.transform.position, obj.transform.up);
        obj.transform.rotation *= Quaternion.Euler(30f * Vector3.up);
    }
}