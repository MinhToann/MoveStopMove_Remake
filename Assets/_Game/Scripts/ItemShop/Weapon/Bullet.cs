using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : GameUnit
{
    private Character character;
    protected float throwSpeed = 8f;
    [SerializeField] Rigidbody rb;
    private Vector3 localScale;
    public Character Character => character;
    public virtual void Start()
    {

    }
    public virtual void OnInit()
    {

    }
     void Update()
    {
        Throw();
    }


    public virtual void Throw()
    {       
        TF.position += TF.forward * throwSpeed * Time.deltaTime;    
        
    }
    public void SetCharacter(Character character)
    {
        this.character = character;
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if(Cache.CharacterCollider(other))
        {
            CharacterCollider(Cache.CharacterCollider(other));
            
        }
        if (Cache.WallCollider(other))
        {
            WallCollider();
        }
    }
    private void CharacterCollider(Character character)
    {
        if (character != this.character)
        {
            character.OnHit(100f);
            this.character.UpSize();
            if (this.character is Player)
            {
                LevelManager.Ins.SetViewGameplayCamera();
                PlayerData.Ins.IncreaseCoin(1);
                LevelManager.Ins.currentPlayer.IncreaseCoin(1);
            }
            else
            {          
                LevelManager.Ins.currentPlayer.SetKillerName(this.character.name);
            }
            OnDespawn();
        }
        
    }
    private void WallCollider()
    {
        OnDespawn();
    }

    public void ReturnBullet(Character character, PrefabType prefabType)
    {
        if(character != this.character)
        {
            Bullet bullet = SimplePool.Spawn<Bullet>(prefabType, character.TF.position, character.TF.rotation);
            character.OnHit(0);
            bullet.OnInit();
            Invoke(nameof(OnDespawn), 1f);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(Cache.SphereCollider(other) == character.rangeCollider)
        {
            OnDespawn();
        }
        
    }
}
