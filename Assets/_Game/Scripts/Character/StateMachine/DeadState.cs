using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.OnDeadEnter();
    }
    public void OnExecute(Bot bot)
    {

    }
    public void OnExit(Bot bot)
    {

    }
}
