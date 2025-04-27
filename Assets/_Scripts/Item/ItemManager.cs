using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    private Item itemPrefab;
    private ItemData itemData;
    private GameObject itemHolder;

    protected override void Awake()
    {
        base.Awake();
        if (!itemData)
            itemData = Resources.Load<ItemData>("SOData/ItemData");
        if (!itemPrefab)
            itemPrefab = Resources.Load<Item>("Prefabs/Item");
    }

    /// <summary>
    /// Lấy tỉ lệ để spawn item theo loại
    /// </summary>
    /// <returns></returns>
    private Rate GetItemRate()
    {
        var normalIndex = GameConfig.normalItemRate;
        var rareIndex = normalIndex + GameConfig.rareItemRate;
        var randomIndex = Random.Range(0, 100);
        if (randomIndex <= normalIndex)
        {
            return Rate.Normal;
        }
        else if (randomIndex < rareIndex)
        {
            return Rate.Rare;
        }
        else
        {
            return Rate.Special;
        }
    }

    /// <summary>
    /// lấy cấu hình của item
    /// </summary>
    /// <returns></returns>
    private ItemConfig GetItemConfig()
    {
        Rate rate = GetItemRate();
        switch (rate)
        {
            case Rate.Normal:
                int normalIndex = Random.Range(0, itemData.normalItems.Length);
                return itemData.normalItems[normalIndex];
            case Rate.Rare:
                int rareIndex = Random.Range(0, itemData.rareItems.Length);
                return itemData.rareItems[rareIndex];
            case Rate.Special:
                return itemData.specialItem;
        }
        return null;
    }

    /// <summary>
    /// spawn item 
    /// </summary>
    /// <param name="pos"></param>
    public void SpawnItem(Transform transform)
    {
        var itemConfig = GetItemConfig();
        if (itemConfig != null)
        {
            var item = Instantiate(itemPrefab, transform);

            var pos = new Vector2(transform.position.x,
                transform.position.y + GameConfig.itemSpawnPosInTrunk);
            item.transform.position = pos;
            item.ItemType = itemConfig.itemType;
            item.Rate = itemConfig.rate;
            item.SetSprite(itemConfig.sprite);
        }

    }

    /// <summary>
    /// Spawn item từ gỗ
    /// </summary>
    /// <param name="trunkController"></param>
    public void SpawnItemsFromTrunk(TrunkController trunkController)
    {
        Trunk[] trunks = trunkController.GetTrunkRandom();
        foreach (var trunk in trunks)
        {
            SpawnItem(trunk.transform);
        }
    }
}
