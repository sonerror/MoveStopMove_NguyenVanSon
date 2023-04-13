using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;

public class BotManager : GameUnit
{
    public static BotManager instance;
    public Player player;
    public Bot _botPrefab;
    public List<Bot> bots = new List<Bot>();

    public int totalBot;
    public int spawnNumberBot;
    private int _radius = 100;
    public float minDistance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LevelManager.instance.characterList.Add(player);
        player.OnInit();
        Oninit();
    }
    private void Oninit()
    {
        LevelManager.instance.characterList.Add(player);
        player.OnInit();
        for (int i = 0; i < totalBot; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(_botPrefab, Vector3.zero, Quaternion.identity);
            bot.gameObject.SetActive(false);
            bots.Add(bot);
        }

        SpawnRealBot(spawnNumberBot);
    }
    public void SpawnRealBot(int realBotSpawn)
    {
        for (int i = 0; i < realBotSpawn; i++)
        {
            SpawnBot();
        }
    }
    public Bot GetBotFormPool()
    {
        Vector3 newPos = RandomNavSphere(transform.position, _radius, -1);
        for (int i = 0; i < bots.Count; i++)
        {
            if (!bots[i].gameObject.activeInHierarchy)
            {
                return bots[i];
            }
        }
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, newPos, Quaternion.identity);
        bot.gameObject.SetActive(false);
        bots.Add(bot);
        return bot;
    }
    public void SpawnBot()
    {
        Bot bot = GetBotFormPool();
        bot.OnInit();
        if (CheckRamdomPosition(bot))
        {
            bot.gameObject.SetActive(true);
            LevelManager.instance.characterList.Add(bot);
        }
        GameObject pooledBotName = BotNamePool.instance.GetObject();
        pooledBotName.GetComponent<CanvasNameOnUI>().SetTargetTransform(bot.transform);
        bot.botName = pooledBotName;
    }
    public void DespawnNameBot(Bot bot)
    { 
        BotNamePool.instance.ReturnToPool(bot.botName);
    }
    public IEnumerator CoroutineSpawnBot()
    {
        yield return new WaitForSeconds(2f);
        SpawnBot();

    }
    public bool CheckRamdomPosition(Character character)
    {
        bool validPosition = false;
        while (!validPosition)
        {
            character.transform.position = RandomNavSphere(character.transform.position, _radius, -1);
            //character.transform.position = new Vector3(Random.Range(-pointX, pointX), 0, Random.Range(-pointZ, pointZ));
            validPosition = true;
            foreach (Character otherCharacter in LevelManager.instance.characterList)
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
