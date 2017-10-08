using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class ARMarkerDetector : MonoBehaviour, ITrackableEventHandler
{
    public List<TrackableBehaviour> TrackableBehaviours = new List<TrackableBehaviour>();

    public static ARMarkerDetector Instance;

    public UnityEvent OnMarkerDetected;
    public UnityEvent OnMarkerLost;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < TrackableBehaviours.Count; i++)
        {
            TrackableBehaviours[i].RegisterTrackableEventHandler(this);
        }

        OnMarkerDetected.AddListener(() => GameEvent.EndEventById("WaitForMarker", true));
        OnMarkerLost.AddListener(() => {
            if (SelectableObject.CurrentlySelectedObject != null)
            {
                SelectableObject.CurrentlySelectedObject.ThisInfoObj.gameObject.SetActive(true);
            }
        });
        OnMarkerLost.AddListener(()=> {
            if (SelectableObject.CurrentlySelectedObject != null)
            {
                SelectableObject.CurrentlySelectedObject.ThisInfoObj.gameObject.SetActive(false);
            }
        });
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // Tracker detected
            OnMarkerDetected.Invoke();
        }
        else
        {
            // Tracker lost
            OnMarkerLost.Invoke();
        }
    }
}
