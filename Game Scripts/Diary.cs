using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    public Text diaryText;
    public string concText;
    public List<string> Entries = new List<string>();

    public void addEntry(EntryType entryType)
    {
        switch (entryType)
        {
            case EntryType.Bed:
                Entries.Add("");
                break;
            case EntryType.Plant:
                //Entries.Add("");
                break;
            case EntryType.Poster:
                Entries.Add("");
                break;
            case EntryType.Radio:
                Entries.Add("");
                break;
            case EntryType.VR:
                Entries.Add("");
                break;
            case EntryType.WardrobeDoorLeftLocked:
                Entries.Add("");
                break;
            case EntryType.WardrobeDoorLeftUnlocked:
                Entries.Add("");
                break;
            case EntryType.WardrobeDoorRight:
                Entries.Add("");
                break;
            case EntryType.Window:
                Entries.Add("");
                break;
            case EntryType.FoodDispenser:
                Entries.Add("Doctor Reiley is checking my blood sample. Hope it’s good news, although standard procedure is still 72 hours of quarantine. Sigh... I’m not getting out any time soon. But dinner was alright.I feel pretty good.");
                break;
            case EntryType.Door:
                Entries.Add("");
                break;
            case EntryType.DuctTape:
                Entries.Add("");
                break;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        concText = "";
        foreach (var entry in Entries)
        {
            concText += (entry + "\n");
            concText.Trim();
        }
    }

    public enum EntryType
    {
        Bed,
        Plant,
        Poster,
        Radio,
        Window,
        VR,
        WardrobeDoorRight,
        WardrobeDoorLeftLocked,
        WardrobeDoorLeftUnlocked,
        FoodDispenser,
        Door,
        DuctTape
    }
}
