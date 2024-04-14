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
    //require a sprite renderer
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
        boxCollider.size = spriteRenderer.size;
        UpdateTile();
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
