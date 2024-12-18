using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePool : MonoBehaviour
{
    private static Dictionary<PrefabType, Pool> dicGU = new Dictionary<PrefabType, Pool>();
    public static void Preload(GameUnit prefab, int amout, Transform parent)
    {
        if (prefab == null)
        {
            Debug.Log("Preload Error !!!");
            return;
        }
        if (!dicGU.ContainsKey(prefab.type) || dicGU[prefab.type] == null)
        {
            Pool p = new Pool();
            p.Preload(prefab, parent);
            dicGU[prefab.type] = p;
        }

    }
    public static T Spawn<T>(PrefabType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!dicGU.ContainsKey(poolType))
        {
            Debug.Log("Spawn Error !!!");
        }
        return dicGU[poolType].Spawn(pos, rot) as T;
    }
    public static void Despawn(GameUnit prefab)
    {
        if (!dicGU.ContainsKey(prefab.type))
        {
            Debug.Log("Despawn Error !!!");
        }
        dicGU[prefab.type].Despawn(prefab);
    }

    public static void DespawnAll()
    {
        foreach (var item in dicGU)
        {
            item.Value.DespawnAll();
        }
    }
}
public class Pool
{
    private GameUnit prefab;
    private Transform parent;
    Queue<GameUnit> queueWeapon = new Queue<GameUnit>();
    List<GameUnit> listWeapon = new List<GameUnit>();
    public void Preload(GameUnit prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
        for (int i = 0; i < queueWeapon.Count; i++)
        {
            Despawn(GameObject.Instantiate(prefab, parent));
        }

    }
    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit;
        if (queueWeapon.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = queueWeapon.Dequeue();
        }
        unit.TF.SetPositionAndRotation(pos, rot);
        listWeapon.Add(unit);
        unit.gameObject.SetActive(true);
        return unit;
    }
    public void Despawn(GameUnit prefab)
    {
        if (prefab != null && prefab.gameObject.activeSelf)
        {
            prefab.gameObject.SetActive(false);
            listWeapon.Remove(prefab);
            queueWeapon.Enqueue(prefab);
        }
    }

    public void DespawnAll()
    {
        while (listWeapon.Count > 0)
        {
            Despawn(listWeapon[0]);
        }
    }
}
