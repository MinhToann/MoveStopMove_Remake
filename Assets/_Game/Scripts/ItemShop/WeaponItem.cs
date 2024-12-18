using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : Item
{
    [SerializeField] Text textTitle;
    [SerializeField] Text textInfo;
    [SerializeField] RawImage rawImg;
    [SerializeField] Transform camParentImg;
    private RawImage currrentImg;
    [SerializeField] Camera viewWeaponCamera;
    private WeaponBase currentWeapon;
    [field: SerializeField] public ItemSO itemSO { get; private set; }
    private int index;
    
    public override void SetValueItem(ItemType itemType, PrefabType prefabType, ColorType colorType, string titleName, int cost, string description)
    {
        base.SetValueItem(itemType, prefabType, colorType, titleName, cost, description);
    }
    public void SetTitleText(string text)
    {
        textTitle.text = text;
    }
    public void SetInfomationText(string text)
    {
        textInfo.text = text;
    }    
    public void SetIndex(int index)
    {
        this.index = index;
    }
    public int GetIndex()
    {
        return index;
    }
    public void OnDespawn()
    {
        if (currrentImg != null)
        {
            Destroy(currrentImg.gameObject);
        }
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
    }    
    public void OnDestroyItem()
    {
        Destroy(this.gameObject);
    }
    public void CreateImage(int index)
    {
        if(index >= 0 || index <= itemSO.listWeaponItem.Count)
        {
            OnDespawn();
        }
        
        if(index < itemSO.listWeaponItem.Count)
        {
            currrentImg = Instantiate(rawImg, transform);
            currentWeapon = Instantiate((WeaponBase)itemSO.listWeaponItem[index], camParentImg);
            currentWeapon.TF.localPosition = Vector3.zero;
            currentWeapon.TF.localRotation = Quaternion.identity;
            currentWeapon.TF.localScale = Vector3.one;
            currrentImg.texture = viewWeaponCamera.targetTexture;
            SetIndex(index);
        }
        
    }
    public WeaponBase GetCurrentWeapon()
    {
        return currentWeapon;
    }    
}
