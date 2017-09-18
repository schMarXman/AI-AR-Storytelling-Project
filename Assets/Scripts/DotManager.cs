using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    public static DotManager Instance;

    public DotBehaviour DotPrefab;
    public GameObject LinePrefab;

    public bool SpawnDotsOnStart = true;

    public int DotAmount = 10;

    public float MaxDotDistance = 1f;

    private List<DotBehaviour> mDots = new List<DotBehaviour>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (SpawnDotsOnStart)
        {
            for (int i = 0; i < DotAmount; i++)
            {
                SpawnDot();
            }
        }
    }

    private void SpawnDot()
    {
        var newDot = Instantiate(DotPrefab.gameObject);
        //mDots.Add(newDot.GetComponent<DotBehaviour>());

        newDot.transform.position =
            Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
        newDot.transform.position = new Vector3(newDot.transform.position.x, newDot.transform.position.y, 0f);

        newDot.transform.SetParent(transform.GetChild(0));
    }

    public void AddDot(DotBehaviour dot)
    {
        if (!mDots.Contains(dot))
        {
            mDots.Add(dot);
        }
    }

    public void RemoveDot(DotBehaviour dot)
    {
        if (mDots.Contains(dot))
        {
            mDots.Remove(dot);

            SpawnDot();
        }

    }

    public void DrawLine(DotBehaviour dot1, DotBehaviour dot2)
    {
        if (!dot1.IsConnectedTo(dot2))
        {
            var line = Instantiate(LinePrefab.gameObject).GetComponent<LineBehaviour>();
            line.transform.SetParent(transform.GetChild(1));
            line.Setup(dot1, dot2);

            //GetLineAmount(true);
        }
    }

    public int GetLineAmount(bool print)
    {
        var amount = transform.GetChild(1).childCount;

        if (print)
        {
            Debug.Log("Current line amount: " + amount);
        }

        return amount;
    }

    public int GetDotAmount(bool print)
    {
        var amount = transform.GetChild(0).childCount;

        if (print)
        {
            Debug.Log("Current dot amount: " + amount);
        }

        return amount;
    }
}
