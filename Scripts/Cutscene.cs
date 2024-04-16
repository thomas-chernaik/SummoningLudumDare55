using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//include the System.Text.Json namespace

using UnityEngine;

[System.Serializable]
public struct CutsceneLine
{
    public string name;
    public string dialogue;
    public string portrait;
}


public class Cutscene : MonoBehaviour
{
    public string cutsceneFile;
    public List<CutsceneLine> lines;

    void Start()
    {

    }


    public void LoadCutscene(string cutsceneFile)
    {
        //we are hardcoding the cutscene file for now
        if (cutsceneFile == "intro.json")
        {
            lines = new List<CutsceneLine>();
            CutsceneLine lineToAdd = new CutsceneLine();
            //                new CutsceneLine {name,"Narrator",dialogue,"Raj Berry has been banished from the magical realm and has settled down as a gardener.",portrait,"raj"},

            lineToAdd.name = "Narrator";
            lineToAdd.dialogue = "Raj Berry has been banished from the magical realm and has settled down as a gardener.";
            lineToAdd.portrait = "raj";
            lines.Add(lineToAdd);
            //{"name","Narrator","dialogue","He doesn't mind the new life, but he misses his old demon pals.","portrait","raj"}
            CutsceneLine lineToAdd2 = new CutsceneLine();
            lineToAdd2.name = "Narrator";
            lineToAdd2.dialogue = "He doesn't mind the new life, but he misses his old demon pals.";
            lineToAdd2.portrait = "raj";
            lines.Add(lineToAdd2);
            //{"name","Narrator","dialogue","He's managed to find a book on summoning demons, but some of the pages are missing information.","portrait","raj"}
            CutsceneLine lineToAdd3 = new CutsceneLine();
            lineToAdd3.name = "Narrator";
            lineToAdd3.dialogue = "He's managed to find a book on summoning demons, but some of the pages are missing information.";
            lineToAdd3.portrait = "raj";
            lines.Add(lineToAdd3);
            //{"name","Narrator","dialogue","All he knows for sure is when he goes to bed, and the plants are planted in the correct pattern, a demon is summoned and he is reunited with his friends.","portrait","raj"}
            CutsceneLine lineToAdd4 = new CutsceneLine();
            lineToAdd4.name = "Narrator";
            lineToAdd4.dialogue = "All he knows for sure is when he goes to bed, and the plants are planted in the correct pattern, a demon is summoned and he is reunited with his friends.";
            lineToAdd4.portrait = "raj";
            lines.Add(lineToAdd4);
            //{"name","Narrator","dialogue","He also has learned a bit of gardening knowledge, and knows that while a daisy can grow anywhere, a glow berry can only grow when it is next to exactly one other flower.","portrait","raj"}
            CutsceneLine lineToAdd5 = new CutsceneLine();
            lineToAdd5.name = "Narrator";
            lineToAdd5.dialogue = "He also has learned a bit of gardening knowledge, and knows that while a daisy can grow anywhere, a glow berry can only grow when it is next to exactly one other flower.";
            lineToAdd5.portrait = "raj";
            lines.Add(lineToAdd5);
            //{"name","Narrator","dialogue","The demon contract will be broken if he doesn't summon at least one a day. Failure to do so will mean he loses touch with the demon realm forever.....'","portrait","raj"}
            CutsceneLine lineToAdd6 = new CutsceneLine();
            lineToAdd6.name = "Narrator";
            lineToAdd6.dialogue = "The demon contract will be broken if he doesn't summon at least one a day. Failure to do so will mean he loses touch with the demon realm forever.....'";
            lineToAdd6.portrait = "raj";
            lines.Add(lineToAdd6);
        }


    }

    // Wrapper class to make List<T> serializable by JsonUtility
    [System.Serializable]
    private class SerializableList<T>
    {
        public List<T> items;

        public SerializableList(List<T> items)
        {
            this.items = items;
        }
    }

}