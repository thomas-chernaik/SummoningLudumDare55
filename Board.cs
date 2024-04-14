using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    //section world generation
    [SerializeField]
    public int width;
    public int height;
    public float tileSize;
    public Vector2 offset;
    public GameObject tilePrefab;
    public GameObject[][] tiles;
    public Tile[][] tileScripts;
    public string levelFile;
    //section game logic
    public GameObject tileSelector;
    public Tile selectedTile;
    public ToolBar toolBar;
    
    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[width][];
        tileScripts = new Tile[width][];
        for (int i = 0; i < width; i++)
        {
            tiles[i] = new GameObject[height];
            tileScripts[i] = new Tile[height];
        }
        //load level
        LoadLevel();
    }
    void LoadLevel(string level=null)
    {
        if(level == null)
        {
            //leave all tiles as default
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tiles[i][j] = Instantiate(tilePrefab, new Vector3(i * tileSize + offset.x, j * tileSize + offset.y, 0), Quaternion.identity);
                    tileScripts[i][j] = tiles[i][j].GetComponent<Tile>();
                    tileScripts[i][j].board = this;
                }
            }

        }
    }
    void GeneratePlantArray()
    {
        //TODO: generate plant array
    }
    void TestForSpells()
    {
        //call each spells test function

    }
    void TestForSpell1()
    {
        //test for spell 1
    }

    void GrowPlants()
    {
        //grow plants according to rules
    }

    public void SelectTile(Transform tilePosition, Tile tileToSelect)
    {
        //move the tile selector to the selected tile
        tileSelector.transform.position = tilePosition.position - transform.forward;
        tileSelector.transform.rotation = tilePosition.rotation;
        tileSelector.transform.localScale = tilePosition.localScale;
        //set the selected tile
        selectedTile = tileToSelect;
    }

    void ManageCommands()
    {
        //if the player clicks the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            //get the thing selected in the toolbar
            int selectedItem = toolBar.selectedItem;
            //if the selected item is 0, then till the land
            if (selectedItem == 0)
            {
                selectedTile.Till();
            }
            //otherwise, we plant a seed
            else
            {
                selectedTile.Plant(selectedItem - 1);
            }
        }
    }

    void LateUpdate()
    {
        TestForSpells();
        GrowPlants();
        ManageCommands();
    }
}
