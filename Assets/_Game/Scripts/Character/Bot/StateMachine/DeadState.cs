using System.Collections;
using System.Collections.Generic;
using UIExample;
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
        bot._isCanMove = false;
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if (time >= timer)
        {
            bot._isDead = true;
            bot.ChangeState(new IdleState());
            SimplePool.Despawn(bot);
            LevelManager.instance.alive--;
            if (LevelManager.instance.alive > BotManager.instance.spawnNumberBot)
            {
                BotManager.instance.StartCoroutine(BotManager.instance.CoroutineSpawnBot());
            }
            if (LevelManager.instance.alive == 1)
            {
                UIManager.Ins.OpenUI<Win>();
                UIManager.Ins.OpenUI<GamePlay>().CloseDirectly();
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