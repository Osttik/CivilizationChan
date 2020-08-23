using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalMapCoordinates
{
    public Vector3 Start { get; private set; }
    public float Step { get; private set; }
    public float HexWidht { get; private set; }
    public float HexHeight { get; private set; }

    private float _offset;

    public HexagonalMapCoordinates(Vector3 start, float step = 1f, float hexH = 1f, float hexW = 1.547f)
    {
        Start = start;
        Step = step;
        HexWidht = hexW;
        HexHeight = hexH;
        _offset = HexHeight / 2;
    }

    public (float, float) this[float x, float y]
    {
        get
        {
            return (GetX(x), GetY(x, y));
        }
    }

    public float GetX(float x)
    {
        float hexagonalX = Mathf.Abs(Start.x - x) * (HexWidht * 3 / 4);

        return hexagonalX;
    }

    public float GetY(float x, float y)
    {
        float hexagonalY = Mathf.Abs(Start.y - y);

        if (Mathf.Abs(Start.x - x) / Step % 2 == 1)
        {
            hexagonalY -= x - x + _offset;
        }

        return hexagonalY;
    }
}