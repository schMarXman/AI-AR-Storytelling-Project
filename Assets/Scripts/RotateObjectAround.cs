using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectAround : MonoBehaviour
{
    public Transform Target;
    public float MovementSpeed;

    void Start()
    {
        StartCoroutine(RotateAroundRoutine());
    }

    IEnumerator RotateAroundRoutine()
    {
        while (true)
        {
            transform.RotateAround(Target.transform.position, Vector3.right, MovementSpeed * Time.deltaTime);
            yield return null; 
        }
    }

}
