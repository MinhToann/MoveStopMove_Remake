using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            if(tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }    
    [field: SerializeField] public ItemType itemType { get; private set; }
    [field: SerializeField] public PrefabType prefabType { get; private set; }
    [field: SerializeField] public ColorType colorType { get; private set; }
    [field: SerializeField] public string titleName { get; private set; }
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public string description { get; private set; }

    public virtual void SetValueItem(ItemType itemType, PrefabType prefabType, ColorType colorType, string titleName, int cost, string description)
    {
        this.itemType = itemType;
        this.prefabType = prefabType;
        this.colorType = colorType;
        this.titleName = titleName;
        this.cost = cost;
        this.description = description;
    }
    public void SetPrefabType(PrefabType prefabType)
    {
        this.prefabType = prefabType;
    }    
    public void SetColorType(ColorType colorType)
    {
        this.colorType = colorType;
    }
}
