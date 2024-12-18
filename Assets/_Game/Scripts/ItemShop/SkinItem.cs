using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinItem : Item
{
    [field: SerializeField] public Sprite spriteImage { get; private set; }
    [SerializeField] Image image;
    [field: SerializeField] public Image imgPickFrame { get; private set; }
    public override void SetValueItem(ItemType itemType, PrefabType prefabType, ColorType colorType, string titleName, int cost, string description)
    {
        base.SetValueItem(itemType, prefabType, colorType,titleName, cost, description);
    }
    public void SetImage(Sprite spriteImage)
    {
        this.spriteImage = spriteImage;
        SetIconItem(spriteImage);
    }
    public void SetIconItem(Sprite spriteImage)
    {
        image.sprite = spriteImage;
    }    
       
    public void OnClickItem()
    {
        CanvasSkinShop canvas = UIManager.Ins.GetUI<CanvasSkinShop>();
        canvas.OnClickItem(this);
        
    }    
}
