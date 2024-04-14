using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public float tileSize;
    public Vector2 offset;
    public GameObject tilePrefab;
    public GameObject[][] tiles;
    public Tile[][] tileScripts;
    public string levelFile;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
