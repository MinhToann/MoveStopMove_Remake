using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Character : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
    private float timeSpawnBox;

    [field: SerializeField] public List<Character> listTarget { get; private set; } = new List<Character>();
    [SerializeField] CharacterModel characterModel;
    [field: SerializeField] public CharacterModel currentCharacterModel { get; private set; }
    [SerializeField] protected Animator characterAnim;
    private float attackRange;
    private float sizeUp;
    private float maxHealth;
    private float health;
    public bool isDeath => health <= 0f;
    private string currentAnim;
    [field: SerializeField] public WeaponBase currentWeapon { get; private set; }
    [field: SerializeField] public Hat currentHat { get; private set; }
    [field: SerializeField] public CharacterModel currentModel { get; private set; }
    [field: SerializeField] public Pant currentPant { get; private set; }
    [field: SerializeField] public List<SkinItem> currentSkinItemList { get; private set; } = new List<SkinItem>();
    [SerializeField] protected ItemSO itemSO;
    Transform newTF;
    protected float moveSpeed = 6f;
    Collider[] Colliders = new Collider[1];
    [SerializeField] LayerMask mask;
    [SerializeField] LineRenderer line;
    [SerializeField] Character character;
    [SerializeField] Character target;
    private Vector3 targetPos;
    [SerializeField] CircleCollider circleCollider;
    public CircleCollider rangeCollider => circleCollider;
    public bool isMoving;
    private float rotateSpeed = 20f;
    public bool isAttack;

    protected float cooldownThrow = 0f;

    private float valueUpsize = 0.05f;
    public float valueUp => valueUpsize;
    public float GetCoolDownThrow => cooldownThrow;
    [field: SerializeField] public Transform bulletPoint { get; private set; }
    public float GetAttackRange => attackRange;

    public CharacterModel GetModel => currentModel;
    public Character GetTarget => target;
    public Character GetCharacter => character;
    private float sizeEatBox = 40;
    public float SizeEatBox => sizeEatBox;
    public bool isEatBox = false;
    [SerializeField] CapsuleCollider characterHitBox;
    [SerializeField] protected CanvasLevelCharacter numberLevel;
    public virtual void OnInit()
    {
        //SpawnModel();
        ChangeIdleAnim();
        CreateCircleCollider();
        attackRange = currentModel.radius * 2;
        isMoving = false;
        health = maxHealth = 100f;
        timeSpawnBox = 0;
    }
    public virtual void OnDespawn()
    {
        LevelManager.Ins.RemoveCharacter(this);
        LevelManager.Ins.DecreaseNumberAlive();
    }
    public void SpawnModel()
    {
        if (currentCharacterModel != null)
        {
            Destroy(currentCharacterModel.gameObject);
        }
        currentCharacterModel = Instantiate(characterModel, TF);
        characterAnim = currentCharacterModel.animModel;
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
    }
    public void SetSizeAttack()
    {
        //Size after eat the gift box

        if (currentModel.radius <= currentModel.maxRadius)
        {
            attackRange += valueUpsize * sizeEatBox;
            circleCollider.transform.localScale += new Vector3(valueUpsize * sizeEatBox, 0, valueUpsize * sizeEatBox);
            currentModel.UpSizeRadius(valueUpsize * sizeEatBox);
        }

    }
    public void SetNormalSizeAttack()
    {
        attackRange -= valueUpsize * sizeEatBox;
        circleCollider.transform.localScale -= new Vector3(valueUpsize * sizeEatBox, 0, valueUpsize * sizeEatBox);
        currentModel.UpSizeRadius(-valueUpsize * sizeEatBox);
    }
    public void UpSize()
    {
        UpSizeAttack();
        UpSizeCharacter();
        LevelUp();
    }    
    public void SetLevel(int level)
    {
        numberLevel.SetTextLevel(level);
    }    
    public void LevelUp()
    {
        numberLevel.SetNumberLevel(numberLevel.NumberLevel + 1);
        numberLevel.SetTextLevel(numberLevel.NumberLevel);
    }    
    public void UpSizeAttack()
    {
        if (currentModel.radius <= currentModel.maxRadius)
        {
            attackRange += valueUpsize;
            circleCollider.transform.localScale += new Vector3(valueUpsize, 0, valueUpsize);
            currentModel.UpSizeRadius(valueUpsize);
        }
    }
    public void UpSizeCharacter()
    {
        if (currentModel.radius <= currentModel.maxRadius)
        {
            float value = valueUpsize / 4;
            Vector3 size = new Vector3(value, value, value);
            TF.localScale += size;
            Vector3 posY = new Vector3(0, value, 0);
            TF.position += posY;
        }

    }    
    public void OnHit(float damage)
    {
        if (!isDeath)
        {
            health -= damage;
        }

    }
    public virtual void OnDeath()
    {
        ChangeDeathAnim();
        characterHitBox.enabled = false;
        Invoke(nameof(OnDespawn), 1.5f);
    }
    public void RemoveDeathTarget()
    {
        if (target.isDeath)
        {
            listTarget.Remove(target);
        }
    }

    public virtual void Throw()
    {
        RotateToTarget();
        currentWeapon.SpawnBullet();
        isAttack = false;
        cooldownThrow = 1f;
    }
    public void StartThrow()
    {
        currentWeapon.SpawnBullet();
    }
    public void StartThrowBullet()
    {
        currentWeapon.SpawnBullet();
    }
    public void AddTarget(Character character)
    {
        listTarget.Add(character);
    }
    public void RemoveTarget(Character character)
    {
        listTarget.Remove(character);
    }
    public void ClearTarget(Character character)
    {
        for (int i = 0; i < listTarget.Count; i++)
        {
            Destroy(listTarget[i].gameObject);
        }
        listTarget.Clear();
    }
    public virtual void Attack()
    {
        if (!isMoving)
        {
            isAttack = true;
            ChangeAnim(Constant.ANIM_ATTACK);
            currentWeapon.SetCharacter(this);
            Invoke(nameof(Throw), 0.1f);
        }
    }
    public void SetCoolDownTime(float time)
    {
        cooldownThrow = time;
    }
    public void RotateToTarget()
    {
        if (GetTarget != null)
        {
            Vector3 direction = GetTarget.TF.position - TF.position;
            TF.rotation = Quaternion.LookRotation(direction);
        }

    }


    public virtual void Update()
    {
        if (LevelManager.Ins.GetGameState() == GameState.Gameplay)
        {
            currentModel.DrawCircleLine();
            //SetRightTarget();
            LevelManager.Ins.DecreaseCoolDownSpawnBoxTime();
            if (LevelManager.Ins.CoolDownSpawnBox <= 0)
            {
                LevelManager.Ins.SpawnGiftBox();
                LevelManager.Ins.SetCoolDownSpawnBoxTime(40f);
            }
            if (!isDeath)
            {
                cooldownThrow -= Time.deltaTime;
            }
            if (target != null)
            {
                if (target.isDeath)
                {
                    listTarget.Remove(target);
                    SetTarget(null);
                }
            }
            if (listTarget.Count > 0)
            {
                for (int i = 0; i < listTarget.Count; i++)
                {
                    if (listTarget[i].isDeath)
                    {
                        RemoveCharacter(listTarget[i]);
                    }
                }
            }
        }
    }
    public void RemoveCharacter(Character character)
    {
        listTarget.Remove(character);
    }
    public void SetRightTarget()
    {
        if (listTarget.Count > 0)
        {
            //GetClosetTarget();
            for (int i = 0; i < listTarget.Count; i++)
            {
                if (listTarget[i] != null)
                {
                    if (listTarget[i] is Bot)
                    {
                        Bot bot = (Bot)listTarget[i];
                        bot.DeactiveImage();
                    }

                    if (Vector3.Distance(TF.position, listTarget[i].TF.position) <= GetDistanceClosetTarget())
                    {
                        SetTarget(listTarget[i]);
                        if (target is Bot)
                        {
                            Bot bot = (Bot)target;
                            bot.ActiveImage();
                        }
                    }
                    //else
                    //{
                    //    if (listTarget[i] is Bot)
                    //    {
                    //        Bot bot = (Bot)listTarget[i];
                    //        bot.DeactiveImage();
                    //    }
                    //}
                }

            }
        }

    }
    public virtual void FixedUpdate()
    {

    }
    public void CreateCircleCollider()
    {
        //CircleCollider currentCircle = Instantiate(circleCollider, TF);
        float xScale = circleCollider.transform.localScale.x;
        float zScale = circleCollider.transform.localScale.z;
        xScale = zScale = currentModel.radius * 2;
        circleCollider.transform.localScale = new Vector3(xScale, circleCollider.transform.lossyScale.y, zScale);
    }

    public void ChangeAnim(string newAnim)
    {
        if (currentAnim != newAnim)
        {
            characterAnim.ResetTrigger(currentAnim);
            currentAnim = newAnim;
            characterAnim.SetTrigger(currentAnim);
        }
    }
    public bool CheckIsTargetInRange(Character character)
    {
        if(character != null || !character.isDeath)
        {
            if (Vector3.Distance(TF.position, character.TF.position) <= GetDistanceClosetTarget())
            {
                SetTarget(character);
                return true;
            }
            else return false;
        }       
        else return false;
    }
    public void SetNewTarget(Character character)
    {
        if (character != null || !character.isDeath)
        {
            if(Vector3.Distance(TF.position, character.TF.position) <= GetDistanceClosetTarget())
            {
                SetTarget(character);
            }
        }
    }
    public void ChangeIdleAnim()
    {
        ChangeAnim(Constant.ANIM_IDLE);
    }
    public void ChangeDeathAnim()
    {
        ChangeAnim(Constant.ANIM_DEATH);
    }
    public virtual void ChangeWeapon(PrefabType prefabType)
    {
        if (newTF != null)
        {
            Destroy(newTF.gameObject);
        }
        newTF = Instantiate(currentModel.newHandTF, currentModel.rightHand.position, Quaternion.identity) as Transform;
        newTF.parent = currentModel.rightHand;
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate((WeaponBase)itemSO.ChangeWeaponItem(prefabType));
        currentWeapon.TF.SetParent(newTF);

        InitWeaponTransform(currentWeapon);
        InitTransformHolder(newTF);

    }
    private void InitTransformHolder(Transform tf)
    {
        tf.localPosition = Vector3.zero;
        tf.localRotation = Quaternion.identity;
    }
    private void InitWeaponTransform(WeaponBase weapon)
    {
        weapon.TF.localPosition = weapon.TF.position;
        weapon.TF.localRotation = weapon.TF.rotation;
        weapon.TF.localScale = weapon.TF.lossyScale;
    }
    
    public virtual void ChangeHat(PrefabType prefabType)
    {
        //Transform newTF = Instantiate(currentCharacterModel.newHandTF, currentCharacterModel.Head.position, Quaternion.identity) as Transform;
        // newTF.parent = currentCharacterModel.Head;
        if (currentHat != null)
        {
            Destroy(currentHat.gameObject);
        }
        currentHat = Instantiate((Hat)itemSO.ChangeSkinItem(prefabType));
        currentHat.TF.SetParent(currentModel.Head);
        InitHatTransform(currentHat);
        currentSkinItemList.Add(currentHat);
    }
    private void InitHatTransform(Hat hat)
    {
        hat.TF.localPosition = hat.TF.position;
        hat.TF.localRotation = hat.TF.rotation;
        hat.TF.localScale = hat.TF.lossyScale;
    }
    public void ChangeAccessory()
    {

    }
    public virtual void ChangeCharacterModel(PrefabType prefabType)
    {
        if (currentModel != null)
        {
            Destroy(currentModel.gameObject);
        }
        currentModel = Instantiate((CharacterModel)itemSO.ChangeSkinItem(prefabType),TF);
        currentModel.DeactiveLine();
        characterAnim = currentModel.animModel;
        currentSkinItemList.Add(currentModel);
    }    
    public virtual void ChangePant(ColorType colorType)
    {
        currentModel.currentPant.SetColorType(colorType);
        currentModel.currentPant.SetSkinMesh(itemSO.ChangeMaterial(colorType));
        currentSkinItemList.Add(currentModel.currentPant);
        for (int i = 0; i < itemSO.listPrefabSO.Count; i++)
        {
            if (currentModel.currentPant.colorType == itemSO.listPrefabSO[i].colorType)
            {
                currentModel.currentPant.SetValueItem(itemSO.listPrefabSO[i].itemType, itemSO.listPrefabSO[i].prefabType,
                   itemSO.listPrefabSO[i].colorType, itemSO.listPrefabSO[i].titleName, itemSO.listPrefabSO[i].cost, itemSO.listPrefabSO[i].description);
            }
        }
        currentModel.currentPant.SetPrefabType(currentModel.currentPant.prefabType);
    } 
    public float GetDistanceClosetTarget()
    {
        
        if (listTarget.Count > 0)
        {
            SetTarget(listTarget[0]);
            float min = Vector3.Distance(TF.position, target.TF.position);
            for (int i = 1; i < listTarget.Count; i++)
            {
                if (min > Vector3.Distance(TF.position, listTarget[i].TF.position))
                {
                    min = Vector3.Distance(TF.position, listTarget[i].TF.position);
                }
            }
            return min;
        }
        return 0;
    }
    public void ChangeTemporaryHat(PrefabType prefabType)
    {
        if (currentHat != null)
        {
            Destroy(currentHat.gameObject);
        }
        currentHat = Instantiate((Hat)itemSO.ChangeSkinItem(prefabType));
        currentHat.TF.SetParent(currentModel.Head);
        InitHatTransform(currentHat);
    }
    public virtual void ChangeTemporaryCharacterModel(PrefabType prefabType)
    {
        if (currentModel != null)
        {
            Destroy(currentModel.gameObject);
        }
        currentModel = Instantiate((CharacterModel)itemSO.ChangeSkinItem(prefabType), TF);
        currentModel.DeactiveLine();

        currentModel.ChangeAnim(Constant.ANIM_DANCE);
        characterAnim = currentModel.animModel;
    }
    public virtual void ChangeTemporaryPant(ColorType colorType)
    {
        currentModel.currentPant.SetColorType(colorType);
        currentModel.currentPant.SetSkinMesh(itemSO.ChangeMaterial(colorType));
        for (int i = 0; i < itemSO.listPrefabSO.Count; i++)
        {
            if (currentModel.currentPant.colorType == itemSO.listPrefabSO[i].colorType)
            {
                currentModel.currentPant.SetValueItem(itemSO.listPrefabSO[i].itemType, itemSO.listPrefabSO[i].prefabType,
                   itemSO.listPrefabSO[i].colorType, itemSO.listPrefabSO[i].titleName, itemSO.listPrefabSO[i].cost, itemSO.listPrefabSO[i].description);               
            }
        }        
    }
    public void SetTarget(Character target)
    {
        this.target = target;
    }
    
    public void AddSkinItemToList(SkinItem item)
    {
        currentSkinItemList.Add(item);
    }    

    public void UpWeaponSize()
    {
        Vector3 size = currentWeapon.TF.localScale;
        size += new Vector3(1, 1, 1);
        currentWeapon.TF.localScale = size;
    }
}
