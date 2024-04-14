using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    public Material toolBarMaterial;
    public List<Texture2D> toolBarTextures;
    public int numberOfItems = 5;
    public int selectedItem = 0;
    public float widthPerItem = 0.3f;
    private int previousSelectedItem = 0;
    private int previousNumberOfItems = 0;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        //set up the material parameters
        toolBarMaterial.SetInt("_NumberOfItems", numberOfItems);
        toolBarMaterial.SetInt("_SelectedItem", selectedItem);
        toolBarMaterial.SetTexture("_MainTex1", toolBarTextures[0]);
        toolBarMaterial.SetTexture("_MainTex2", toolBarTextures[1]);
        toolBarMaterial.SetTexture("_MainTex3", toolBarTextures[2]);
        toolBarMaterial.SetTexture("_MainTex4", toolBarTextures[3]);
        toolBarMaterial.SetTexture("_MainTex5", toolBarTextures[4]);
        previousNumberOfItems = numberOfItems;
        previousSelectedItem = selectedItem;
        //get the rect transform component
        rectTransform = GetComponent<RectTransform>();

    }
    void UpdateSelection()
    {
        if(previousSelectedItem != selectedItem)
        {
            toolBarMaterial.SetInt("_SelectedItem", selectedItem);
            previousSelectedItem = selectedItem;
        }
    }
    void UpdateItems()
    {
        if(previousNumberOfItems != numberOfItems)
        {
            //cap the number of items to 10
            if(numberOfItems > 10)
            {
                numberOfItems = 10;
            }
            if(numberOfItems < 1)
            {
                numberOfItems = 1;
            }
            toolBarMaterial.SetInt("_NumberOfItems", numberOfItems);
            previousNumberOfItems = numberOfItems;
            //update the width of the toolbar to fit the number of items
            float width = widthPerItem * numberOfItems;
            rectTransform.localScale = new Vector3(width, rectTransform.localScale.y, rectTransform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check for input is a number key is pressed
        if (Input.anyKeyDown)
        {
            //check if we have a key pressed
            if(Input.inputString.Length <= 0)
            {
                return;
            }
            //check if the key pressed is a number key
            //convert keycode to an integer
            int keyNumber = (int)char.GetNumericValue(Input.inputString[0]);
            keyNumber--;
            if (keyNumber == -1)
            {
                keyNumber = 10;
            }
            //check if the key pressed is a number key
            if (keyNumber >= 0 && keyNumber < numberOfItems)
            {
                //set the selected item to the key pressed
                selectedItem = keyNumber;
                //update the material
                UpdateSelection();
            }
        }
        //check if the mouse wheel has been scrolled
        if (Input.mouseScrollDelta.y > 0)
        {
            //scroll up
            selectedItem++;
            if (selectedItem >= numberOfItems)
            {
                selectedItem = 0;
            }
            //update the material
            UpdateSelection();
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            //scroll down
            selectedItem--;
            if (selectedItem < 0)
            {
                selectedItem = numberOfItems - 1;
            }
            //update the material
            UpdateSelection();
        }
        UpdateItems();
    }
}
