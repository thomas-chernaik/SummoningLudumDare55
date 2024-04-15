using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CutsceneManager cutsceneManager;
    public Board board;
    // Start is called before the first frame update
    void Start()
    {
        cutsceneManager.StartCutscene("intro");
    }

    public void TriggerNextDay()
    {
        board.GrowPlants();
        board.TestForSpells();
        cutsceneManager.StartCutscene(board.GenerateNewDayCutscene());

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerNextDay();
        }
    }
}
