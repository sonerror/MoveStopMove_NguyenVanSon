using System.Collections;
using System.Collections.Generic;
using UIExample;
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
        bot.isCanMove = false;
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if (time > timer)
        {
            bot.isDead = true;
            bot.ChangeState(new IdleState());
            SimplePool.Despawn(bot);
            LevelManager.Ins.alive--;
            if (LevelManager.Ins.alive > BotManager.instance.realBot)
            {
                BotManager.instance.StartCoroutine(BotManager.instance.CoroutineSpawnBot());
            }
            if (LevelManager.Ins.alive == 1 && LevelManager.Ins.player.isDead != true)
            {
                UIManager.Ins.OpenUI<UIWin>();
                UIManager.Ins.OpenUI<UIGamePlay>().CloseDirectly();
            }
            BotManager.instance.bots.Remove(bot);
            LevelManager.Ins.characterList.Remove(bot);
            BotManager.instance.DespawnNameBot(bot);
            time = 0;
        }
    }
    public void OnExit(Bot bot)
    {

    }

}