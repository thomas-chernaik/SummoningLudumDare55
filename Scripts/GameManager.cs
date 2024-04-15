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
        cutsceneManager.StartCutscene("Intro");
        board.GenerateBoard();
    }

    public void TriggerNextDay()
    {
        cutsceneManager.StartCutscene("NextDay");
        board.GrowPlants();
        board.TestForSpells();
    }
}
