using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    public Sprite sprite;
    public string skillName;
    public SkillDescription skillDescription;
    public SkillEffectAmount effectAmount;
    public int lv;
    public bool isUpgrade;
    public List<Buff> buffs = new List<Buff>();
    public enum Type {Buff, Debuff, Attack };
    public Type type;
    public Position posToUse;
    public Position posTarget;

    public string GetSkilDescription()
    {
        if(!isUpgrade)
        {
            return this.skillDescription.nonUpgrade;
        }
        else { return this.skillDescription.upgrade; }
    }
    public int GetSkillEffectAmount()
    {
        if (!isUpgrade) { return this.effectAmount.nonUpgrade; }
        else { return this.effectAmount.upgrade;}
    }
}


[System.Serializable]
public struct SkillDescription
{
    public string nonUpgrade;
    public string upgrade;
}
[System.Serializable]

//Luong dmg gay ra || Heal || Shield neu can
public struct SkillEffectAmount
{
    public int nonUpgrade;
    public int upgrade;
}
[System.Serializable]

// true false cho nhung vi tri cast dc chieu
public struct Position
{
    public List<bool> pos ;
}