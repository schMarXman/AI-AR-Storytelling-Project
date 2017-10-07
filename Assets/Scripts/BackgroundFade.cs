using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFade : MonoBehaviour
{
    public float Speed;
    public float ResetThreshold;

    private RectTransform mRectT;
    private Vector3 mStartPosition;

    void Start()
    {
        mRectT = GetComponent<RectTransform>();

        mStartPosition = mRectT.anchoredPosition;
    }

    void Update()
    {
        transform.Translate(new Vector3(Speed, 0, 0));

        if (mRectT.anchoredPosition.x >= ResetThreshold)
        {
            mRectT.anchoredPosition = mStartPosition;
        }
    }

}
