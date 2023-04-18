using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackState : IState<Bot>
{
    private float timer;
    private float time;
    public void OnEnter(Bot bot)
    {
        bot.OnAttack();
        timer = 0.0f;
        time = 0.35f;
    }
    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            int randomValue = Random.Range(0, 3);
            if (randomValue == 1)
            {
                bot.ChangeState(new IdleState());
            }
            else
            {
                bot.ChangeState(new PatrolState());
            }
        }
        if (bot._isDead)
        {
            bot.ChangeState(new DeadState());
        }
    }
    public void OnExit(Bot bot)
    {

    }
}