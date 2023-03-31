using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class DeadState : IState<Bot>
{
    float timer = 0f;
    float time;
    public void OnEnter(Bot bot)
    {
        timer = 2f;
        bot.OnDead();
    }

    public void OnExecute(Bot bot)
    {
        if (time > timer)
        {

            bot._isDead = false;

            SimplePool.Despawn(bot);
            LevelManager.instance.alive--;
            if (LevelManager.instance.alive > BotManager.instance.spawnNumberBot)
            {
                BotManager.instance.StartCoroutine(BotManager.instance.CoroutineSpawnBot());
            }

            BotManager.instance.bots.Remove(bot);
            time = 0;
        }
        time += Time.deltaTime;
    }
    public void OnExit(Bot bot)
    {

    }

}