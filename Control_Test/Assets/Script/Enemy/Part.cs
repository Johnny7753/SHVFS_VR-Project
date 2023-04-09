using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    FlyingDragon,    
    Murloc,
    Crab,
    SmallCrab,
    MurlocMother,
    Ghost
};
//this is the type for enemy to change
public enum SwitchType
{
    EnemyLeft,
    Time
}
[System.Serializable]
public class Part
{
    public SwitchType switchType;
    public EnemyType enemyType;
    public int enemyNum;
    public int switchTime;
    public int leftEnemy;
}
