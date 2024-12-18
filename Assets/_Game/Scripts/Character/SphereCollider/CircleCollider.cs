using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    [SerializeField] Character character;
    public void ColliderWithCharacter(Character character)
    {
        this.character.AddTarget(character);
        this.character.CheckIsTargetInRange(character);
    }
    public void ExitFromCharacter(Character character)
    {
        if (character is Bot)
        {
            Bot bot = (Bot)character;
            bot.DeactiveImage();
        }
        this.character.RemoveTarget(character);
        this.character.SetTarget(null);
    }    
    private void OnTriggerEnter(Collider other)
    {
        if (Cache.CharacterCollider(other))
        {
            ColliderWithCharacter(Cache.CharacterCollider(other));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Cache.CharacterCollider(other))
        {
            ExitFromCharacter(Cache.CharacterCollider(other));
        }
    }
}

