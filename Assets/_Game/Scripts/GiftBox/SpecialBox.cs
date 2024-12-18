using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBox : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshCollider meshCollider;
    [SerializeField] ItemSO itemSO;
    private void DeactiveGravity()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
    }
    private void ActiveMeshTrigger()
    {
        meshCollider.isTrigger = true;
    }
    private void Update()
    {
        if(LevelManager.Ins.GetGameState() == GameState.Gameplay)
        {
            if (rb.velocity.y >= 0 && transform.position.y <= LevelManager.Ins.GetLevel().startPos.position.y)
            {
                DeactiveGravity();
                ActiveMeshTrigger();
            }
        }
        
    }
    private void OnDespawn()
    {
        LevelManager.Ins.OnDespawnBox(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Cache.CharacterCollider(other))
        {
            //Up attack range
            Cache.CharacterCollider(other).isEatBox = true;
            Cache.CharacterCollider(other).SetSizeAttack();
            OnDespawn();
        }
    }
}
