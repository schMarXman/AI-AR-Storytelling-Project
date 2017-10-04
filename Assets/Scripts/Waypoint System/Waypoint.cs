using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    private WaypointPath mParent;

    public Waypoint Predecessor { get; private set; }
    public Waypoint Successor { get; private set; }


    public void Setup()
    {
        mParent = transform.parent.GetComponent<WaypointPath>();

        Predecessor = null;
        Successor = null;

        if (mParent.Loop)
        {
            if (transform.GetSiblingIndex() == 0)
            {
                Predecessor = mParent.transform.GetChild(transform.parent.childCount - 1).GetComponent<Waypoint>();
            }
            else if (transform.GetSiblingIndex() == mParent.transform.childCount - 1)
            {
                Successor = mParent.transform.GetChild(0).GetComponent<Waypoint>();
            }
        }

        if (Predecessor == null)
        {
            int index = transform.GetSiblingIndex() - 1;

            if (index >= 0)
            {
                Predecessor = mParent.transform.GetChild(index).GetComponent<Waypoint>();
            }
        }

        if (Successor == null)
        {
            int index = transform.GetSiblingIndex() + 1;

            if (index < mParent.transform.childCount)
            {
                Successor = mParent.transform.GetChild(index).GetComponent<Waypoint>();
            }
        }
    }


}
