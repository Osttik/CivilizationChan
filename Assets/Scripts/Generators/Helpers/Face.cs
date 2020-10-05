using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Face
{
    public List<Vector3> Points { get; set; }
    public Vector3 Center { get; set; }

    public Face(params Vector3[] points)
    {
        Center = Vector3.zero;
        Points = points.ToList();
    }

    public Face(Vector3 center, params Vector3[] points): this(points)
    {
        Center = center;
    }

    public List<Vector3> SubdivideBy(int numberOfSlices = 2, float r = 1, bool withMain = false)
    {
        List<Vector3> slicedPoints = new List<Vector3>();

        List<Vector3> firstPoints = DivideLineBy(numberOfSlices, Points[0], Points[1]);
        List<Vector3> secondPoints = DivideLineBy(numberOfSlices, Points[1], Points[2]);
        List<Vector3> thirdPoints = DivideLineBy(numberOfSlices, Points[0], Points[2]);
        slicedPoints.AddRange(firstPoints);
        slicedPoints.AddRange(secondPoints);
        slicedPoints.AddRange(thirdPoints);
        
        for (int i = numberOfSlices - 1; i > 0; i--)
        {
            slicedPoints.AddRange(DivideLineBy(i, firstPoints[i - 1], thirdPoints[i - 1]));
        }

        if (withMain)
        {
            slicedPoints.AddRange(Points);
        }

        for (int i = 0; i < slicedPoints.Count; i++)
        {
            slicedPoints[i] *= CorrectToRadius(r, slicedPoints[i], Center);
        }

        return slicedPoints;
    }

    private List<Vector3> DivideLineBy(float nuberOfSlices, Vector3 point1, Vector3 point2)
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 1; i < nuberOfSlices; i++)
        {
            float currentPercentage = i / nuberOfSlices;

            Vector3 point = new Vector3(
                point1.x * (1 - currentPercentage) + point2.x * currentPercentage,
                point1.y * (1 - currentPercentage) + point2.y * currentPercentage,
                point1.z * (1 - currentPercentage) + point2.z * currentPercentage
                );

            points.Add(point);
        }

        return points;
    }

    public static float CorrectToRadius(float sphereRadius, Vector3 p, Vector3 center)
    {
        float currentDistance = Mathf.Sqrt(Mathf.Pow(center.x - p.x, 2) + Mathf.Pow(center.y - p.y, 2) + Mathf.Pow(center.z - p.z, 2));
        float adjustment = sphereRadius / currentDistance;
        return adjustment;
    }

    public void FixRadius(float radius)
    {
        Points[0] *= CorrectToRadius(radius, Points[0], Center);
        Points[1] *= CorrectToRadius(radius, Points[1], Center);
        Points[2] *= CorrectToRadius(radius, Points[2], Center);
    }
}
