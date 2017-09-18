using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotBehaviour : MonoBehaviour
{
    public float Speed = 1;

    private List<LineBehaviour> mLines = new List<LineBehaviour>();

    private Vector2 mDirection;

    private Coroutine mMovementCoroutine;

    private Vector2 mScreenPosition;

    // Use this for initialization
    void Start()
    {
        DotManager.Instance.AddDot(this);

        mDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        mMovementCoroutine = StartCoroutine(Movement());

        //var line = Instantiate(DotManager.Instance.LinePrefab.gameObject).GetComponent<LineBehaviour>();
        //line.Setup(this, GameObject.Find("DotPrefab (1)").GetComponent<DotBehaviour>());
    }

    // Update is called once per frame
    void Update()
    {
        mScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

        CheckPositionWithinScreen();
    }

    private void CheckPositionWithinScreen()
    {
        if (mScreenPosition.x > Screen.width || mScreenPosition.x < 0)
        {
            DestroyDot();
        }
        else if (mScreenPosition.y > Screen.height || mScreenPosition.y < 0)
        {
            DestroyDot();
        }
    }

    private IEnumerator Movement()
    {
        while (true)
        {
            transform.Translate(mDirection * Speed * Time.deltaTime);

            yield return null;
        }
    }

    private void DestroyDot()
    {
        DotManager.Instance.RemoveDot(this);
        for (int i = 0; i < mLines.Count; i++)
        {
            mLines[i].DestroyLine();
            i--;
        }

        Destroy(gameObject);
    }

    public void AddLine(LineBehaviour line)
    {
        mLines.Add(line);
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    var otherDot = other.gameObject.GetComponent<DotBehaviour>();

    //    if (otherDot)
    //    {
    //        DotManager.Instance.DrawLine(this, otherDot);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherDot = other.gameObject.GetComponent<DotBehaviour>();

        if (otherDot)
        {
            DotManager.Instance.DrawLine(this, otherDot);
        }
    }


    //public bool HasLine()
    //{
    //    return mLine != null;
    //}

    public bool IsConnectedTo(DotBehaviour dot)
    {
        for (int i = 0; i < mLines.Count; i++)
        {
            var line = mLines[i];

            if ((line.GetDot1() == this && line.GetDot2() == dot) || (line.GetDot1() == dot && line.GetDot2() == this))
            {
                return true;
            }
        }

        return false;
    }

    public void RemoveLine(LineBehaviour line)
    {
        if (mLines.Contains(line))
        {
            mLines.Remove(line); 
        }
    }
}
