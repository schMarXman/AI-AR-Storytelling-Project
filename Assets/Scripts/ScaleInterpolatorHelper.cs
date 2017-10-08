using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleInterpolatorHelper : MonoBehaviour
{
    public void TriggerSpecificInterpolator(int index)
    {
        GetComponents<ScaleInterpolator>()[index].LaunchAnimation();
    }
}
