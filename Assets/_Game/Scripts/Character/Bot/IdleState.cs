using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float _randomTime;
    float _timer;
    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
        _timer = 0;
        _randomTime = Random.Range(2f, 4f);
    }
    public void OnExecute(Bot bot)
    {
        _timer += Time.deltaTime;
        if(_timer > _randomTime)
        {
            bot.ChangeState(new PatrolState());
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
