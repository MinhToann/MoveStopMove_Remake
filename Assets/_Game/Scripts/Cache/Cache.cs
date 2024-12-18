using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour
{
    private static Dictionary<Collider, Character> dicColliderCharacter = new Dictionary<Collider, Character>();
    private static Dictionary<Collider, CircleCollider> dicColliderSphere = new Dictionary<Collider, CircleCollider>();
    private static Dictionary<Collider, Walls> dicWalls = new Dictionary<Collider, Walls>();

    public static Character CharacterCollider(Collider collider)
    {
        if(!dicColliderCharacter.ContainsKey(collider))
        {
            Character character = collider.GetComponent<Character>();
            dicColliderCharacter.Add(collider, character);
        }    
        return dicColliderCharacter[collider];
    }    
    public static CircleCollider SphereCollider(Collider collider)
    {
        if(!dicColliderSphere.ContainsKey(collider))
        {
            CircleCollider sphere = collider.GetComponent<CircleCollider>();
            dicColliderSphere.Add(collider, sphere);
        }
        return dicColliderSphere[collider];
    }
    public static Walls WallCollider(Collider collider)
    {
        if (!dicWalls.ContainsKey(collider))
        {
            Walls wall = collider.GetComponent<Walls>();
            dicWalls.Add(collider, wall);
        }
        return dicWalls[collider];
    }
}
