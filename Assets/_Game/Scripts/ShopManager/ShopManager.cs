using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] ItemSO itemSO;
    [SerializeField] WeaponItem weaponItem;
    [SerializeField] SkinItem skinItem;
    private WeaponItem curWeaponItem;
    private SkinItem curSkinItem;
    [SerializeField] List<SkinItem> listSkinItem = new List<SkinItem>();
    [SerializeField] List<WeaponItem> listWeaponItem = new List<WeaponItem>();
    public void SpawnWeaponInShop(Transform tf)
    {        
        WeaponItem weaponItem = Instantiate(this.weaponItem, tf);
        weaponItem.SetValueItem(itemSO.listWeaponItem[0].itemType, itemSO.listWeaponItem[0].prefabType, itemSO.listWeaponItem[0].colorType, itemSO.listWeaponItem[0].titleName, itemSO.listWeaponItem[0].cost, itemSO.listWeaponItem[0].description);
        weaponItem.SetTitleText(weaponItem.titleName);
        weaponItem.SetInfomationText(weaponItem.description);
        weaponItem.CreateImage(0);        
        SetCurrentWeaponInShop(weaponItem);
        listWeaponItem.Add(weaponItem);
    }    
    public void SetValueWeaponItem(int index)
    {
        curWeaponItem.SetValueItem(itemSO.listWeaponItem[index].itemType, itemSO.listWeaponItem[index].prefabType, itemSO.listWeaponItem[index].colorType, itemSO.listWeaponItem[index].titleName, itemSO.listWeaponItem[index].cost, itemSO.listWeaponItem[index].description);
        curWeaponItem.SetTitleText(curWeaponItem.titleName);
        curWeaponItem.SetInfomationText(curWeaponItem.description);
    }

    public void SpawnSkinItemShop(ItemType itemType, Transform tf)
    {
        if (listSkinItem.Count > 0)
        {
            for (int i = 0; i < listSkinItem.Count; i++)
            {
                Destroy(listSkinItem[i].gameObject);
            }
            listSkinItem.Clear();
        }
        for (int i = 0; i < itemSO.listSkinItem.Count; i++)
        {
            SkinItem skinItem = Instantiate(this.skinItem);
            skinItem.SetValueItem(itemSO.listSkinItem[i].itemType, itemSO.listSkinItem[i].prefabType, itemSO.listSkinItem[i].colorType,itemSO.listSkinItem[i].titleName, itemSO.listSkinItem[i].cost, itemSO.listSkinItem[i].description);
            skinItem.SetImage(itemSO.listPrefabSO[i].spriteImage);
            skinItem.imgPickFrame.gameObject.SetActive(false);
            if(skinItem.itemType == itemType)
            {
                skinItem.transform.SetParent(tf);
                listSkinItem.Add(skinItem);
                listSkinItem[0].imgPickFrame.gameObject.SetActive(true); 
                SetCurrentSkinItemInShop(listSkinItem[0]);
                //ShopData.Ins.SetCurrentItem(listSkinItem[0]);
            }    
            else
            {
                Destroy(skinItem.gameObject);
                listSkinItem.Remove(skinItem);
            }
        }

    }    

    public void SetCurrentSkinItemInShop(SkinItem skinItem)
    {
        this.curSkinItem = skinItem;
    }    
    public SkinItem GetCurrentSkinItem()
    {
        return this.curSkinItem;
    }    
    public void SetCurrentWeaponInShop(WeaponItem weaponItem)
    {
        this.curWeaponItem = weaponItem;
    }
    public WeaponItem GetCurrentWeaponItem()
    {
        return curWeaponItem;
    }
}
