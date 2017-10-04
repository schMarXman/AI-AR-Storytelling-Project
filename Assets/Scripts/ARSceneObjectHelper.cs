using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSceneObjectHelper : MonoBehaviour
{
    public static ARSceneObjectHelper Instance;

    public GameObject DataParticles;

    void Awake()
    {
        Instance = this;
    }


}
