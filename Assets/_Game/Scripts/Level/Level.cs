using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [field: SerializeField] public Transform startPos { get; private set; }
    [field: SerializeField] public Transform xPos { get; private set; }
    [field: SerializeField] public Transform zPos { get; private set; }

    public Vector3 GetRandomPositionOnMap()
    {
        float xLeft = -xPos.position.x;
        float xRight = xPos.position.x;
        float zForward = zPos.position.z;
        float zBack = -zPos.position.z;
        return new Vector3(Random.Range(xLeft, xRight), startPos.position.y, Random.Range(zBack, zForward));
    }
    public Vector3 GetRandomPositionSpawnGiftOnMap()
    {
        float xLeft = -xPos.position.x;
        float xRight = xPos.position.x;
        float zForward = zPos.position.z;
        float zBack = -zPos.position.z;
        return new Vector3(Random.Range(xLeft, xRight), startPos.position.y + 10f, Random.Range(zBack, zForward));
    }
}

