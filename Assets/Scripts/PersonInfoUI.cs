using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonInfoUI : MonoBehaviour
{
    public Transform Target;

    public Text Label;

    private SelectableInformationDatabase.PersonData mData;

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
        transform.position = Target.position;
        transform.LookAt(Camera.main.transform);
    }
}
