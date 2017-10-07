using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonInfoUI : MonoBehaviour
{
    [HideInInspector]
    public Transform Target;

    public Text Label;

    public Image TextPanel;

    public Vector3 Offset;

    private SelectableInformationDatabase.PersonData mData;

    private Camera mSelectorCamera;

    void Start()
    {
        mSelectorCamera = ObjectSelector.Instance.AttachedCamera;
    }

    public void SetPerson(SelectableInformationDatabase.PersonData data)
    {
        mData = data;
        string text = "Vorname: " + mData.FirstName + Environment.NewLine
                      + "Nachname: " + mData.LastName + Environment.NewLine
                      + "Beziehung: " + mData.RelationshipStatus + Environment.NewLine
                      + "Job: " + mData.Job + Environment.NewLine
                      + "Hobby: " + mData.Hobby;
        SetText(text);
    }

    private void SetText(string text)
    {
        Label.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        TextPanel.transform.position = mSelectorCamera.WorldToScreenPoint(Target.position) + Offset;
        //transform.LookAt(Camera.main.transform);
    }
}
