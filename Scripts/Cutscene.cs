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
        //debug output the path to the cutscene file
        Debug.Log("Loading cutscene from: " + Application.dataPath + "/Cutscenes/" + cutsceneFile);
        //check if the file exists
        if(!System.IO.File.Exists(Application.dataPath + "/Cutscenes/" + cutsceneFile))
        {
            Debug.LogError("Cutscene file not found!");
            return;
        }
        string json = System.IO.File.ReadAllText(Application.dataPath + "/Cutscenes/" + cutsceneFile);
        lines = JsonUtility.FromJson<SerializableList<CutsceneLine>>(json).items;

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
