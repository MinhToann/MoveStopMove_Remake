using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTemplate : MonoBehaviour
{
    [SerializeField] private Text textCoin;

    private void Awake()
    {

    }
    public void SetCoin(int coin)
    {
        textCoin.text = coin.ToString();
    }    
    public void OnClickBuyWeaponItem()
    {
        CanvasWeaponShop canvasWeapon = UIManager.Ins.GetUI<CanvasWeaponShop>();
        canvasWeapon.BuyItem();
    }    
    public void OnClickSelectWeaponItem()
    {
        CanvasWeaponShop canvasWeapon = UIManager.Ins.GetUI<CanvasWeaponShop>();
        canvasWeapon.SelectItem();
    }    
    public void OnClickEquippedWeaponItem()
    {
        CanvasWeaponShop canvasWeapon = UIManager.Ins.GetUI<CanvasWeaponShop>();
        canvasWeapon.EquippedItem();
    }    
    public void OnClickBuySkinItem()
    {
        CanvasSkinShop canvasSkin = UIManager.Ins.GetUI<CanvasSkinShop>();
        canvasSkin.BuyItem();
    }    
    public void OnClickSelectSkinItem()
    {
        CanvasSkinShop canvasSkin = UIManager.Ins.GetUI<CanvasSkinShop>();
        canvasSkin.SelectItem();
    }    
    public void OnClickEquippedSkinItem()
    {
        CanvasSkinShop canvasSkin = UIManager.Ins.GetUI<CanvasSkinShop>();
        canvasSkin.EquippedItem();
    }    
}
