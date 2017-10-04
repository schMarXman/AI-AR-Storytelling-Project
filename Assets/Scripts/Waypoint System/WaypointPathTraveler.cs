using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPathTraveler : MonoBehaviour
{
    public WaypointPath Path;

    public bool UseX = true, UseY = true, UseZ = true, LaunchOnStart;

    public float MovementSpeed, RotationSpeed;

    public AnimationCurve MovementCurve;

    public static List<WaypointPathTraveler> AllTravelers = new List<WaypointPathTraveler>();

    void Start()
    {
        AllTravelers.Add(this);

        if (LaunchOnStart)
        {
            Launch();
        }
    }

    public static void LaunchAll()
    {
        for (int i = 0; i < AllTravelers.Count; i++)
        {
            AllTravelers[i].Launch();
        }
    }

    public void Launch()
    {
        var start = Path.GetWaypoint(0).transform.position;

        if (!UseX)
        {
            start.x = transform.position.x;
        }

        if (!UseY)
        {
            start.y = transform.position.y;
        }

        if (!UseZ)
        {
            start.z = transform.position.z;
        }

        transform.position = start;

        StartCoroutine(MoveAlongPath());
    }

    IEnumerator MoveAlongPath()
    {
        int count = Path.GetWaypointCount();

        for (int i = 1; i < count; i++)
        {
            var start = transform.position;
            var waypoint = Path.GetWaypoint(i);
            var target = waypoint.transform.position;

            var lookPosition = waypoint.transform.position;

            if (!UseX)
            {
                lookPosition.x = start.x;
                target.x = start.x;
            }

            if (!UseY)
            {
                lookPosition.y = start.y;
                target.y = start.y;
            }

            if (!UseZ)
            {
                lookPosition.z = start.z;
                target.z = start.z;
            }

            transform.LookAt(lookPosition);

            float time = 0;

            while (transform.position != target)
            {
                time += Time.deltaTime;
                //transform.position = Vector3.Lerp(start, target, MovementCurve.Evaluate(time * MovementSpeed));
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * MovementSpeed);

                //var distance = Vector3.Distance(transform.position, target);
                //bool rotate = false;
                //Quaternion targetRot = new Quaternion();
                //if (distance <= 0.1f)
                //{
                //    rotate = true;
                //}

                //if (rotate)
                //{
                //    targetRot = Quaternion.LookRotation((waypoint.Successor.transform.position - transform.position)
                //            .normalized);
                //    transform.rotation = Quaternion.Slerp(transform.rotation,
                //        targetRot, time * RotationSpeed);
                //}

                //if (transform.rotation == targetRot)
                //{
                //    rotate = false;
                //}

                yield return null;
            }

            //transform.LookAt(waypoint.Successor.transform);

            if (Path.Loop && i == count - 1)
            {
                i = -1;
            }
        }
    }
}
