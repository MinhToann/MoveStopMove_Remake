using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Player playerPrefab;
    [field: SerializeField] public Player currentPlayer { get; private set; }
    private int levelNumber = 0;
    public int GetLevelNumber => levelNumber;
    private Level currentLevel;
    [SerializeField] private List<Level> listLevel = new List<Level>();
    [SerializeField] private List<CinemachineVirtualCamera> listCinemacineCamera = new List<CinemachineVirtualCamera>();
    private GameState gameState;
    [SerializeField] ItemSO itemSO;
    private int totalCharacter;
    public int GetTotalCharacter => totalCharacter;
    private float initReviveNumber;
    public float GetReviveNumber => initReviveNumber;
    public string GetKillerName => currentPlayer.KillerName;
    private int totalBox;
    private int countBox;
    private int randomNumber;
    float cooldownSpawnBox;
    public bool isChangeGameState = false;
    public float CoolDownSpawnBox => cooldownSpawnBox;
    [field: SerializeField] public List<SpecialBox> listGiftBox { get; private set; } = new List<SpecialBox>();
    [field: SerializeField] public List<SpecialBox> listSpecialBox { get; private set; } = new List<SpecialBox>();
    [field: SerializeField] public List<Character> listCharacterInGame { get; private set; } = new List<Character>();
    private void Start()
    {
        
        OnInit();
        SetInitShop();   
    }

    public void OnInit()
    {
        OnDespawnAll();
        OnloadLevel(levelNumber);
        itemSO.SetValueItem();
        cooldownSpawnBox = 25f;       
        SpawnPlayer();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
        SpawnBot();
        UIManager.Ins.SetCoin(PlayerData.Ins.LoadData().coin);
        countBox = 0;
        totalBox = 4;
        totalCharacter = BotManager.Ins.GetTotalBot + 1;
        
    }    
    public void SpawnPlayer()
    {
        if(currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
        }    
        currentPlayer = Instantiate(playerPrefab);
        currentPlayer.TF.position = currentLevel.startPos.position;
        currentPlayer.TF.rotation = currentLevel.startPos.rotation;
        currentPlayer.OnInit();
        AddCharacter(currentPlayer);
    }    
    public void SpawnBot()
    {
        BotManager.Ins.OnInit();
    }    
    public void OnDespawnAll()
    {
        if(currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
        }    
        if(BotManager.Ins.listBot.Count > 0)
        {
            BotManager.Ins.ClearListBot();
        }
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        if(listGiftBox.Count > 0)
        {
            ClearListBox();
        }
        ClearListCharacter();
    }
    public void SpawnGiftBox()
    {
        if(countBox <= totalBox)
        {
            randomNumber = Random.Range(0, listSpecialBox.Count);
            SpecialBox giftBox = Instantiate(listSpecialBox[randomNumber], currentLevel.GetRandomPositionSpawnGiftOnMap(), Quaternion.identity);
            listGiftBox.Add(giftBox);
            countBox += 1;
        }
                
    }
    public void OnDespawnBox(SpecialBox box)
    {
        listGiftBox.Remove(box);
        Destroy(box.gameObject);
        countBox -= 1;
    }
    public void ClearListBox()
    {
        if (listGiftBox.Count > 0)
        {
            for (int i = 0; i < listGiftBox.Count; i++)
            {
                Destroy(listGiftBox[i].gameObject);
            }
            listGiftBox.Clear();
            countBox = 0;
            totalBox = 0;
        }
    }
    public void AddCharacter(Character character)
    {
        listCharacterInGame.Add(character);
    }
    public void RemoveCharacter(Character character)
    {
        listCharacterInGame.Remove(character);       
    }
    public void ClearListCharacter()
    {
        for(int i = 0; i < listCharacterInGame.Count; i++)
        {
            Destroy(listCharacterInGame[i].gameObject);
        }
        listCharacterInGame.Clear();
    }
    public void DecreaseCoolDownSpawnBoxTime()
    {
        cooldownSpawnBox -= Time.deltaTime;
    }
    public void SetCoolDownSpawnBoxTime(float time)
    {
        cooldownSpawnBox = time;
    }
    public void ChangeViewCinemachine()
    {
        switch(gameState)
        {
            case GameState.MainMenu:
                SetPriorityCamera(0);
                break;
            case GameState.Gameplay:
                SetPriorityCamera(1);
                break;
            case GameState.ShoppingView:
                SetPriorityCamera(2);
                break;
            case GameState.Win:
                SetPriorityCamera(3);
                break;
            case GameState.Lose:
                SetPriorityCamera(4);
                break;
        } 
    }   
    public void SetViewGameplayCamera()
    {
        listCinemacineCamera[1].transform.position += new Vector3(0, currentPlayer.valueUp * 2.5f, -currentPlayer.valueUp * 4);
    }
    public void SetInitShop()
    {
        for (int j = ShopData.Ins.LoadData().listEquippedItem.Count - 1; j >= 0; j--)
        {
            ShopData.Ins.RemoveItemFromListEquipped(ShopData.Ins.LoadData().listEquippedItem[j]);
        }
        ShopData.Ins.LoadData().listEquippedItem.Clear();
        ShopData.Ins.AddItemToListEquipped(currentPlayer.currentWeapon.prefabType);
        ShopData.Ins.AddItemToListEquipped(currentPlayer.currentHat.prefabType);
        ShopData.Ins.AddItemToListEquipped(currentPlayer.currentModel.currentPant.prefabType);
        ShopData.Ins.AddItemToListEquipped(currentPlayer.currentModel.prefabType);
        ShopData.Ins.AddTypeItemToList(ItemType.Weapon);
        ShopData.Ins.AddTypeItemToList(ItemType.Hat);
        ShopData.Ins.AddTypeItemToList(ItemType.Pant);
        ShopData.Ins.AddTypeItemToList(ItemType.Skin);
    }    
    public void SetPriorityCamera(int index)
    {
        if(listCinemacineCamera.Count > 0)
        {
            for (int i = 0; i < listCinemacineCamera.Count; i++)
            {
                listCinemacineCamera[i].Priority = 1;
            }
            listCinemacineCamera[index].Priority = 10;
        }            
    }   
    public Level GetLevel()
    {
        return currentLevel;
    }    
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }   
    public GameState GetGameState()
    {
        return gameState;
    }   
    public void ReloadLevel()
    {
        OnloadLevel(levelNumber);
    }    
    public void NextLevel()
    {
        OnloadLevel(++levelNumber);
    }
    public void OnloadLevel(int level)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);   
        }    
        if(level < listLevel.Count)
        {
            currentLevel = Instantiate(listLevel[level]);
        }    
    }    
    public void SetInitNumberCharacterAlive()
    {
        UIManager.Ins.GetUI<CanvasGameplay>().SetNumberCharacterAlive(totalCharacter);
    }
    public void DecreaseNumberAlive()
    {
        totalCharacter -= 1;
        UIManager.Ins.GetUI<CanvasGameplay>().SetNumberCharacterAlive(totalCharacter);
    }
    public void SetInitTimeRevive(float time)
    {
        initReviveNumber = time;
    }    
    public void DecreaseReviveTime(float time)
    {
        if (gameState == GameState.Revive)
            time -= 1;
    }    
}
