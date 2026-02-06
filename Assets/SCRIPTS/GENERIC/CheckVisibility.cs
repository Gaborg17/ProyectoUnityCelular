using UnityEngine;
using System;
using System.Linq;

public class CheckVisibility : MonoBehaviour
{
    [SerializeField] private float visibleDistance;
    public bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return planes[2].GetDistanceToPoint(transform.position + new Vector3(0,visibleDistance,0)) >= 0;
    }


    public bool InFrame()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return planes[3].GetDistanceToPoint(transform.position + new Vector3(0, 0, 0)) >= 0;
    }
}
