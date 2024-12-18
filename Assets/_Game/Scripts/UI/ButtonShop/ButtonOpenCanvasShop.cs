using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOpenCanvasShop : MonoBehaviour
{
    [SerializeField] protected Image image;
    [SerializeField] protected Button btn;
    public void SetNewColorImage(Color color)
    {
        image.color = color;
        Color colorBtn = btn.image.color;
        colorBtn.a = 0;
        btn.image.color = colorBtn;
    }
    public void SetOriginalColor()
    {
        Color colorBtn = btn.image.color;
        colorBtn.a = 1;
        btn.image.color = colorBtn;
        image.color = Color.HSVToRGB(115, 115, 1);
    }    
}
