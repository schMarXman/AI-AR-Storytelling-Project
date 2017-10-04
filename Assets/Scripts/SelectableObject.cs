using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public static SelectableObject CurrentlySelectedObject;

    public PersonInfoUI InfoUiPrefab;

    public bool IsMale;

    public PersonInfoUI ThisInfoObj;

    //public void Select()
    //{
    //    if (CurrentlySelectedObject != this)
    //    {
    //        CurrentlySelectedObject = this;
    //    }

    //    ShowInfo();
    //}

    public void Select()
    {
        if (CurrentlySelectedObject != null)
        {
            CurrentlySelectedObject.ThisInfoObj.gameObject.SetActive(false);
        }

        if (ThisInfoObj == null)
        {
            ThisInfoObj = Instantiate(InfoUiPrefab.gameObject).GetComponent<PersonInfoUI>();
            ThisInfoObj.Target = transform;
            ThisInfoObj.SetPerson(SelectableInformationDatabase.GetPerson(IsMale));
        }

        CurrentlySelectedObject = this;
        ThisInfoObj.gameObject.SetActive(true);
    }
}
