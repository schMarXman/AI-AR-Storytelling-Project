using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehaviour : MonoBehaviour
{
    public float MinShowDistance = 0.2f;

    private DotBehaviour mDot1, mDot2;

    private bool mSetupDone = false;

    private float mDistance;

    private SpriteRenderer mRenderer;

    void Start()
    {
        mRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(DotBehaviour dot1, DotBehaviour dot2)
    {
        mDot1 = dot1;
        mDot2 = dot2;

        mDot1.AddLine(this);
        mDot2.AddLine(this);

        //transform.SetParent(mDot1.transform);
        transform.localPosition = Vector3.zero;

        mSetupDone = true;
    }

    void Update()
    {
        if (mSetupDone /*&& mDot1 != null && mDot2 != null*/)
        {
            transform.position = mDot1.transform.position;

            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2((mDot2.transform.position.y - transform.position.y), (mDot2.transform.position.x - transform.position.x)) * Mathf.Rad2Deg));

            mDistance = Vector2.Distance(mDot1.transform.position, mDot2.transform.position);

            StretchScaleToDot2();

            //if (mDistance <= MinShowDistance)
            //{
            var col = mRenderer.color;
            col.a = 1 - mDistance;
            mRenderer.color = col;
            //}

            if (mRenderer.color.a <= 0)
            {
                DestroyLine();
            }
        }
    }

    void StretchScaleToDot2()
    {

        var realDistance = mDistance * (Screen.height / 2) /*/ Camera.main.orthographicSize) * 1.12f*/;

        transform.localScale = new Vector3(realDistance, 1, 1);
    }

    public void DestroyLine()
    {
        mDot1.RemoveLine(this);
        mDot2.RemoveLine(this);

        Destroy(gameObject);

        //DotManager.Instance.GetLineAmount(true);
    }

    public DotBehaviour GetDot1()
    {
        return mDot1;
    }

    public DotBehaviour GetDot2()
    {
        return mDot2;
    }
}
