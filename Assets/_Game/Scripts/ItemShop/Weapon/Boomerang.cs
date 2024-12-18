using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Bullet
{
    [SerializeField] BoomerangBullet boomerangObj;

    public override void OnInit()
    {
        base.OnInit();
        AudioManager.Ins.PlayThrowWeaponAudio();
    }
    public override void Throw()
    {
        boomerangObj.transform.Rotate(0, 20, 0, Space.World);
        
        base.Throw();
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(Cache.CharacterCollider(other))
        {
            ReturnBullet(Cache.CharacterCollider(other), PrefabType.Boomerang);
        }
               
    }
}
