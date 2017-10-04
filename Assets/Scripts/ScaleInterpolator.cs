using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScaleInterpolator : MonoBehaviour
{
    public Vector3 StartVector, EndVector;

    public float ScaleInTime;

    public AnimationCurve Curve;

    public bool DoOnEnable;

    public UnityEvent OnScaleEnded;

    void OnEnable()
    {
        if (DoOnEnable)
        {
            StartCoroutine(ScaleLerp());
        }
    }

    IEnumerator ScaleLerp()
    {
        float i = 0;
        float rate = 1 / ScaleInTime;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(StartVector, EndVector, Curve.Evaluate(i));
            yield return null;
        }

        transform.localScale = EndVector;

        OnScaleEnded.Invoke();
    }
}
