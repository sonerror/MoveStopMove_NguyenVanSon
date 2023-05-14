using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BotManager : MonoBehaviour
{
    public static BotManager instance;

    public Player player;
    public Bot botPrefab;
    public List<Bot> bots = new List<Bot>();

    public int totalBot;
    public int realBot;

    public float minDistance;
    private float radius = 100f;
    [SerializeField] private Transform _tfBots;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        totalBot = LevelManager.Ins.alive_Max;
        LevelManager.Ins.characterList.Add(player);
        player.OnInit();
        for (int i = 0; i < totalBot; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefab, Vector3.zero, Quaternion.identity);
            bot.transform.SetParent(_tfBots);
            bot.gameObject.SetActive(false);
            bots.Add(bot);
        }
        SpawnNumberBotReal(realBot);
    }
    public void SpawnBot(int quantityEnemy, int realbotSpawn)
    {
        for (int i = 0; i < quantityEnemy; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefab, Vector3.zero, Quaternion.identity);
            bot.gameObject.SetActive(false);
            bots.Add(bot);

        }
        SpawnNumberBotReal(realbotSpawn);
    }
    public void SpawnNumberBotReal(int realBotSpawn)
    {
        for (int i = 0; i < realBotSpawn; i++)
        {
            SpawnBot();
        }
    }
    public Bot GetBotFormPool()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            if (!bots[i].gameObject.activeInHierarchy)
            {
                return bots[i];
            }
        }
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, Vector3.zero, Quaternion.identity);
        bot.gameObject.SetActive(false);
        bots.Add(bot);
        return bot;
    }
    public void SpawnBot()
    {
        Bot bot = GetBotFormPool();
        bot.OnInit();
        bot.ChangeSize(1);
        bot._isCanMove = false;
        if (CheckRamdomPosition(bot))
        {
            bot.gameObject.SetActive(true);
            LevelManager.Ins.characterList.Add(bot);
        }
        SpawnNameBots();
    }
    public void SpawnNameBots()
    {
        Bot bot = GetBotFormPool();
        GameObject pooledBotName = BotNamePool.instance.GetObject();
        pooledBotName.GetComponent<CanvasNameBotsUI>().SetTargetTransform(bot.transform);
        bot.botName = pooledBotName;
    }
    public void SpawnBot(bool isCanMove)
    { 
        Bot bot = GetBotFormPool();
        bot.OnInit();
        bot._isCanMove = isCanMove;

        float randSize = Random.Range(Character.MAX_SIZE, Character.MIN_SIZE);
        bot.attRange *= randSize;
        bot.ChangeSize(randSize);
        if (CheckRamdomPosition(bot))
        {
            bot.gameObject.SetActive(true);
            LevelManager.Ins.characterList.Add(bot);
        }
        
    }
    public void DespawnNameBot(Bot bot)
    {
        BotNamePool.instance.ReturnToPool(bot.botName);
    }
    public IEnumerator CoroutineSpawnBot()
    {
        yield return new WaitForSeconds(2f);
        SpawnBot(true); 
        SpawnNameBots();

    }
    public bool CheckRamdomPosition(Character character)
    {
        int currentLevel = LevelManager.Ins.currentLevel - 1;
        bool validPosition = false;
        while (!validPosition)
        {
            character.transform.position = RandomNavSphere(character.transform.position, radius, -1);
            validPosition = true;
            foreach (Character otherCharacter in LevelManager.Ins.characterList)
            {
                if (Vector3.Distance(character.transform.position, otherCharacter.transform.position) < minDistance)
                {
                    validPosition = false;
                    break;
                }
            }
        }
        return validPosition;
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}


