using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;
using Unity.VisualScripting;


[RequireComponent(typeof(Cutscene))]
public class CutsceneManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI NameText;
    public TMPro.TextMeshProUGUI DialogueText;
    public Image Portrait;
    public Button NextButton;
    public float lettersPerSecond = 30;
    public float portraitFadeSpeed = 0.3f;
    private float typeTimer;
    private string currentDialogue;
    private bool nextButtonPressed;
    private bool portraitFading;
    public Cutscene cutscene;
    void Start()
    {
        cutscene = GetComponent<Cutscene>();
    }

    public void StartCutscene(string cutsceneFile)
    {
        cutscene.LoadCutscene(cutsceneFile);
        StartCoroutine(PlayCutscene(cutscene));
        NextButton.onClick.AddListener(() => { nextButtonPressed = true; });
    }

    private IEnumerator PlayCutscene(Cutscene cutscene)
    {
        //enable all children
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
       int currentLine = 0;
        while(currentLine < cutscene.lines.Count)
        {
            CutsceneLine line = cutscene.lines[currentLine];
            NameText.text = line.name;
            Portrait.sprite = Resources.Load<Sprite>("Portraits/" + line.portrait + ".png");
            Portrait.color = new Color(1, 1, 1, 0);
            currentDialogue = line.dialogue;
            DialogueText.text = "";
            typeTimer = 0;
            double portraitTime = 0;

            while(portraitTime < portraitFadeSpeed)
            {
                Portrait.color = new Color(1, 1, 1, Portrait.color.a + portraitFadeSpeed * Time.deltaTime);
                portraitTime += Time.deltaTime;
                yield return null;
            }
            while (DialogueText.text != currentDialogue)
            {
                typeTimer += Time.deltaTime;
                int numLetters = (int)(typeTimer * lettersPerSecond);
                numLetters = Mathf.Min(numLetters, currentDialogue.Length);
                DialogueText.text = currentDialogue.Substring(0, numLetters);
                //check for next button press
                if (nextButtonPressed)
                {
                    DialogueText.text = currentDialogue;
                    nextButtonPressed = false;
                    break;
                }
                //wait for next frame
                yield return null;
            }

            //wait for next button press
            while (!nextButtonPressed)
            {
                yield return null;
            }
            nextButtonPressed = false;
            currentLine++;
        }
        //wait for next button press
        while (!nextButtonPressed)
        {
            yield return null;
        }
        //end of cutscene
        //disable all children
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {

        
    }
}
