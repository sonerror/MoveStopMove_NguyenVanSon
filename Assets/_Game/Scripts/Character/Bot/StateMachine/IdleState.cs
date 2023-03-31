using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class IdleState : IState<Bot>
{
    float timer;
    float time = 0f;
    float durationTimeAttack = 1.1f;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim(Constant.ANIM_IDLE);
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        timer = Random.Range(2f, 5f);
        if (time > timer && bot._listTarget.Count <= 0)
        {
            bot.ChangeState(new PatrolState());
            time = 0f;
        }
        else if (bot._listTarget.Count > 0 && time > durationTimeAttack)
        {
            bot.ChangeState(new AttackState());
            time = 0f;
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