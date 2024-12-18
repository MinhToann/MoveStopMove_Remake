using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BotManager : Singleton<BotManager>
{
    string[] botName = new string[] { "Smith", "Johnson", "Williams", "Edward", "Eddy", "Teddy", "God", "David",
    "Miller", "King", "Arthur", "Doremon", "Nobita", "Conan", "Xeko", "Goku", "Cadick", "Biden", "Donald", "Mickey",
    "Luffy", "Zoro", "Chaien", "Dekhi", "Adududu", "Justatee", "Zenky", "Orochi", "Hunter", "Blast", "Natsu", "Jihuang",
    "Xiaohu", "Jeyung", "Joe", "Zoe", "Jin", "Vayne", "Kaido", "Rengar", "Ezreal", "Sol", "Bass", "Satan", "Santa", "Devv"};
    [field: SerializeField] public List<Bot> listBot { get; private set; } = new List<Bot>();
    private Bot currentBot;
    [SerializeField] private Bot bot;
    private int totalBot = 49;
    [SerializeField] private int numberSpawn;
    private int count = 10;
    [SerializeField] ItemSO itemSO;
    public int GetTotalBot => totalBot;
    private int botLevel;
    private void Start()
    {
        
    }

    public void OnInit()
    {
        numberSpawn = 0;
        botLevel = Random.Range(LevelManager.Ins.currentPlayer.PlayerLevel, LevelManager.Ins.currentPlayer.PlayerLevel + 3);
        SpawnBot();
    }
    public string GetRandomBotName()
    {
        return botName[Random.Range(0, botName.Length)];
    }    
    public void SpawnBot()
    {
        for(int i = 0; i < count; i++)
        {
            currentBot = Instantiate(bot, transform);
            currentBot.name = GetRandomBotName();
            currentBot.SetLevel(botLevel);
            if (LevelManager.Ins.currentPlayer != null)
            {
                while(Vector3.Distance(currentBot.TF.position, LevelManager.Ins.currentPlayer.TF.position) <= LevelManager.Ins.currentPlayer.currentModel.maxRadius * 2)
                {
                    
                    Vector3 randomPos = LevelManager.Ins.GetLevel().GetRandomPositionOnMap();
                    randomPos = LevelManager.Ins.GetLevel().GetRandomPositionOnMap();
                    currentBot.TF.position = randomPos;
                    currentBot.TF.rotation = LevelManager.Ins.GetLevel().startPos.rotation;
                    currentBot.OnInit();
                    currentBot.GetModel.DeactiveLine();
                    
                }
                //do
                //{

                //}
                //while (Vector3.Distance(currentBot.TF.position, LevelManager.Ins.currentPlayer.TF.position) < LevelManager.Ins.currentPlayer.currentModel.maxRadius * 2);                
                
            }
            listBot.Add(currentBot);
            LevelManager.Ins.AddCharacter(currentBot);
            numberSpawn = count;

        }
        
    }    
    public void Spawn()
    {
        if (LevelManager.Ins.GetGameState() == GameState.Gameplay)
        {
            currentBot = Instantiate(bot, transform);
            currentBot.name = GetRandomBotName();
            botLevel = Random.Range(LevelManager.Ins.currentPlayer.PlayerLevel, LevelManager.Ins.currentPlayer.PlayerLevel + 3);
            currentBot.SetLevel(botLevel);
            currentBot.TF.position = LevelManager.Ins.GetLevel().GetRandomPositionOnMap();
            currentBot.TF.rotation = LevelManager.Ins.GetLevel().startPos.rotation;
            currentBot.OnInit();
            currentBot.GetModel.DeactiveLine();
            listBot.Add(currentBot);
            LevelManager.Ins.AddCharacter(currentBot);
            numberSpawn += 1;
        }       
    }    
    public void OnBotDeath(Bot bot)
    {
        if (numberSpawn < totalBot)
        {
            Invoke(nameof(Spawn), 2);
        }
        Destroy(bot.gameObject);
        listBot.Remove(bot);        
    }
    public void ClearListBot()
    {
        for(int i = 0; i < listBot.Count; i++)
        {
            Destroy(listBot[i].gameObject);
        }    
        listBot.Clear();
    }    
}
