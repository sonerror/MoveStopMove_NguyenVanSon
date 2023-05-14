using System.Collections.Generic;
using UIExample;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    public List<Character> characterList;
    public Player player;

    public int alive;
    public int alive_Max;
    public int currentLevel;
    private int numberRealBotSpawn;
    private int numberCharacter = 50;

    [SerializeField] private Map[] mapFrefab;
    [SerializeField] private List<Map> maps;

    public Transform tfMaps;
    private void Awake()
    {
       
        if (PlayerPrefs.HasKey(Pref.KEY_LEVEL))
        {
            currentLevel = PlayerPrefs.GetInt(Pref.KEY_LEVEL);
        }
        else
        {
            currentLevel = 1;
        }
        for (int i = 0; i < mapFrefab.Length; i++)
        {
            Map map = SimplePool.Spawn<Map>(mapFrefab[i]);
            map.gameObject.transform.SetParent(tfMaps);
            map.gameObject.SetActive(false);
            maps.Add(map);
        }
        maps[currentLevel - 1].gameObject.SetActive(true);

        alive_Max = PlayerPrefs.GetInt(Pref.KEY_ALIVEMAX, numberCharacter);
        alive = alive_Max; 
    }
    private void Start()
    {
        numberRealBotSpawn = BotManager.instance.realBot;
    }
    public void ResetGame()
    {
        player.OnRevive();
        player._listTarget.Clear();
        for (int i = 0; i < BotManager.instance.bots.Count; i++)
        {
            BotManager.instance.bots[i].OnDespawn();
            SimplePool.Despawn(BotManager.instance.bots[i]);
            BotManager.instance.bots[i]._isCanMove = false;
            BotNamePool.instance.ClearNameBot();
            BotManager.instance.bots[i]._listTarget.Clear();
        }
        BotManager.instance.bots.Clear();
    }
    public void LoseGame()
    {
        characterList.Clear();
        ResetGame();
        alive = alive_Max;
        PlayerPrefs.SetInt(Pref.KEY_ALIVEMAX, alive_Max);
        BotManager.instance.SpawnNumberBotReal(BotManager.instance.realBot);
        PlayerPrefs.Save();
        GameManager.Ins.UIMainMenu();
    }

    public void NextLevelGame()
    {
        characterList.Clear();
        ResetGame();
        alive_Max += 10;
        PlayerPrefs.SetInt(Pref.KEY_ALIVEMAX, alive_Max);
        alive = alive_Max;
        NextMap();
        UIManager.Ins.OpenUI<UIWin>().CloseDirectly();
        numberRealBotSpawn += 1;
        BotManager.instance.SpawnBot(alive, numberRealBotSpawn);
        GameManager.Ins.UIMainMenu();
        UIManager.Ins.OpenUI<MainMenu>();
        PlayerPrefs.Save();
    }

    public void NextMap()
    {
        SimplePool.Despawn(maps[currentLevel - 1]);
        currentLevel++;
        if (currentLevel > maps.Count)
        {
            maps[maps.Count - 1].gameObject.SetActive(true);
        }
        else
        {
            maps[currentLevel - 1].gameObject.SetActive(true);
        }
         PlayerPrefs.GetInt(Pref.KEY_LEVEL,currentLevel);
        PlayerPrefs.SetInt(Pref.KEY_ALIVEMAX, currentLevel);
        PlayerPrefs.Save();
    }
    public void RemoveTarget(Character character)
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i]._listTarget.Contains(character))
            {
                characterList[i]._listTarget.Remove(character);
            }
        }
    }
}
