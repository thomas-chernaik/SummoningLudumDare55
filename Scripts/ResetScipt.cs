using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResetScipt : MonoBehaviour
{
    public GameObject ResetUI;
    public Button ResetButton;
    public Button ConfirmButton;
    public Button CancelButton;
    // Start is called before the first frame update
    void Start()
    {
        ResetUI.SetActive(false);
        ResetButton.onClick.AddListener(Reset);
        ConfirmButton.onClick.AddListener(Confirm);
        CancelButton.onClick.AddListener(Cancel);
    }

    void Reset()
    {
        ResetUI.SetActive(true);
    }
    void Confirm()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Cancel()
    {
        ResetUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
