using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu = 0,
    Gameplay = 1,
    ShoppingView = 2,
    Win = 3,
    Lose = 4,
    Setting = 5,
    Revive = 6
}
public enum ItemType
{
    Hat = 0,
    Pant = 1,
    Accessory = 2,
    Skin = 3,
    Weapon = 4
}
public enum PrefabType
{
    Arrow = 0,
    Cowboy = 1,
    Crown = 2,
    BunnyEar = 3,
    Cap = 4,
    Police = 5, 
    StrawHat = 6,
    Headphone = 7,
    Horn = 8,
    Beard = 9,

    
    Normal = 30,
    America = 31,
    Batman = 32,
    Dot = 33,
    Onion = 34,
    Panther = 35,
    Pokemon = 36,
    Purple = 37,
    Skull = 38,
    Rainbow = 39,


    Axe = 50,
    Knife = 51,
    Hammer = 52,
    ChupachupCandy = 53,
    ChristmasCandy = 54,
    IceScreamCandy = 55,
    CircleCandy = 56,
    Boomerang = 57,
    Z = 58,

    NormalSkin = 70,
    Angel = 71,
    Witch = 72,
    Devil = 73,

    //AngelWing = 80,
    //AngelRingHat = 81,
    //AngelPant = 82,
    //WitchBook = 83,
    //WitchHat = 84,
    //HornDevil = 85,
    //DevilTail = 86,
    //DeadpoolSword = 87,
    //ThorHat = 88
}
public enum ColorType
{
    None = 0,
    America = 1,
    Batman = 2,
    Dot = 3,
    Onion = 4,
    Panther = 5,
    Pokemon = 6,
    Purple = 7,
    Skull = 8,
    Rainbow = 9,
}
public class Constant 
{
    public const string ANIM_IDLE = "Idle";
    public const string ANIM_RUN = "Run";
    public const string ANIM_THROW = "Throw";
    public const string ANIM_DANCE = "Dance";
    public const string ANIM_ATTACK = "Attack";
    public const string ANIM_DEATH = "Death";
    public const string ANIM_DANCE_WIN = "DanceWin";
    public const string ANIM_IN = "In";
    public const string ANIM_OUT = "Out";
    public const string ANIM_ON = "On";
    public const string ANIM_OFF = "Off";
    public const string ANIM_LOADING = "Loading";
    public const string ANIM_FADE_ENDLOADING = "EndLoading";
    public static IdleState IDLE_STATE = new IdleState();
    public static PatrolState PATROL_STATE = new PatrolState();
    public static AttackState ATTACK_STATE = new AttackState();
    public static DeadState DEAD_STATE = new DeadState();
}
