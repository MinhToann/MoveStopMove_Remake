using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasWeaponShop : UICanvas
{
    [SerializeField] Transform parentItem;
    [SerializeField] ItemSO itemSO;
    [SerializeField] List<ButtonTemplate> listButton = new List<ButtonTemplate>();
    [SerializeField] Transform parentBtn;
    private ButtonTemplate currentButton;
    public override void Setup()
    {
        base.Setup();
        LevelManager.Ins.SetGameState(GameState.ShoppingView);
        UIManager.Ins.coinBtn.gameObject.SetActive(true);

    }
    public override void Open()
    {
        base.Open();
        ShopManager.Ins.SpawnWeaponInShop(parentItem);
        LevelManager.Ins.currentPlayer.gameObject.SetActive(false);        
        SetCurrentButton(listButton[0]);
        CreateBuyButton();
        CheckButton();
        Debug.Log(currentButton);
    }
    public void CloseUI()
    {
        ShopManager.Ins.GetCurrentWeaponItem().OnDestroyItem();
        listButton[1].gameObject.SetActive(false);
        Close(0);
        //LevelManager.Ins.currentPlayer.OnInit();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
    }
    public void SetCurrentButton(ButtonTemplate button)
    {
        currentButton = button;
    }    
    public void CreateBuyButton()
    {
        if (currentButton.gameObject.activeSelf)
        {
            currentButton.gameObject.SetActive(false);           
        }
        SetCurrentButton(listButton[0]);
        currentButton.gameObject.SetActive(true);
    }    
    public void CreateEquippedButton()
    {
        if (currentButton.gameObject.activeSelf)
        {
            currentButton.gameObject.SetActive(false);           
        }
        SetCurrentButton(listButton[1]);
        currentButton.gameObject.SetActive(true);
    }    
    public void CreateSelectButton()
    {
        if(currentButton.gameObject.activeSelf)
        {
            currentButton.gameObject.SetActive(false);           
        }
        SetCurrentButton(listButton[2]);
        currentButton.gameObject.SetActive(true);
    }    
    public void PreviousItem()
    {
        if (ShopManager.Ins.GetCurrentWeaponItem().GetIndex() > 0 && ShopManager.Ins.GetCurrentWeaponItem().GetIndex() < itemSO.listWeaponItem.Count)
        {
            ShopManager.Ins.SetValueWeaponItem(ShopManager.Ins.GetCurrentWeaponItem().GetIndex() - 1);
            PlayerData.Ins.SetTemporaryWeaponType(ShopManager.Ins.GetCurrentWeaponItem().prefabType);          
            ShopManager.Ins.GetCurrentWeaponItem().CreateImage(ShopManager.Ins.GetCurrentWeaponItem().GetIndex() - 1);
            CheckButton();
        }
    }
    public void NextItem()
    {
        if (ShopManager.Ins.GetCurrentWeaponItem().GetIndex() < itemSO.listWeaponItem.Count - 1)
        {          
            ShopManager.Ins.SetValueWeaponItem(ShopManager.Ins.GetCurrentWeaponItem().GetIndex() + 1);
            PlayerData.Ins.SetTemporaryWeaponType(ShopManager.Ins.GetCurrentWeaponItem().prefabType);          
            ShopManager.Ins.GetCurrentWeaponItem().CreateImage(ShopManager.Ins.GetCurrentWeaponItem().GetIndex() + 1);
            ShopManager.Ins.SetValueWeaponItem(ShopManager.Ins.GetCurrentWeaponItem().GetIndex());
            CheckButton();
        }
    }
    public bool CheckEquippedItemInList()
    {
        return ShopData.Ins.LoadData().listEquippedItem.Contains(ShopManager.Ins.GetCurrentWeaponItem().prefabType);
    }
    public bool CheckSelectItemInList()
    {
        return ShopData.Ins.LoadData().listSelectItem.Contains(ShopManager.Ins.GetCurrentWeaponItem().prefabType);
    }
    public void CheckButton()
    {
        if (!CheckEquippedItemInList() && !CheckSelectItemInList())
        {
            CreateBuyButton();
            currentButton.SetCoin(ShopManager.Ins.GetCurrentWeaponItem().cost);
        }
        else
        {
            if (CheckEquippedItemInList())
            {
                CreateEquippedButton();
            }
            if (CheckSelectItemInList())
            {
                CreateSelectButton();
            }
        }
    }    
    public void BuyItem()
    {
        WeaponItem curItem = ShopManager.Ins.GetCurrentWeaponItem();
        if(PlayerData.Ins.LoadData().coin >= curItem.cost)
        {
            for (int j = ShopData.Ins.LoadData().listItemType.Count - 1; j >= 0; j--)
            {
                if (curItem.itemType == ShopData.Ins.LoadData().listItemType[j])
                {
                    ShopData.Ins.AddItemToListSelect(ShopData.Ins.LoadData().listEquippedItem[j]);
                    ShopData.Ins.RemoveItemFromListEquipped(ShopData.Ins.LoadData().listEquippedItem[j]);
                    ShopData.Ins.LoadData().listItemType.RemoveAt(j);

                }
            }
            PlayerData.Ins.SetWeaponType(curItem.prefabType);
            ShopData.Ins.AddTypeItemToList(curItem.itemType);
            ShopData.Ins.AddItemToListEquipped(curItem.prefabType);
            CreateEquippedButton();
            PlayerData.Ins.DecreaseCoin(curItem.cost);
            UIManager.Ins.SetCoin(PlayerData.Ins.LoadData().coin);
            LevelManager.Ins.currentPlayer.ChangeWeapon(curItem.prefabType);
            PlayerData.Ins.SetWeaponType(curItem.prefabType);
        }      
    }
    public void SelectItem()
    {
        WeaponItem curItem = ShopManager.Ins.GetCurrentWeaponItem();
        if(ShopData.Ins.LoadData().listSelectItem.Contains(curItem.prefabType))
        {
            ShopData.Ins.RemoveItemFromListSelect(curItem.prefabType);
        }
        for (int j = ShopData.Ins.LoadData().listItemType.Count - 1; j >= 0; j--)
        {
            if (curItem.itemType == ShopData.Ins.LoadData().listItemType[j])
            {
                ShopData.Ins.AddItemToListSelect(ShopData.Ins.LoadData().listEquippedItem[j]);
                ShopData.Ins.RemoveItemFromListEquipped(ShopData.Ins.LoadData().listEquippedItem[j]);
                ShopData.Ins.LoadData().listItemType.RemoveAt(j);
            }
        }
        PlayerData.Ins.SetWeaponType(curItem.prefabType);
        ShopData.Ins.AddTypeItemToList(curItem.itemType);
        ShopData.Ins.AddItemToListEquipped(curItem.prefabType);
        CreateEquippedButton();
        LevelManager.Ins.currentPlayer.ChangeWeapon(PlayerData.Ins.LoadData().weaponType);
        PlayerData.Ins.SetWeaponType(PlayerData.Ins.LoadData().weaponType);
    }    
    public void EquippedItem()
    {
        CloseUI();
        
    }    
}
