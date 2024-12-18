using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [field: SerializeField] public int playerCoin { get; private set; }
    [field: SerializeField] public int coin { get; private set; }

    [SerializeField] Rigidbody rb;
    private string killerName;
    public string KillerName => killerName;
    private int gainCoin;
    public int TotalGainCoin => gainCoin;
    private int playerLevel;
    public int PlayerLevel => playerLevel;
    private void Awake()
    {
        
    }
    public override void OnInit()
    {
        
        playerCoin = PlayerData.Ins.LoadData().coin;
        gainCoin = 0;
        ChangePlayerModel();
        ChangePlayerWeapon();
        ChangePlayerHat();
        ChangePlayerPant();
        playerLevel = numberLevel.NumberLevel;
        SetLevel(playerLevel);
        base.OnInit();      
        SetCharacter(this);
    }
    public override void Update()
    {
        base.Update();
        if(LevelManager.Ins.GetGameState() == GameState.Gameplay)
        {
            if(!isDeath)
            {
                SetRightTarget();
                Movement();
                if (GetTarget != null)
                {
                    if (cooldownThrow <= 0)
                    {
                        Attack();
                    }
                }
                if(LevelManager.Ins.listCharacterInGame.Count <= 1)
                {
                    OnWin();
                }
            }
            else
            {
                OnDeath();
            }
        }     
        if(LevelManager.Ins.GetGameState() == GameState.Setting)
        {
            ChangeIdleAnim();
        }
    }
    public void OnWin()
    {
        AudioManager.Ins.PlayVictoryAudio();
        ChangeAnim(Constant.ANIM_DANCE_WIN);
        UIManager.Ins.OpenUI<CanvasWin>();        
    }

    public void Movement()
    {
        if(UIManager.Ins.IsOpened<JoystickControl>())
        {
            if (Input.GetMouseButton(0))
            {
                isMoving = true;
                CancelInvoke();
                ChangeAnim(Constant.ANIM_RUN);
                rb.velocity = JoystickControl.direct * moveSpeed + rb.velocity.y * Vector3.up;
                rb.rotation = Quaternion.LookRotation(rb.velocity);
            }
            if (Input.GetMouseButtonUp(0))
            {
                isMoving = false;
                ChangeIdleAnim();
                rb.velocity = Vector3.zero;
                if (GetTarget != null)
                {
                    Attack();
                }

            }
        }
        
    }
    public override void Throw()
    {
        base.Throw();
    }
    public override void Attack()
    {
        base.Attack();       
        Invoke(nameof(ChangeIdleAnim), 0.55f);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        UIManager.Ins.OpenUI<CanvasRevive>();
        Destroy(this.gameObject);
    }
    
    public override void OnDeath()
    {
        base.OnDeath();
        rb.velocity = Vector3.zero;
        for(int i = 0; i < BotManager.Ins.listBot.Count; i++)
        {
            BotManager.Ins.listBot[i].ChangeIdleState();
        }
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

    public void ChangeDanceAnim()
    {
        ChangeAnim(Constant.ANIM_DANCE);
    }
    public void ChangePlayerWeapon()
    {
        ChangeWeapon(PlayerData.Ins.LoadData().weaponType);
    }    
    public void ChangePlayerHat()
    {
        ChangeHat(PlayerData.Ins.LoadData().hatType);
    }   
    public void ChangePlayerModel()
    {
        ChangeCharacterModel(PlayerData.Ins.LoadData().modelType);
    }    
    public void ChangePlayerPant()
    {
        ChangePant(PlayerData.Ins.LoadData().colorPantType);

    }
    public void ChangeTemporaryPlayerHat(PrefabType prefabType)
    {
        ChangeTemporaryHat(prefabType);
    }    
    public void ChangeTemporaryPlayerModel(PrefabType prefabType)
    {
        ChangeTemporaryCharacterModel(prefabType);
        ChangePlayerWeapon();
        //ChangePlayerHat();
        //ChangePlayerPant();
        //ChangeDanceAnim();
    }    
    public void ChangeTemporaryPlayerPant(ColorType colorType)
    {
        ChangeTemporaryPant(colorType);
    }    
    public void SetCoin(int coin)
    {
        this.coin = coin;
    }
    public void SetKillerName(string name)
    {
        killerName = name;
    }    
    public void IncreaseCoin(int coin)
    {
        gainCoin += coin;
    }
    public void SetGainCoin(int coin)
    {
        gainCoin = coin;
    }
}
