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
[RequireComponent(typeof(BoxCollider2D))]
public class Tile : MonoBehaviour
{
    public Board board;
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
    private SpriteRenderer spriteRenderer;
    private TileType prevType;

    // Start is called before the first frame update
    void Start()
    {
        prevType = type;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //set the box collider to be the same size as the sprite
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = spriteRenderer.size * 2;
        UpdateTile();
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

    void UpdateTile()
    {
        //set sprite based on type
        switch (type)
        {
            case TileType.NotGarden:
                spriteRenderer.sprite = defaultSprites[defaultType];
                break;
            case TileType.Dead:
                spriteRenderer.sprite = deadSprites[deadType];
                break;
            case TileType.Tilled:
                spriteRenderer.sprite = tilledSprite;
                break;
            case TileType.Seeded:
                spriteRenderer.sprite = seededSprites[seedType];
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
        //check if the mouse is over the tile

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            board.SelectTile(transform, this);
        }        
    }
}
