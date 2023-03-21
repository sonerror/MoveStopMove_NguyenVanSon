using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float _randomTime;
    float _timer;
    public void OnEnter(Bot bot)
    {
        _timer = 0;
        _randomTime = Random.Range(3f, 6f);
    }

    public void OnExecute(Bot bot)
    {
        _timer += Time.deltaTime;
        if (_timer < _randomTime)
        {
            bot.Moving();
        }
        else 
        {  
            bot.ChangeState(new IdleState());
        }

    }
    public void OnExit(Bot bot)
    {
       
    }
}
