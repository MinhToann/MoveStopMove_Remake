using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : WeaponItem
{
    private Character character;
    Bullet bullet;
    public void SetCharacter(Character character)
    {
        this.character = character;
    }
    public void SpawnBullet()
    {
        if (character.GetCoolDownThrow <= 0)
        {
            bullet = SimplePool.Spawn<Bullet>(character.currentWeapon.prefabType, character.bulletPoint.position, character.TF.rotation);
            bullet.SetCharacter(character);
            bullet.OnInit();
            if(character.isEatBox)
            {
                bullet.transform.localScale *= 2.5f;
                character.isEatBox = false;
                this.character.SetNormalSizeAttack();
                Invoke(nameof(DelayDecreaseScaleBullet), 1f);
            }
        }
    }
    private void DelayDecreaseScaleBullet()
    {
        bullet.TF.localScale /= 2.5f;
    }
}
