using UnityEngine;

public class PatrolState : IState<Bot>
{
    float timer;
    float time;
    public void OnEnter(Bot bot)
    {
        time = 0f;
        timer = 1f;
    }
    public void OnExecute(Bot bot)
    {
        bot.Moving();
        time += Time.deltaTime;
        if (bot._listTarget.Count > 0 && time > timer)
        {
            bot.StopMoving();
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