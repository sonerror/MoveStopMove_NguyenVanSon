public class AttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.StartCoroutine(bot.ActionAttack());
    }
    public void OnExecute(Bot bot)
    {
        if (bot._isDead)
        {
            bot.ChangeState(new DeadState());
        }
        if(bot._listTarget.Count <= 0)
        {
            bot.ChangeState(new PatrolState());

        }
    }
    public void OnExit(Bot bot)
    {
    }
}
