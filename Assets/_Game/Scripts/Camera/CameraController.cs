using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float lerpSpeed = 5f;
    private Transform tf;

    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
    private void LateUpdate()
    {
        if (LevelManager.Ins.currentPlayer != null)
        {
            transform.position = Vector3.Lerp(TF.position, LevelManager.Ins.currentPlayer.TF.position + new Vector3(0, 0, -10f), lerpSpeed * Time.deltaTime);
        }    
            
    }
}
