using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.OnPatrolEnter();
    }
    public void OnExecute(Bot bot)
    {
        bot.OnPatrolExecute();
    }
    public void OnExit(Bot bot)
    {

    }
}
