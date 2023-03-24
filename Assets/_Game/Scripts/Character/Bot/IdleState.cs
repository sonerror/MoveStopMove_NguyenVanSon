using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class IdleState : IState<Bot>
{
    float timer;
    float time = 0f;
    float _timeAttack = 1f;
    public void OnEnter(Bot bot)
    {
        bot.ChangAnim(Constant.EANIM_IDLE);
    }

    public void OnExecute(Bot bot)
    {
        timer = Random.Range(2f, 6f);
        if (time > timer && bot._listTarget.Count <= 0)
        {
            bot.ChangeState(new PatrolState());
            time = 0f;
        }
        else if (bot._listTarget.Count > 0 && time > _timeAttack)
        {
            bot.ChangeState(new AttackState());
            time = 0f;
        }
        time += Time.deltaTime;
        if (bot._isDead)
        {
            bot.ChangeState(new DeadState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
