using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        //UIManager.Ins.CloseUI<JoystickControl>(0.1f);
        Debug.Log("Deactive");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //UIManager.Ins.OpenUI<JoystickControl>();
        Debug.Log("Active");
    }
}
