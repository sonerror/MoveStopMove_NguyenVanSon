using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class DeadState : IState<Bot>
{
    float timer;
    float time;
    public void OnEnter(Bot bot)
    {
        time = 0.1f;
        timer = 3.1f;
        bot.OnDead();
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if (time > timer)
        {

            bot._isDead = false;

            SimplePool.Despawn(bot);
            LevelManager.instance.alive--;
            if (LevelManager.instance.alive > BotManager.instance.spawnNumberBot)
            {
                BotManager.instance.StartCoroutine(BotManager.instance.CoroutineSpawnBot());
            }
            BotManager.instance.DespawnNameBot(bot);
            BotManager.instance.bots.Remove(bot);
            time = 0;
        }
    }
    public void OnExit(Bot bot)
    {

    }

}