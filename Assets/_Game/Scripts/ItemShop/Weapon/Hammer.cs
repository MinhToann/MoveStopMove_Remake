using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Bullet
{
    [SerializeField] HammerBullet hammerObj;

    public override void OnInit()
    {
        base.OnInit();
        AudioManager.Ins.PlayThrowWeaponAudio();
    }
    public override void Throw()
    {
        hammerObj.transform.Rotate(0, 20, 0, Space.World);
        
        base.Throw();
        
    }
}
