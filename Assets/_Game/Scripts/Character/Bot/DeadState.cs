using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState<Bot>
{
    float timer = 0f;
    float time;
    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
        timer = 2f;
        bot.ChangAnim(Constant.EANIM_DEAD);
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if (time > timer)
        {
            SimplePool.Despawn(bot);
            time = 0;
        }
    }


    public void OnExit(Bot bot)
    {

    }
}
