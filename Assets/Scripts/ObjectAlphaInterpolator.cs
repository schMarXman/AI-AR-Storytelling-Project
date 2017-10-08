using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAlphaInterpolator : MonoBehaviour
{
    private Renderer[] mChildRenderers;

    public float LerpInTime;

    public AnimationCurve Curve;

    public bool Show, LaunchOnStart;

    void Start()
    {
        mChildRenderers = GetComponentsInChildren<Renderer>();

        if (Show)
        {
            for (int i = 0; i < mChildRenderers.Length; i++)
            {
                var renderer = mChildRenderers[i];
                var c = renderer.material.color;
                c.a = 0;
                renderer.material.color = c;
            }
        }

        if (LaunchOnStart)
        {
            Launch();
        }
    }

    public void Launch()
    {
        StartCoroutine(AlphaLerpChildren());
    }

    IEnumerator AlphaLerpChildren()
    {
        float i = 0;
        float rate = 1 / LerpInTime;

        float startValue, endValue;

        if (Show)
        {
            startValue = 0;
            endValue = 1;
        }
        else
        {
            startValue = 1;
            endValue = 0;
        }

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            for (int j = 0; j < mChildRenderers.Length; j++)
            {
                var renderer = mChildRenderers[j];

                var c = renderer.material.color;
                c.a = Mathf.Lerp(startValue, endValue, Curve.Evaluate(i));
                renderer.material.color = c;
            }
            yield return null;
        }


    }
}
