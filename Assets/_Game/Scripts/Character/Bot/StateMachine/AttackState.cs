using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackState : IState<Bot>
{

    public void OnEnter(Bot bot)
    {
        bot.StartCoroutine(bot.DoAttack());
    }
    public void OnExecute(Bot bot)
    {
        if (bot._isDead)
        {
            bot.ChangeState(new DeadState());
        }

    }
    public void OnExit(Bot bot)
    {

    }
}