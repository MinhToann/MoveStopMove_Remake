using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public List<PrefabItemSO> listPrefabSO { get; private set; } = new List<PrefabItemSO>();
    [field: SerializeField] public List<Item> listShopItem { get; private set; } = new List<Item>();
    [field: SerializeField] public List<WeaponItem> listWeaponItem { get; private set; } = new List<WeaponItem>();
    [field: SerializeField] public List<SkinItem> listSkinItem { get; private set; } = new List<SkinItem>();
    [field: SerializeField] public List<Hat> listHatItem { get; private set; } = new List<Hat>();
    [field: SerializeField] public List<CharacterModel> listModel { get; private set; } = new List<CharacterModel>();
    [field: SerializeField] public List<Material> listMaterials { get; private set; } = new List<Material>();

    public ItemType GetItemType(int index)
    {
        return listPrefabSO[index].itemType;
    }    
    public PrefabType GetPrefabTypeItem(int index)
    {
        return listPrefabSO[index].prefabType;
    }    
    public ColorType GetColorTypeItem(int index)
    {
        return listPrefabSO[index].colorType;
    }
    public string GetItemName(int index)
    {
        return listPrefabSO[index].titleName;
    }    
    public int GetCostItem(int index)
    {
        return listPrefabSO[index].cost;
    }    
    public string GetDescriptionItem(int index)
    {
        return listPrefabSO[index].description;
    }    
    public Sprite GetImageItem(int index)
    {
        return listPrefabSO[index].spriteImage;
    }    
    public void SetValueItem()
    {
        for(int i = 0; i < listShopItem.Count; i++)
        {
            listShopItem[i].SetValueItem(listPrefabSO[i].itemType, listPrefabSO[i].prefabType, listPrefabSO[i].colorType,listPrefabSO[i].titleName, listPrefabSO[i].cost, listPrefabSO[i].description);
        }    
    }    
    public WeaponItem ChangeWeaponItem(PrefabType prefabType)
    {
        WeaponItem weaponItem = new WeaponItem();
        for(int i = 0; i < listWeaponItem.Count; i++)
        {
            if(listWeaponItem[i].prefabType == prefabType)
            {
                weaponItem = listWeaponItem[i];
            }    
        }
        return weaponItem;
    }    
    public SkinItem ChangeSkinItem(PrefabType prefabType)
    {
        SkinItem skinItem = new SkinItem();
        for(int i = 0; i < listSkinItem.Count; i++)
        {
            if (listSkinItem[i].prefabType == prefabType)
            {
                skinItem = listSkinItem[i];
            }    
        }
        return skinItem;
    }    
    public Material ChangeMaterial(ColorType colorType)
    {
        return listMaterials[(int)colorType];
    }
}
