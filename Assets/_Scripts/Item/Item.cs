using UnityEngine;

public class Item: MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private Rate rate;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (!spriteRenderer)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public ItemType ItemType
    {
        get { return itemType; }
        set { itemType = value; }
    }

    public Rate Rate
    {
        get { return rate; }
        set { rate = value; }
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
