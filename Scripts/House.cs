using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    public GameObject player;
    public GameObject NewDayUI;
    public Button closeButton;
    public Button NewDayButton;
    public GameManager gameManager;
    
    void Start()
    {
        NewDayUI.SetActive(false);
        closeButton.onClick.AddListener(CloseNewDayUI);
        NewDayButton.onClick.AddListener(TriggerNextDay);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            NewDayUI.SetActive(true);
        }
    }
    void CloseNewDayUI()
    {
        NewDayUI.SetActive(false);
    }
    void TriggerNextDay()
    {
        NewDayUI.SetActive(false);
        gameManager.TriggerNextDay();
    }
}
