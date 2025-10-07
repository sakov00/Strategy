using UnityEngine;

namespace _General.Scripts.Extentions
{
    public static class PositionExtention
    {
        public static float GetDistanceBetweenObjects(Transform a, Transform b)
        {
            var dir = (b.position - a.position).normalized;
            var aExtent = Vector3.Dot(a.localScale / 2f, AbsVector(dir));
            var bExtent = Vector3.Dot(b.localScale / 2f, AbsVector(-dir));

            var centerDistance = Vector3.Distance(a.position, b.position);
            var edgeDistance = centerDistance - aExtent - bExtent;

            return Mathf.Max(0f, edgeDistance);
        }
        
        public static float GetDistanceBetweenObjects(Transform a, Transform b, out Vector3 pointA, out Vector3 pointB)
        {
            var dir = (b.position - a.position).normalized;

            var aExtent = Vector3.Dot(a.localScale / 2f, AbsVector(dir));
            var bExtent = Vector3.Dot(b.localScale / 2f, AbsVector(-dir));

            var centerDistance = Vector3.Distance(a.position, b.position);

            pointA = a.position + dir * aExtent;
            pointB = b.position - dir * bExtent;

            var edgeDistance = centerDistance - aExtent - bExtent;

            return Mathf.Max(0f, edgeDistance);
        }
        
        public static Vector3 AbsVector(Vector3 v) => new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }
}