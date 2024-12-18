using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Bullet
{
    [SerializeField] AxeBullet axeObj;

    public override void OnInit()
    {
        base.OnInit();
        AudioManager.Ins.PlayThrowWeaponAudio();
    }
    public override void Throw()
    {
        axeObj.transform.Rotate(0, 20, 0, Space.World);
        
        base.Throw();
    }
}
