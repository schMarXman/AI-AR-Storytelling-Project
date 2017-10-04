using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    private bool mEnabled = true;

    private Camera mCam;

    void Start()
    {
        mCam = GetComponent<Camera>();

        if (mCam==null)
        {
            mCam = GetComponentInChildren<Camera>();
        }
    }

    void Update()
    {
        if (mEnabled)
        {
            if (Application.isMobilePlatform)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Raycast(Input.GetTouch(0).position);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Raycast(Input.mousePosition);
                }
            } 
        }
    }

    void Raycast(Vector3 screenPositon)
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(mCam.ScreenPointToRay(screenPositon), out hitInfo);

        var selectable = hitInfo.transform.GetComponent<SelectableObject>();
        if (hit &&  selectable != null)
        {
            Debug.Log("Object hit");
            selectable.Select();
        }
    }
}
