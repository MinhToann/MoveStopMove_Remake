using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkinShop : UICanvas
{
    [SerializeField] ButtonHatShop btnHatShop;
    [SerializeField] ButtonPantShop btnPantShop;
    [SerializeField] ButtonAccessoryShop btnAccessoryShop;
    [SerializeField] ButtonSkinShop btnSkinShop;
    [SerializeField] Transform parentItem;
    [SerializeField] ButtonOpenCanvasShop currentButton;
    [SerializeField] List<ButtonTemplate> listButton = new List<ButtonTemplate>();
    [SerializeField] Transform parentBtn;
    private ButtonTemplate currentButtonTemplate;

    public override void Setup()
    {
        base.Setup();
        LevelManager.Ins.SetGameState(GameState.ShoppingView);
        LevelManager.Ins.ChangeViewCinemachine();
        UIManager.Ins.coinBtn.gameObject.SetActive(true);
        SetCurrentButtonTemplate(listButton[0]);
        CreateBuyButton();
        OpenHatShop();
    }
    public override void Open()
    {
        base.Open();
        LevelManager.Ins.currentPlayer.ChangeDanceAnim();
        CheckButton(ShopManager.Ins.GetCurrentSkinItem());
    }

    public void SetCurrentButtonTemplate(ButtonTemplate button)
    {
        currentButtonTemplate = button;
    }
    public void CreateBuyButton()
    {
        if (currentButtonTemplate.gameObject.activeSelf)
        {
            currentButtonTemplate.gameObject.SetActive(false);
        }
        SetCurrentButtonTemplate(listButton[0]);
        currentButtonTemplate.gameObject.SetActive(true);
    }
    public void CreateEquippedButton()
    {
        if (currentButtonTemplate.gameObject.activeSelf)
        {
            currentButtonTemplate.gameObject.SetActive(false);
        }
        SetCurrentButtonTemplate(listButton[1]);
        currentButtonTemplate.gameObject.SetActive(true);
       
    }
    public void CreateSelectButton()
    {
        if(currentButtonTemplate.gameObject.activeSelf)
        {
            currentButtonTemplate.gameObject.SetActive(false);
        }    
        SetCurrentButtonTemplate(listButton[2]);
        currentButtonTemplate.gameObject.SetActive(true);
    }
    public bool CheckEquippedItemInList(SkinItem item)
    {
        return ShopData.Ins.LoadData().listEquippedItem.Contains(item.prefabType);
    }
    public bool CheckSelectItemInList(SkinItem item)
    {
        return ShopData.Ins.LoadData().listSelectItem.Contains(item.prefabType);
    }
    public void CheckButton(SkinItem item)
    {
        
        if (!CheckEquippedItemInList(item) && !CheckSelectItemInList(item))
        {
            CreateBuyButton();
            currentButtonTemplate.SetCoin(item.cost);
        }
        else
        {
            if (CheckEquippedItemInList(item))
            {
                CreateEquippedButton();
            }
            else if (CheckSelectItemInList(item))
            {
                CreateSelectButton();
            }
        }
    }
    public ButtonTemplate GetButtonTemplate()
    {
        return currentButtonTemplate;
    }    
    public void SetCurrentButtonOpen(ButtonOpenCanvasShop button)
    {
        if(currentButton != null)
        {
            currentButton.SetOriginalColor();
        }            
        currentButton = button;
        currentButton.SetNewColorImage(Color.white);
    }    
    public void SetValueForButton(ItemType itemType, ButtonOpenCanvasShop button)
    {
        ShopManager.Ins.SpawnSkinItemShop(itemType, parentItem);       
        SetCurrentButtonOpen(button);
        CheckButton(ShopManager.Ins.GetCurrentSkinItem());
    }    
    public void OpenHatShop()
    {
        SetValueForButton(ItemType.Hat, btnHatShop);
    }   
    public void OpenPantShop()
    {
        SetValueForButton(ItemType.Pant, btnPantShop);
    }    

    public void OpenAccessoryShop()
    {
        SetValueForButton(ItemType.Accessory, btnAccessoryShop);
    }    

    public void OpenSkinShop()
    {
        SetValueForButton(ItemType.Skin, btnSkinShop);
    }    
    public void CloseUI()
    {
        listButton[1].gameObject.SetActive(false);
        Close(0);
        LevelManager.Ins.currentPlayer.OnInit();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
    }
    public void BuyItem()
    {       
        SkinItem curItem = (SkinItem)ShopData.Ins.LoadData().currentItem;
        if (PlayerData.Ins.LoadData().coin >= curItem.cost)
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
            PlayerData.Ins.SetSkinType(curItem.prefabType);
            PlayerData.Ins.SetColorType(curItem.colorType);
            ShopData.Ins.AddTypeItemToList(curItem.itemType);
            ShopData.Ins.AddItemToListEquipped(curItem.prefabType);
            PlayerData.Ins.DecreaseCoin(curItem.cost);
            UIManager.Ins.SetCoin(PlayerData.Ins.LoadData().coin);
            CreateEquippedButton();
            if (curItem.itemType == ItemType.Hat)
            {
                LevelManager.Ins.currentPlayer.ChangeHat(curItem.prefabType);
                PlayerData.Ins.SetHatType(curItem.prefabType);
            }
            else if (curItem.itemType == ItemType.Skin)
            {
                LevelManager.Ins.currentPlayer.ChangeCharacterModel(curItem.prefabType);
                PlayerData.Ins.SetModelType(curItem.prefabType);
            }
            else if(curItem.itemType == ItemType.Pant)
            {
                PlayerData.Ins.SetColorPantType(curItem.colorType);
                LevelManager.Ins.currentPlayer.ChangePant(curItem.colorType);
                PlayerData.Ins.SetPantType(curItem.prefabType);
                
            }     
        }     
    }
    public void SelectItem()
    {
        SkinItem curItem = (SkinItem)ShopData.Ins.LoadData().currentItem;
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
        PlayerData.Ins.SetSkinType(curItem.prefabType);
        PlayerData.Ins.SetColorType(curItem.colorType);
        ShopData.Ins.AddTypeItemToList(curItem.itemType);
        ShopData.Ins.AddItemToListEquipped(curItem.prefabType);
        CreateEquippedButton();
        if (curItem.itemType == ItemType.Hat)
        {
            LevelManager.Ins.currentPlayer.ChangeHat(curItem.prefabType);
            PlayerData.Ins.SetHatType(curItem.prefabType);
        }
        else if (curItem.itemType == ItemType.Skin)
        {
            LevelManager.Ins.currentPlayer.ChangeCharacterModel(curItem.prefabType);
            PlayerData.Ins.SetModelType(curItem.prefabType);
        }
        else if (curItem.itemType == ItemType.Pant)
        {
            PlayerData.Ins.SetColorPantType(curItem.colorType);
            LevelManager.Ins.currentPlayer.ChangePant(curItem.colorType);
            PlayerData.Ins.SetColorType(curItem.colorType);
        }
    }    
    public void EquippedItem()
    {
        CloseUI();
    }    
    public void OnClickItem(SkinItem item)
    {
        
        if (item.itemType == ItemType.Hat)
        {
            LevelManager.Ins.currentPlayer.ChangeTemporaryPlayerHat(item.prefabType);
        }
        else if (item.itemType == ItemType.Skin)
        {
            LevelManager.Ins.currentPlayer.ChangeTemporaryPlayerModel(item.prefabType);
        }
        else if (item.itemType == ItemType.Pant)
        {
            LevelManager.Ins.currentPlayer.ChangeTemporaryPlayerPant(item.colorType);
        }
        SkinItem previousItem = ShopManager.Ins.GetCurrentSkinItem();
        previousItem.imgPickFrame.gameObject.SetActive(false);
        ShopData.Ins.SetCurrentItem(item);               
        ShopManager.Ins.SetCurrentSkinItemInShop(item);
        item.imgPickFrame.gameObject.SetActive(true);
        CheckButton(item);
    }
}
