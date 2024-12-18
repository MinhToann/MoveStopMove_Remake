using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    float originalValue;
    [SerializeField] Material material;
    [SerializeField] Renderer objRenderer;
    private float fadeSpeed = 10f;
    private float fadeAmount = 0.3f;
    private void Start()
    {
        material = objRenderer.material;
        originalValue = material.color.a;
    }
    private void Update()
    {
        if(LevelManager.Ins.currentPlayer != null)
        {
            if (Vector3.Distance(transform.position, LevelManager.Ins.currentPlayer.TF.position) <= LevelManager.Ins.currentPlayer.currentModel.maxRadius)
            {
                FadeNow();
            }
            else
            {
                ResetFade();
            }
        }
        else
        {
            ResetFade();
        }
    }
    public void FadeNow()
    {
        Color currentColor = material.color;
        Color fadeColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
        material.color = fadeColor;
    }
    public void ResetFade()
    {
        Color currentColor = material.color;
        Color fadeColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalValue, fadeSpeed * Time.deltaTime));
        material.color = fadeColor;
    }
}
