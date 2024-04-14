using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class UIHandler : MonoBehaviour
{
    private VisualElement m_Bar;
    public static UIHandler instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Bar = uiDocument.rootVisualElement.Q<VisualElement>("Bar");
        SetManaValue(1.0f);
    }


    public void SetManaValue(float percentage)
    {
        m_Bar.style.width = Length.Percent(100 * percentage);
    }


}