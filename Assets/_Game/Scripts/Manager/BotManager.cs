using System.Collections;
using System.Collections.Generic;
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
    private int ra = 100;

    public float pointX;
    public float pointZ;
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
       Vector3 newPos = RandomNavSphere(transform.position, ra, -1);
        for (int i = 0; i < totalBot; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(_botPrefab,newPos , Quaternion.identity);
            bot.gameObject.SetActive(false);
            bots.Add(bot);
        }

        for (int i = 0; i < spawnNumberBot; i++)
        {
            SpawnBot();
        }
    }
    public Bot GetBotFormPool()
    {
        Vector3 newPos = RandomNavSphere(transform.position, ra, -1);
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
        //bot.transform.position = RandomPosition();
        if (CheckRamdomPosition(bot))
        {
            bot.gameObject.SetActive(true);

            LevelManager.instance.characterList.Add(bot);
        }
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
            character.transform.position = RandomNavSphere(character.transform.position, ra, -1);
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
