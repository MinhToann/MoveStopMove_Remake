using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pant : SkinItem
{
    [field: SerializeField] public SkinnedMeshRenderer skinMesh { get; private set; }

    public void SetSkinMesh(Material mat)
    {
        skinMesh.material = mat;
    }    
}
