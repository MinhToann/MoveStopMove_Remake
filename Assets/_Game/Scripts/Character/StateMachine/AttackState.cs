using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.OnAttackEnter();
    }
    public void OnExecute(Bot bot)
    {
        bot.OnAttackExecute();
    }
    public void OnExit(Bot bot)
    {

    }
}