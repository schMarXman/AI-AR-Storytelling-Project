﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableInformationDatabase
{
    public struct PersonData
    {
        public string FirstName;
        public string LastName;
        public string RelationshipStatus;
        public string Job;
        public string Hobby;
        public string NegativeTrait;
        public int Income;
        public int Age;
    }

    private static string[] mMaleFirstNames =
    {
        "Hans",
        "Peter",
        "Bill",
        "John",
        "Gregor",
        "Kevin"
    };

    private static string[] mFemaleFirstNames =
    {
        "Jeanette",
        "Lena",
        "Maria",
        "Ashley",
        "Linda",
        "Katharina",
        "Evelin"
    };

    private static string[] mLastNames =
    {
        "Ericson",
        "Schmidt",
        "Marx",
        "Müller",
        "Meyer",
        "Nguyen",
        "Brecht",
        "Wagner"
    };

    private static string[] mRelationshipStatus =
    {
        "Single",
        "In Beziehung",
        "Verheiratet",
        "Geschieden",
        "Verwitwet"
    };

    private static string[] mJob =
    {
        "Programmierer",
        "Ingenieur",
        "Frisör",
        "Bauer",
        "Banker",
        "Maurer",
        "Arbeitslos"
    };

    
    private static string[] mHobby =
    {
        "Videospiele spielen",
        "Musik machen",
        "Tanzen",
        "Sport machen",
        "Stricken",
        "Töpfern"
    };

    private static string[] mNegativeTraits =
    {
        "Vorbestraft",
        "Verschuldet",
        "Aggressiv",
        "Depressiv",
        "Auf Bewährung"
    };


    public static PersonData GetPerson(bool male)
    {
        var person = new PersonData();

        if (male)
        {
            person.FirstName = mMaleFirstNames[Random.Range(0, mMaleFirstNames.Length)];
        }
        else
        {
            person.FirstName = mFemaleFirstNames[Random.Range(0, mFemaleFirstNames.Length)];
        }

        person.LastName = mLastNames[Random.Range(0, mLastNames.Length)];
        person.Hobby = mHobby[Random.Range(0, mHobby.Length)];
        person.Job = mJob[Random.Range(0, mJob.Length)];
        person.RelationshipStatus = mRelationshipStatus[Random.Range(0, mRelationshipStatus.Length)];
        person.NegativeTrait = mNegativeTraits[Random.Range(0, mNegativeTraits.Length)];
        person.Income = Random.Range(200, 25000);
        person.Age = Random.Range(20, 50);

        return person;
    }
    
}
