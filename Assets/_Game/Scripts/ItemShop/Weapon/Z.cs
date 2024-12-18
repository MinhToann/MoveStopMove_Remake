using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z : Bullet
{
    [SerializeField] ZBullet ZObject;
    public override void OnInit()
    {
        base.OnInit();
        AudioManager.Ins.PlayThrowWeaponAudio();
    }
    public override void Throw()
    {
        ZObject.transform.Rotate(0, 20, 0, Space.World);
        
        base.Throw();
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (Cache.CharacterCollider(other))
        {
            ReturnBullet(Cache.CharacterCollider(other), PrefabType.Z);
        }

    }
}
