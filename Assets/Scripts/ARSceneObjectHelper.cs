using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSceneObjectHelper : MonoBehaviour
{
    public static ARSceneObjectHelper Instance;

    public GameObject DataParticles;
    public GameObject TimeSlider;

    void Awake()
    {
        Instance = this;
    }

    public void SliderUsed()
    {
        GameEvent.EndEventById("WaitForSliderUsed", true);
    }
}
