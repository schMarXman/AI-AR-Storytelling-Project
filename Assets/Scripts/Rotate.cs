using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Angle;


    void Update()
    {
        transform.Rotate(Vector3.up, Angle);
    }
}
