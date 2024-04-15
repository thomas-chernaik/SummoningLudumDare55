using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    private VisualElement m_Bar;
    public static UIHandler instance { get; private set; }

    // UI dialogue window variables
    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialogue;
    private Label m_Label;
    private float m_TimerDisplay;


    // Awake is called when the script instance is being loaded (in this situation, when the game scene loads)
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Bar = uiDocument.rootVisualElement.Q<VisualElement>("Bar");
        SetManaValue(1.0f);


        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("DialogueUI");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;

        m_Label = uiDocument.rootVisualElement.Q<Label>("TextDisplayed");


    }



    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }


        }
    }


    public void SetManaValue(float percentage)
    {
        m_Bar.style.width = Length.Percent(100 * percentage);
    }


    public void DisplayDialogue(string inputText)
    {
        m_Label.text = inputText;
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
        
    }

}