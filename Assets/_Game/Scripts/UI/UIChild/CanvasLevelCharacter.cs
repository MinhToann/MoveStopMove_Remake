using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CanvasLevelCharacter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textLevel;
    private int initLevel = 1;
    private int numberLevel;
    public int NumberLevel => numberLevel;
    private void Awake()
    {
        numberLevel = initLevel;
    }

    public void SetTextLevel(int level)
    {
        textLevel.text = level.ToString();
    }    
    public void SetNumberLevel(int level)
    {
        numberLevel = level;
    }    
    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
