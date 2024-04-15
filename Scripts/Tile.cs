using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create enum
public enum TileType
{
    NotGarden,
    Dead,
    Tilled,
    Seeded,
    Grown,
    Drained
}
[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public GameObject tillParticles;
    public GameObject plantParticles;
    public TileType type = TileType.Dead;
    public int defaultType = 0;
    public int deadType = 0;
    public int seedType = 0;
    public List<Sprite> defaultSprites;
    public List<Sprite> deadSprites;
    public Sprite tilledSprite;
    public List<Sprite> seededSprites;
    public List<Sprite> grownSprites;
    public List<Sprite> drainedSprites;
    private Board board;
    private SpriteRenderer spriteRenderer;
    private TileType prevType;

    // Start is called before the first frame update
    void Start()
    {
        prevType = type;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //set the box collider to be the same size as the sprite
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if(boxCollider != null)
            boxCollider.size = spriteRenderer.size;
        UpdateTile();
    }
    public void SetBoard(Board board)
    {
        this.board = board;
    }
    public int GetSeedType(bool grown)
    {
        if (grown)
        {
            if (type == TileType.Grown)
            {
                return seedType;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (type == TileType.Seeded || type == TileType.Grown || type == TileType.Drained)
            {
                return seedType;
            }
            else
            {
                return -1;
            }
        }
    }

    public void Drain()
    {
        if (type == TileType.Grown)
        {
            type = TileType.Drained;
        }
    }

    public void Grow()
    {
        print("hey");
        if (type == TileType.Seeded)
        {
            //if we are a rose, enable the box collider
            if (seedType == 0)
            {
                BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                    boxCollider.enabled = true;
            }
            type = TileType.Grown;
        }
    }

    public void Plant(int seedType)
    {
        if (type == TileType.Tilled)
        {
            type = TileType.Seeded;
            this.seedType = seedType;
        }
    }
    public void Till()
    {
        if (type == TileType.Dead || type == TileType.Seeded)
        {
            type = TileType.Tilled;
        }
    }
    void DeactivateTillParticles()
    {
        tillParticles.SetActive(false);
    }
    void DeactivatePlantParticles()
    {
        plantParticles.SetActive(false);
    }

    void UpdateTile()
    {
        //set sprite based on type
        switch (type)
        {
            case TileType.NotGarden:
                //enable box collider
                BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                    boxCollider.enabled = true;
                spriteRenderer.sprite = defaultSprites[defaultType];
                break;
            case TileType.Dead:
                spriteRenderer.sprite = deadSprites[deadType];
                break;
            case TileType.Tilled:
                spriteRenderer.sprite = tilledSprite;
                //enable till particles
                tillParticles.SetActive(true);
                //set to deactive after 0.5 seconds
                Invoke("DeactivateTillParticles", 0.5f);
                break;
            case TileType.Seeded:
                spriteRenderer.sprite = seededSprites[seedType];
                //enable plant particles
                plantParticles.SetActive(true);
                //set to deactive after 0.5 seconds
                Invoke("DeactivatePlantParticles", 0.5f);
                break;
            case TileType.Grown:
                spriteRenderer.sprite = grownSprites[seedType];
                break;
            case TileType.Drained:
                spriteRenderer.sprite = drainedSprites[seedType];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (prevType != type)
        {
            UpdateTile();
            prevType = type;
        }    
    }
}
