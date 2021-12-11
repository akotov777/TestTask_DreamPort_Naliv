using UnityEngine;


public static class Math
{
    #region Methods

    public static Vector3 FindRayPlaneIntersection(Ray ray, Vector3 normalPosition, Vector3 normalVector)
    {
        float d = Vector3.Dot(normalVector, ray.direction);
        float e = Vector3.Dot(normalVector, normalPosition - ray.origin);

        float t = e / d;

        Vector3 hitPos = ray.origin + ray.direction * t;
        return hitPos;
    }

    public static bool TryFindRayPlaneIntersection(Ray ray, Vector3 normalPosition, Vector3 normalVector, out Vector3 intersection)
    {
        float d = Vector3.Dot(normalVector, ray.direction);
        float e = Vector3.Dot(normalVector, normalPosition - ray.origin);

        float t = e / d;

        intersection = ray.origin + ray.direction * t;
        if (t >= 0)
            return true;
        return false;
    }

    #endregion
}