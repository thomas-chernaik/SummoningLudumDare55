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
        //save a sample cutscene file
        CutsceneLine line1 = new CutsceneLine();
        line1.name = "Player";
        line1.dialogue = "Hello, world!";
        line1.portrait = "player";
        CutsceneLine line2 = new CutsceneLine();
        line2.name = "NPC";
        line2.dialogue = "Goodbye, world!";
        line2.portrait = "npc";
        lines = new List<CutsceneLine>();
        lines.Add(line1);
        lines.Add(line2);
        string json = JsonUtility.ToJson(new SerializableList<CutsceneLine>(lines));
        System.IO.File.WriteAllText(Application.dataPath + "/Cutscenes/intro.json", json);

    }

    
    public void LoadCutscene(string cutsceneFile)
    {
        //debug output the path to the cutscene file
        Debug.Log("Loading cutscene from: " + Application.dataPath + "/Cutscenes/" + cutsceneFile);
        //check if the file exists
        if(!System.IO.File.Exists(Application.dataPath + "/Cutscenes/" + cutsceneFile))
        {
            Debug.LogError("Cutscene file not found!");
            return;
        }
        print(Resources.Load<TextAsset>("Cutscenes/" + cutsceneFile));
        string json = System.IO.File.ReadAllText(Application.dataPath + "/Cutscenes/" + cutsceneFile);
        print(json);
        lines = JsonUtility.FromJson<SerializableList<CutsceneLine>>(json).items;
        //debug output the loaded cutscene
        foreach(CutsceneLine line in lines)
        {
            Debug.Log(line.name + ": " + line.dialogue);
        }
        if(lines.Count == 0)
        {
            Debug.LogError("Cutscene file is empty!");
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
