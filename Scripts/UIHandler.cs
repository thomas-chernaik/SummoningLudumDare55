using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public Board gameboard;
    private VisualElement m_Bar;
    public static UIHandler instance { get; private set; }

    // UI dialogue window variables
    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialogue;
    private VisualElement m_Book;
    private Label m_Label;
    private Label m_Title;
    private Label m_Pattern;
    private float m_TimerDisplay;

    private bool bookShowing = false;

    private string[] titles = {"Wax","Vitriol","Iron","Copper","Sulphur"};
    private string[,] patterns = {
        {"?XXXB\nXDD?X\nXDDDX\nXDDDX\nBXXXB","?XXXXX?\nX?XXXDX\nXXDX?XX\nXXXDXXX\nXX?XDXX\nXDXXX?X\n?XXXXX?","DD??\n???D\n????\n???D","X?X?X\nX?X?X\n?XXX?\nX???X","XX?XX\nXX?XX\n?????\nXX?XX\n"},
        {"BXXXB\nXDDDX\nXDDDX\nXDDDX\nBXXXB","BXXXXX?\nXDXXXDX\nXXDXDXX\nXXXDXXX\nXX?XDXX\nXDXXX?X\n?XXXXXB","DD?D\n?R?D\nD?RD\n?D?D","XDX?X\nXDX?X\n?XXX?\nX?D?X","XXDXX\nXX?XX\n?????\nXX?XX\n"},
        {"BXXXB\nXDDDX\nXDDDX\nXDDDX\nBXXXB","BXXXXXB\nXDXXXDX\nXXDXDXX\nXXXDXXX\nXXDXDXX\nXDXXXDX\nBXXXXXB","DDDD\nDR?D\nDRRD\nDDDD","XDXDX\nXDX?X\nFXXX?\nX?DFX","XXDXX\nXXFXX\n??F??\nXXDXX\n"},
        {"BXXXB\nXDDDX\nXDDDX\nXDDDX\nBXXXB","BXXXXXB\nXDXXXDX\nXXDXDXX\nXXXDXXX\nXXDXDXX\nXDXXXDX\nBXXXXXB","DDDD\nDRRD\nDRRD\nDDDD","XDXDX\nXDX?X\nFXXX?\nXFDFX","XXDXX\nXXFXX\nB?F?B\nXXDXX\n"},
        {"BXXXB\nXDDDX\nXDDDX\nXDDDX\nBXXXB","BXXXXXB\nXDXXXDX\nXXDXDXX\nXXXDXXX\nXXDXDXX\nXDXXXDX\nBXXXXXB","DDDD\nDRRD\nDRRD\nDDDD","XDXDX\nXDXDX\nFXXXF\nXFDFX","XXDXX\nXXFXX\nB?F?B\nXXDXX\n"},
        {"BXXXB\nXDDDX\nXDDDX\nXDDDX\nBXXXB","BXXXXXB\nXDXXXDX\nXXDXDXX\nXXXDXXX\nXXDXDXX\nXDXXXDX\nBXXXXXB","DDDD\nDRRD\nDRRD\nDDDD","XDXDX\nXDXDX\nFXXXF\nXFDFX","XXDXX\nXXFXX\nBDFDB\nXXDXX\n"}
        };
    private int index = 0;



    // Awake is called when the script instance is being loaded (in this situation, when the game scene loads)
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("DialogueUI");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;

        m_Book = uiDocument.rootVisualElement.Q<VisualElement>("GardeningBook");
        m_Book.style.display = DisplayStyle.None;

        m_Label = uiDocument.rootVisualElement.Q<Label>("TextDisplayed");

        m_Title = uiDocument.rootVisualElement.Q<Label>("DemonName");
        m_Pattern = uiDocument.rootVisualElement.Q<Label>("Pattern");
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

    public void DisplayDialogue(string inputText)
    {
        m_Label.text = inputText;
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }
    public void ChangeBook()
    {
        m_Title.text = titles[index];
        int prog = gameboard.GetNumberOfDemons();
        m_Pattern.text = patterns[prog,index];
        if (bookShowing)
        {
            m_Book.style.display = DisplayStyle.None;
            bookShowing = false;
        }
        else
        {
            m_Book.style.display = DisplayStyle.Flex;
            bookShowing = true;
        }

    }

    public void BookForward()
    {
        int prog = gameboard.GetNumberOfDemons();
        if(index < 4)
        {
            index++;
            m_Title.text = titles[index];
            m_Pattern.text = patterns[prog,index];
        }
        
    }

    public void BookBackward()
    {
        int prog = gameboard.GetNumberOfDemons();
        if(index > 0)
        {
            index--;
            m_Title.text = titles[index];
            m_Pattern.text = patterns[prog,index];
        }
    }
}