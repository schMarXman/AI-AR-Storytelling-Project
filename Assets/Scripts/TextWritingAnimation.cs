using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWritingAnimation : MonoBehaviour
{
    public float TimeBetweenChars;
    public bool PlayOnStart;
    public bool PlayOnEnable;

    private Text mText;

    void OnEnable()
    {
        if (!mText)
        {
            mText = GetComponent<Text>();
        }

        if (PlayOnEnable)
        {
            StartAnimation();
        }
    }

    void Start()
    {
        if (!mText)
        {
            mText = GetComponent<Text>();
        }

        if (PlayOnStart)
        {
            StartAnimation();
        }
    }

    public void StartAnimation()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        var chars = mText.text.ToCharArray();

        mText.text = String.Empty;

        for (int i = 0; i < chars.Length; i++)
        {
            mText.text += chars[i];
            yield return new WaitForSeconds(TimeBetweenChars);
        }
    }
}
