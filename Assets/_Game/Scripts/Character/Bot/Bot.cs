using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Bot : Character
{
    [field: SerializeField] public GameObject imgTarget { get; private set; }
    float time = 0;
    float timer;
    [SerializeField] NavMeshAgent agent;
    private Vector3 destination;
    private IState<Bot> currentState;
    public bool isDestination => Vector3.Distance(TF.position, destination + (TF.position.y - destination.y) * Vector3.up) < 0.1f;
    
    public override void OnInit()
    {
        ChangeCharacterModel((PrefabType)Random.Range((int)itemSO.listModel[0].prefabType, (int)itemSO.listModel[itemSO.listModel.Count - 1].prefabType));
        ChangeHat((PrefabType)Random.Range(0, itemSO.listHatItem.Count));        
        ChangePant((ColorType)Random.Range(1, itemSO.listMaterials.Count));
        ChangeWeapon((PrefabType)Random.Range((int)itemSO.listWeaponItem[0].prefabType, (int)itemSO.listWeaponItem[itemSO.listWeaponItem.Count - 1].prefabType));
        ChangeState(Constant.IDLE_STATE);
        base.OnInit();
        SetCharacter(this);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();       
        BotManager.Ins.OnBotDeath(this);
    }
    
    public override void ChangeWeapon(PrefabType prefabType)
    {
        base.ChangeWeapon(prefabType);
    }
    public override void ChangeHat(PrefabType prefabType)
    {
        base.ChangeHat(prefabType);
    }
    public override void ChangeCharacterModel(PrefabType prefabType)
    {
        base.ChangeCharacterModel(prefabType);
    }
    public override void ChangePant(ColorType colorType)
    {
        base.ChangePant(colorType);
    }
    public override void Update()
    {
        base.Update();
        if(LevelManager.Ins.GetGameState() == GameState.Gameplay)
        {
            if(!isDeath)
            {
                if (currentState != null)
                {
                    currentState.OnExecute(this);
                }
            }
            else
            {
                ChangeState(Constant.DEAD_STATE);
            }
        }    
    }
    public override void OnDeath()
    {
        base.OnDeath();
        StopSetDestination();
        DeactiveImage();
        SetTarget(null);
        cooldownThrow = 10f;        
    }
    public override void Attack()
    {
        base.Attack();
        Invoke(nameof(ChangeIdleState), 0.55f);
    }
    public void ChangeIdleState()
    {
        ChangeState(Constant.IDLE_STATE);
    }

    public void ChangeState(IState<Bot> state)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }    
        currentState = state;
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }    
    }    
    public void OnIdleEnter()
    {
        isMoving = false;
        ChangeAnim(Constant.ANIM_IDLE);
        StopSetDestination();
        timer = Random.Range(4, 6);
    }    
    public void OnIdleExecute()
    {
        time += Time.deltaTime;
        
        if (time > timer)
        {
            ChangeState(Constant.PATROL_STATE);
        }    
        else if(time >= 2 && time <= timer)
        {
            if (GetTarget != null)
            {
                if (cooldownThrow <= 0)
                {
                    StopSetDestination();
                    ChangeState(Constant.ATTACK_STATE);
                }
            }
        }
    }    
    public void OnPatrolEnter()
    {
        time = 0;
        destination = LevelManager.Ins.GetLevel().GetRandomPositionOnMap();
        SetDestination(destination);
        timer = Random.Range(6, 10);
    }    
    public void OnPatrolExecute()
    {
        time += Time.deltaTime;
        if(isDestination)
        {            
            if (GetTarget != null)
            {
                //if (CheckIsTargetInRange())
                //{
                    if (cooldownThrow <= 0)
                    {
                        StopSetDestination();
                        ChangeState(Constant.ATTACK_STATE);
                    }
                //}
            }
            else
            {
                StopSetDestination();
                ChangeState(Constant.IDLE_STATE);
            }
            
        }        
        else
        {
            if(time > timer)
            {
                ChangeState(Constant.IDLE_STATE);
            }
        }
    }    
      
    public void OnAttackEnter()
    {   
        Attack();
    }   
    public void OnAttackExecute()
    {

    }
    public void OnDeadEnter()
    {
        OnDeath();
        
    }
    public void OnDeadExecute()
    {

    }
    public void SetDestination(Vector3 destination)
    {
        agent.isStopped = false;
        isMoving = true;
        ChangeAnim(Constant.ANIM_RUN);
        this.destination = destination;
        agent.SetDestination(destination);        
    }
    public void StopSetDestination()
    {
        agent.isStopped = true;
        isMoving = false;
    }
    IEnumerator WaitTime(Vector3 destination)
    {
        ChangeAnim(Constant.ANIM_IDLE);
        yield return new WaitForSeconds(0.2f);
        SetDestination(destination);
    }
    private void WaitTimeToChangeDirection(Vector3 destination)
    {
        StartCoroutine(WaitTime(destination));
    }
    public void ActiveImage()
    {
        imgTarget.SetActive(true);
    }
    public void DeactiveImage()
    {
        imgTarget.SetActive(false);
    }
}
