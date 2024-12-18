using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "PrefabSO")]
public class PrefabItemSO : ScriptableObject
{
    [field: SerializeField] public ItemType itemType { get; private set; }
    [field: SerializeField] public PrefabType prefabType { get; private set; }
    [field: SerializeField] public ColorType colorType { get; private set; }
    [field: SerializeField] public string titleName { get; private set; }
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public string description { get; private set; }
    [field: SerializeField] public Sprite spriteImage { get; private set; }
   
}
