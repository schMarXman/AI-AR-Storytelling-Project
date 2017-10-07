using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public static ObjectSelector Instance;

    private bool mEnabled = true;

    public Camera AttachedCamera { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        AttachedCamera = GetComponent<Camera>();

        if (AttachedCamera == null)
        {
            AttachedCamera = GetComponentInChildren<Camera>();
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
        bool hit = Physics.Raycast(AttachedCamera.ScreenPointToRay(screenPositon), out hitInfo);

        if (hit && hitInfo.transform.GetComponent<SelectableObject>())
        {
            Debug.Log("Object hit");
            hitInfo.transform.GetComponent<SelectableObject>().Select();
        }
    }
}
