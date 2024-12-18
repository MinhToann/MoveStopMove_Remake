using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterModel : SkinItem
{
    [field: SerializeField] public Transform leftHand { get; private set; }
    [field: SerializeField] public Transform rightHand { get; private set; }
    [field: SerializeField] public Transform Head { get; private set; }
    [field: SerializeField] public Transform hipBone { get; private set; }
    [field: SerializeField] public Transform Back { get; private set; }
    [field: SerializeField] public Animator animModel { get; private set; }
    [field: SerializeField] public Transform newHandTF { get; private set; }
    [field: SerializeField] public Pant currentPant { get; private set; }
    [field: SerializeField] public SkinnedMeshRenderer skinMesh { get; private set; }
    [field: SerializeField] public Hat currentHat { get; private set; }
    [field: SerializeField] public Wings currentWing { get; private set; }
    [field: SerializeField] public LeftHandItem currentLeftHandItem { get; private set; }
    [field: SerializeField] public Tail currentTail { get; private set; }
    
    [SerializeField] LineRenderer lineRenderer;
    [field: SerializeField] public int subVisions { get; private set; } = 100;
    [field: SerializeField] public float radius { get; private set; } = 5f;
    private string currentAnim;
    [field: SerializeField] public float maxRadius { get; private set; } = 6.5f;

    public void DrawCircleLine()
    {
        float angleStep = 2f * Mathf.PI / subVisions;
        lineRenderer.positionCount = subVisions;
        for(int i = 0; i < subVisions; i++)
        {
            float xPos = radius * Mathf.Cos(angleStep * i);
            float zPos = radius * Mathf.Sin(angleStep * i);

            Vector3 pointInCircle = new Vector3(xPos, 0, zPos);
            lineRenderer.SetPosition(i, pointInCircle);
           
        }    
    }
    public void SetSizeRadius(float size)
    {
        radius = size;
    }
    public void UpSizeRadius(float size)
    {
        this.radius += size;
    }
    public float GetXScale => lineRenderer.transform.localScale.x;
    public float GetZScale => lineRenderer.transform.localScale.z;
    
    public void ActiveLine()
    {
        lineRenderer.gameObject.SetActive(true);
    }
    public void DeactiveLine()
    {
        lineRenderer.gameObject.SetActive(false);
    }  
    
    public void ChangeAnim(string newAnim)
    {
        if(currentAnim != newAnim)
        {
            animModel.ResetTrigger(currentAnim);
            currentAnim = newAnim;
            animModel.SetTrigger(currentAnim);
        }
    }
}
