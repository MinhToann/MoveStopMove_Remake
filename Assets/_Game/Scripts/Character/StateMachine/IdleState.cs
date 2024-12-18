using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.OnIdleEnter();
    }
    public void OnExecute(Bot bot)
    {
        bot.OnIdleExecute();
    }
    public void OnExit(Bot bot)
    {

    }
}
