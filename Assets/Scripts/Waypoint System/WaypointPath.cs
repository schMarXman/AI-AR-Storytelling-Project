using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public bool Loop;

    private List<Waypoint> mWaypoints = new List<Waypoint>();

    void Awake()
    {
        UpdateAllWaypoints();
    }

    [ContextMenu("Force update all waypoints")]
    public void UpdateAllWaypoints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<Waypoint>();
            mWaypoints.Add(child);

            if (child != null)
            {
                child.Setup();
            }
        }
    }

    public int GetWaypointCount()
    {
        return mWaypoints.Count;
    }

    public Waypoint GetWaypoint(int i)
    {
        if (i >= 0 && i < mWaypoints.Count)
        {
            return mWaypoints[i];
        }

        return null;
    }

    private void OnTransformChildrenChanged()
    {
        UpdateAllWaypoints();
    }


    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<Waypoint>();

            if (child != null && child.Successor != null)
            {
                Gizmos.DrawLine(child.transform.position, child.Successor.transform.position);
            }
        }
    }
}
