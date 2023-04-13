using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PatrolState : IState<Bot>
{
    float timer;
    float time;
    public void OnEnter(Bot bot)
    {
        time = 0f;
        timer = 1.1f;
    }

    public void OnExecute(Bot bot)
    {
        if (bot._isDead)
        {
            bot.ChangeState(new DeadState());
        }
        else
        {
            bot.Moving();
            time += Time.deltaTime;
            if (bot._listTarget.Count > 0 && time > timer)
            {
                bot.OnMoveStop();
                bot.ChangeState(new AttackState());
                time = 0f;
            }
        }
    }
    public void OnExit(Bot bot)
    {

    }
}