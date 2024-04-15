using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//enum for seed types
public enum SeedType
{
    Daisy,
    GlowBerry,
    Rose,
    Daffodil,
}

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
    private int[][] plantArray;
    //demons
    public GameObject demon1;
    public GameObject demon2;
    public GameObject demon3;
    public GameObject demon4;
    public GameObject demon5;

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
        plantArray = new int[width][];
        for (int i = 0; i < width; i++)
        {
            plantArray[i] = new int[height];
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
                    tileScripts[i][j].SetBoard(this);
                }
            }

        }
    }
    void GeneratePlantArray(bool grown)
    {
        //print plant array

        
        //fill the plant array with the seed types
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                plantArray[i][j] = tileScripts[i][j].GetSeedType(grown);
            }
        }
    }
    void TestForSpells()
    {
        GeneratePlantArray(true);
        //call each spells test function
        TestForSpell1();
        TestForSpell2();
        TestForSpell3();
        TestForSpell4();
        TestForSpell5();
    }
    bool TestForItemsAtPositions(Vector2[] positions, int item)
    {
        foreach(Vector2 position in positions)
        {
            if (position.x >= 0 && position.x < width && position.y >= 0 && position.y < height)
            {
                if (plantArray[(int)position.x][(int)position.y] != item)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    void DrainAtPositions(Vector2[] positions)
    {
        foreach (Vector2 position in positions)
        {
            tileScripts[(int)position.x][(int)position.y].Drain();
        }
    }
    void TestForSpell1()
    {
        bool spell = false;
        //test for spell 1
        for (int i=0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                spell = true;
                Vector2 tilePosition = new Vector2(i, j);
                //if the tile is a glow berry
                if (plantArray[i][j] == (int)SeedType.GlowBerry)
                {
                    //check for the other glow berries, at (0,4), (4,0), (4,4) + (i,j)
                    Vector2[] glowBerryPositions = new Vector2[] { new Vector2(0,4) + tilePosition, new Vector2(4,0) + tilePosition, new Vector2(4,4) + tilePosition };
                    //check that there is a glow berry at each of the positions
                    spell = spell && TestForItemsAtPositions(glowBerryPositions, (int)SeedType.GlowBerry);
                    //check for the daises at (1,1), (1,2), (1,3), (2,1), (2,3), (3,1), (3,2), (3,3) + (i,j)
                    Vector2[] daisyPositions = new Vector2[] { new Vector2(1,1) + tilePosition, new Vector2(1,2) + tilePosition, new Vector2(1,3) + tilePosition, new Vector2(2,1) + tilePosition, new Vector2(2,3) + tilePosition, new Vector2(3,1) + tilePosition, new Vector2(3,2) + tilePosition, new Vector2(3,3) + tilePosition };
                    //check that there is a daisy at each of the positions
                    spell = spell && TestForItemsAtPositions(daisyPositions, (int)SeedType.Daisy);
                    //if the spell is true, then we cast the spell
                    if (spell)
                    {
                        //drain the glow berries
                        DrainAtPositions(glowBerryPositions);
                        //drain the daisies
                        DrainAtPositions(daisyPositions);
                        //instantiate demon1 at (2,2) + (i,j)
                        Instantiate(demon1, new Vector3(2 * tileSize + offset.x + i * tileSize, 2 * tileSize + offset.y + j * tileSize, -5), Quaternion.identity);

                        return;
                    }



                }
            }
        }
    }
    void TestForSpell2()
    {
        bool spell = false;
        //test for spell 2
        //test for spell 1
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                spell = true;
                Vector2 tilePosition = new Vector2(i, j);
                //if the tile is a glow berry
                if (plantArray[i][j] == (int)SeedType.GlowBerry)
                {
                    //check for the other glow berries, at (0,6), (6,0), (6,6) + (i,j)
                    Vector2[] glowBerryPositions = new Vector2[] { new Vector2(0, 6) + tilePosition, new Vector2(6, 0) + tilePosition, new Vector2(6, 6) + tilePosition };
                    //check that there is a glow berry at each of the positions
                    spell = spell && TestForItemsAtPositions(glowBerryPositions, (int)SeedType.GlowBerry);
                    //check for the daisies at (1,1), (2,2), (3,3), (4,4),(5,5), (1,5), 2,4), (4,2), (5,1) + (i,j)
                    Vector2[] daisyPositions = new Vector2[] { new Vector2(1, 1) + tilePosition, new Vector2(2, 2) + tilePosition, new Vector2(3, 3) + tilePosition, new Vector2(4, 4) + tilePosition, new Vector2(5, 5) + tilePosition, new Vector2(1, 5) + tilePosition, new Vector2(2, 4) + tilePosition, new Vector2(4, 2) + tilePosition, new Vector2(5, 1) + tilePosition };
                    //check that there is a daisy at each of the positions
                    spell = spell && TestForItemsAtPositions(daisyPositions, (int)SeedType.Daisy);
                    //if the spell is true, then we cast the spell
                    if (spell)
                    {
                        //drain the glow berries
                        DrainAtPositions(glowBerryPositions);
                        //drain the daisies
                        DrainAtPositions(daisyPositions);
                        //instantiate demon2 at (3,3) + (i,j)
                        Instantiate(demon2, new Vector3(3 * tileSize + offset.x + i * tileSize, 3 * tileSize + offset.y + j * tileSize, -5), Quaternion.identity);

                        return;
                    }
                }
            }
        }
    }
    void TestForSpell3()
    {
        bool spell = false;
        //test for spell 1
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                spell = true;
                Vector2 tilePosition = new Vector2(i, j);
                //if the tile is a daisy
                if (plantArray[i][j] == (int)SeedType.Daisy)
                {
                    //check for the other daisies, at (0,1), (0,2), (0,3), (3,1), (3,2), (3,3), (2,3),(1,3), (1,0), (2,0), (3,0) + (i,j)
                    Vector2[] daisyPositions = new Vector2[] { new Vector2(0, 1) + tilePosition, new Vector2(0, 2) + tilePosition, new Vector2(0, 3) + tilePosition, new Vector2(3, 1) + tilePosition, new Vector2(3, 2) + tilePosition, new Vector2(3, 3) + tilePosition, new Vector2(2, 3) + tilePosition, new Vector2(1, 3) + tilePosition, new Vector2(1, 0) + tilePosition, new Vector2(2, 0) + tilePosition, new Vector2(3, 0) + tilePosition };
                    //check that there is a daisy at each of the positions
                    spell = spell && TestForItemsAtPositions(daisyPositions, (int)SeedType.Daisy);
                    //check for the roses at (1,1), (1,2), (2,1), (2,2) + (i,j)
                    Vector2[] rosePositions = new Vector2[] { new Vector2(1, 1) + tilePosition, new Vector2(1, 2) + tilePosition, new Vector2(2, 1) + tilePosition, new Vector2(2, 2) + tilePosition };
                    //check that there is a rose at each of the positions
                    spell = spell && TestForItemsAtPositions(rosePositions, (int)SeedType.Rose);
                    //if the spell is true, then we cast the spell
                    if (spell)
                    {
                        //drain the daisies
                        DrainAtPositions(daisyPositions);
                        //drain the roses
                        DrainAtPositions(rosePositions);
                        //instantiate demon3 at (2,2) + (i,j)
                        Instantiate(demon3, new Vector3(2 * tileSize + offset.x + i * tileSize, 2 * tileSize + offset.y + j * tileSize, -5), Quaternion.identity);

                        return;
                    }
                }
            }
        }
    }
    void TestForSpell4()
    {
        bool spell = false;
        //test for spell 1
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                spell = true;
                Vector2 tilePosition = new Vector2(i, j);
                //if the tile is a daisy
                if (plantArray[i][j] == (int)SeedType.Daisy)
                {
                    print("found daisy at " + i + ", " + j);
                    //find daisies at (0,1), (2,0), (2,1), (1,4)
                    Vector2[] daisyPositions = new Vector2[] { new Vector2(0, -1) + tilePosition, new Vector2(2, 0) + tilePosition, new Vector2(2, -1) + tilePosition, new Vector2(1, -3) + tilePosition };
                    //check that there is a daisy at each of the positions
                    spell = spell && TestForItemsAtPositions(daisyPositions, (int)SeedType.Daisy);
                    //find daffodils at (-1,3), (0,4), (2,4), (3,3) + (i,j)
                    Vector2[] daffodilPositions = new Vector2[] { new Vector2(-1, -2) + tilePosition, new Vector2(0, -3) + tilePosition, new Vector2(2, -3) + tilePosition, new Vector2(3, -2) + tilePosition };
                    //check that there is a daffodil at each of the positions
                    spell = spell && TestForItemsAtPositions(daffodilPositions, (int)SeedType.Daffodil);
                    //if the spell is true, then we cast the spell
                    if (spell)
                    {
                        //drain the daisies
                        DrainAtPositions(daisyPositions);
                        //drain the daffodils
                        DrainAtPositions(daffodilPositions);
                        //instantiate demon4 at (1,3) + (i,j)
                        Instantiate(demon4, new Vector3(1 * tileSize + offset.x + i * tileSize, 3 * tileSize + offset.y + j * tileSize, -5), Quaternion.identity);

                        return;
                    }
                }
            }
        }
    }
    void TestForSpell5()
    {

        bool spell = false;
        //test for spell 1
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                spell = true;
                Vector2 tilePosition = new Vector2(i, j);
                //if the tile is a rose
                if (plantArray[i][j] == (int)SeedType.Rose)
                {
                    //check for roses at (2,0), (-1,-1), (1,-1), (3,-1), (-1,-2), (3,-2), (0,-3), (2,-3), (1,-4) + (i,j)
                    Vector2[] rosePositions = new Vector2[] { new Vector2(2,0) + tilePosition, new Vector2(-1,-1) + tilePosition, new Vector2(1,-1) + tilePosition, new Vector2(3,-1) + tilePosition, new Vector2(-1,-2) + tilePosition, new Vector2(3,-2) + tilePosition, new Vector2(0,-3) + tilePosition, new Vector2(2,-3) + tilePosition, new Vector2(1,-4) + tilePosition };
                    //check that there is a rose at each of the positions
                    spell = spell && TestForItemsAtPositions(rosePositions, (int)SeedType.Rose);
                    if (spell)
                    {
                        //drain the roses
                        DrainAtPositions(rosePositions);
                        //instantiate demon5 at (1,-3) + (i,j)
                        Instantiate(demon5, new Vector3(1 * tileSize + offset.x + i * tileSize, -3 * tileSize + offset.y + j * tileSize, -5), Quaternion.identity);

                        return;
                    }
                }
            }
        }
    }

    void GrowPlants()
    {
        GeneratePlantArray(false);
        //grow plants according to rules
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                switch (plantArray[i][j])
                {
                    case (int)SeedType.Daisy:
                        //grow daisies
                        tileScripts[i][j].Grow();
                        break;
                    case (int)SeedType.GlowBerry:
                        //grow glow berries
                        //check the surrounding tiles for the number of non- -1 tiles
                        int count = 0;
                        count = i > 0 && plantArray[i - 1][j] != -1 ? count + 1 : count;
                        count = i < width - 1 && plantArray[i + 1][j] != -1 ? count + 1 : count;
                        count = j > 0 && plantArray[i][j - 1] != -1 ? count + 1 : count;
                        count = j < height - 1 && plantArray[i][j + 1] != -1 ? count + 1 : count;
                        count = j > 0 && i > 0 && plantArray[i - 1][j - 1] != -1 ? count + 1 : count;
                        count = j < height - 1 && i < width - 1 && plantArray[i + 1][j + 1] != -1 ? count + 1 : count;
                        count = j > 0 && i < width - 1 && plantArray[i + 1][j - 1] != -1 ? count + 1 : count;
                        count = j < height - 1 && i > 0 && plantArray[i - 1][j + 1] != -1 ? count + 1 : count;
                        
                        if (count == 1)
                        {
                            tileScripts[i][j].Grow();
                        }
                        break;
                    case (int)SeedType.Rose:
                        //grow roses
                        tileScripts[i][j].Grow();
                        break;
                    case (int)SeedType.Daffodil:
                        //grow daffodils
                        //check the horizontal and vertically adjacent tiles for any daffodils
                        bool grow = true;
                        grow = i > 0 && plantArray[i - 1][j] == (int)SeedType.Daffodil ? false : grow;
                        grow = i < width - 1 && plantArray[i + 1][j] == (int)SeedType.Daffodil ? false : grow;
                        grow = j > 0 && plantArray[i][j - 1] == (int)SeedType.Daffodil ? false : grow;
                        grow = j < height - 1 && plantArray[i][j + 1] == (int)SeedType.Daffodil ? false : grow;
                        if (grow)
                        {
                            tileScripts[i][j].Grow();
                        }
                        break;
                }
            }
        }
    }

    public void SelectTile(Transform tilePosition, Tile tileToSelect)
    {
        if(selectedTile == tileToSelect)
        {
            return;
        }
        //move the tile selector to the selected tile
        tileSelector.transform.position = tilePosition.position - transform.forward;
        print(tileSelector.transform.position);
        //set the selected tile
        selectedTile = tileToSelect;
    }

    void ManageCommands()
    {
        //if the player clicks the left mouse button
        if (Input.GetMouseButtonDown(0) && selectedTile != null)
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
            //animate the selector
            tileSelector.GetComponent<Animator>().SetTrigger("Select");
        }
    }

    //calculate the tile the mouse is over
    void CalculateSelectedTile()
    {
        //get the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //calculate the tile position
        int x = Mathf.FloorToInt((mousePosition.x - offset.x + tileSize / 2) / tileSize);
        int y = Mathf.FloorToInt((mousePosition.y - offset.y + tileSize / 2) / tileSize);
        //if the tile is within the bounds of the board
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            //select the tile
            SelectTile(tiles[x][y].transform, tileScripts[x][y]);
        }
        else
        {
            //deselect the tile
            selectedTile = null;
            //disable the tile selector
            tileSelector.transform.position = new Vector3(-1000, -1000, 0);
        }

    }

    void LateUpdate()
    {
        CalculateSelectedTile();
        ManageCommands();
        if(Input.GetKeyDown(KeyCode.G))
        {
            GrowPlants();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            TestForSpells();
        }
    }
}
