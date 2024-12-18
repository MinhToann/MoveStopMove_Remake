using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class ShopData : Singleton<ShopData>
{
    [field: SerializeField] public ItemData data = new ItemData();
    const string path = "ShopData.abc";
    private void Awake()
    {
        data = LoadData();
    }
    public void SaveData()
    {
        SaveGame.Save(path, data);
    }
    public ItemData LoadData()
    {
        return SaveGame.Load(path, new ItemData());
    }
    public void AddItemToListSelect(PrefabType prefabType)
    {
        data.AddItemToListSelect(prefabType);
        SaveData();
    }
    public void AddItemToListEquipped(PrefabType prefabType)
    {
        data.AddItemToListEquipped(prefabType);
        SaveData();
    }
    public void RemoveItemFromListSelect(PrefabType prefabType)
    {
        data.RemoveItemFromListSelect(prefabType);
        SaveData();
    }
    public void RemoveItemFromListEquipped(PrefabType prefabType)
    {
        data.RemoveItemFromListEquipped(prefabType);
        SaveData();
    }
    public void AddTypeItemToList(ItemType itemType)
    {
        data.AddTypeItemToList(itemType);
        SaveData();
    }
    public void RemoveTypeItemFromList(ItemType itemType)
    {
        data.RemoveTypeItemFromList(itemType);
        SaveData();
    }
    public void SetCurrentItem(Item item)
    {
        data.SetCurrentItem(item);
        SaveData();
    }
}
[System.Serializable]
public class ItemData
{
    [field: SerializeField] public List<ItemType> listItemType { get; private set; } = new List<ItemType>();
    [field: SerializeField] public List<PrefabType> listEquippedItem { get; private set; } = new List<PrefabType>();
    [field: SerializeField] public List<PrefabType> listSelectItem { get; private set; } = new List<PrefabType>();
    [SerializeField] ItemSO itemSO;
    [field: SerializeField] public Item currentItem { get; private set; }
    public ItemData()
    {
        currentItem = null;
    }
    public void AddItemToListSelect(PrefabType prefabType)
    {
        for (int i = listSelectItem.Count - 1; i >= 0; i--)
        {
            if (listSelectItem.Contains(prefabType))
            {
                listSelectItem.Remove(prefabType);
            }
        }
        listSelectItem.Add(prefabType);
        
    }
    public void AddItemToListEquipped(PrefabType prefabType)
    {
        for (int i = listEquippedItem.Count - 1; i >= 0; i--)
        {
            if (listEquippedItem.Contains(prefabType))
            {
                listEquippedItem.Remove(prefabType);
            }
        }
        listEquippedItem.Add(prefabType);           
    }
    public void RemoveItemFromListSelect(PrefabType prefabType)
    {
        listSelectItem.Remove(prefabType);
    }
    public void RemoveItemFromListEquipped(PrefabType prefabType)
    {
        listEquippedItem.Remove(prefabType);
    }
    public void AddTypeItemToList(ItemType itemType)
    {
        for (int i = listItemType.Count - 1; i >= 0; i--)
        {
            if (listItemType.Contains(itemType))
            {
                listItemType.Remove(itemType);
            }
        }

        listItemType.Add(itemType);
    }
    public void RemoveTypeItemFromList(ItemType itemType)
    {
        listItemType.Remove(itemType);
    }
    public void SetCurrentItem(Item item)
    {
        currentItem = item;
    }
}
