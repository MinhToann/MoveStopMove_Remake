using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class PlayerData : Singleton<PlayerData>
{
    [field: SerializeField] public Data data = new Data();
    const string path = "PlayerData.abc";
    private void Awake()
    {       
        data = LoadData();
    }
    public void SaveData()
    {
        SaveGame.Save(path, data);
    }
    public Data LoadData()
    {
        return SaveGame.Load(path, new Data());
    }
    public void SetCoin(int coin)
    {
        data.SetCoin(coin);
        SaveData();
    }    
    public void IncreaseCoin(int coin)
    {
        data.SetCoin(data.GetCoin() + coin);
        SaveData();
    }    
    public void DecreaseCoin(int coin)
    {
        data.SetCoin(data.GetCoin() - coin);
        SaveData();
    }
    public void SetHatType(PrefabType prefabType)
    {
        data.SetHatType(prefabType);
        SaveData();
    }
    public void SetPantType(PrefabType prefabType)
    {
        data.SetPantType(prefabType);
        SaveData();
    }
    public void SetWeaponType(PrefabType prefabType)
    {
        data.SetWeaponType(prefabType);
        SaveData();
    }
    public void SetSkinType(PrefabType prefabType)
    {
        data.SetSkinType(prefabType);
        SaveData();
    }
    public void SetModelType(PrefabType prefabType)
    {
        data.SetModelType(prefabType);
        SaveData();
    }    
    public void SetColorType(ColorType colorType)
    {
        data.SetColorType(colorType);
        SaveData();
    }
    public void SetColorPantType(ColorType colorType)
    {
        data.SetColorPantType(colorType);
        SaveData();
    }
    public void SetTemporaryHatType(PrefabType prefabType)
    {
        data.SetHatType(prefabType);
    }
    public void SetTemporaryPantType(PrefabType prefabType)
    {
        data.SetPantType(prefabType);
    }
    public void SetTemporaryWeaponType(PrefabType prefabType)
    {
        data.SetWeaponTryType(prefabType);
    }
    public void SetTemporarySkinType(PrefabType prefabType)
    {
        data.SetSkinType(prefabType);
    }
    public void SetTemporaryModelType(PrefabType prefabType)
    {
        data.SetModelType(prefabType);
    }
    public void SetTemporaryColorType(ColorType colorType)
    {
        data.SetColorType(colorType);
    }
    public void SetTemporaryColorpantType(ColorType colorType)
    {
        data.SetColorPantType(colorType);
    }
}
[System.Serializable]
public class Data
{
    [field: SerializeField] public int coin { get; private set; }
    [field: SerializeField] public PrefabType weaponType { get; private set; }
    [field: SerializeField] public PrefabType weaponTryType { get; private set; }
    [field: SerializeField] public PrefabType hatType { get; private set; }
    [field: SerializeField] public PrefabType pantType { get; private set; }
    [field: SerializeField] public PrefabType skinType { get; private set; }
    [field: SerializeField] public PrefabType modelType { get; private set; }
    [field: SerializeField] public ColorType colorType { get; private set; }
    [field: SerializeField] public ColorType colorPantType { get; private set; }
    public Data()
    {
        coin = 0;
        weaponType = PrefabType.Hammer;
        hatType = PrefabType.Arrow;
        pantType = PrefabType.Normal;
        modelType = PrefabType.NormalSkin;
        colorType = ColorType.None;
        colorPantType = ColorType.None;
    }
    public void SetCoin(int coin)
    {
        this.coin = coin;
    }
    public int GetCoin()
    {
        return coin;
    }
    public void SetHatType(PrefabType prefabType)
    {
        hatType = prefabType;
    }
    public void SetPantType(PrefabType prefabType)
    {
        pantType = prefabType;
    }
    public void SetWeaponType(PrefabType prefabType)
    {
        weaponType = prefabType;
    }
    public void SetSkinType(PrefabType prefabType)
    {
        skinType = prefabType;
    }
    public void SetModelType(PrefabType prefabType)
    {
        modelType = prefabType;
    }
    public void SetColorType(ColorType colorType)
    {
        this.colorType = colorType;
    }
    public void SetWeaponTryType(PrefabType prefabType)
    {
        weaponTryType = prefabType;
    }
    public void SetColorPantType(ColorType colorType)
    {
        colorPantType = colorType;
    }
}

