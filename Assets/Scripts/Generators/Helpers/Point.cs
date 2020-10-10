using System;
using UnityEngine;

namespace Assets.Scripts.Generators.Helpers
{
    public class Point
    {
        public Vector3 Position { get; set; }
        public Vector3 LookToBySide { get; set; }

        public Point(Vector3 position)
        {
            Position = position;
            LookToBySide = Vector3.zero;
        }

        public Point(Vector3 position, Vector3 lookToBySide): this(position)
        {
            LookToBySide = lookToBySide;
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Position.Equals(obj);
        }
    }
}
