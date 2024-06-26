using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum Stats
{
    Money,
    Charm,
    Knowledge,
    Strength
}

public class Player : Person
{
    public int money;
    public int charm;
    public int knowledge;
    public int strength;
    public bool CheckStat(Stats stats, int limitation)
    {
        switch (stats)
        {
            case Stats.Money:
                if (limitation <= money) return true;
                return false;
            case Stats.Charm:
                if(limitation <= charm) return true;
                return false;
            case Stats.Knowledge:
                if(limitation <= knowledge) return true;
                return false;
            case Stats.Strength:
                if(limitation <= strength) return true;
                return false;
            default:
                return false;
        }
    }
    public int AddStat(Stats stats, int amount)
    {
        switch (stats)
        {
            case Stats.Money:
                money += amount;
                return money;
            case Stats.Charm:
                charm += amount;
                return charm;
            case Stats.Knowledge:
                knowledge += amount;
                return knowledge;
            case Stats.Strength:
                strength += amount;
                return strength;
            default:
                return 0;
        }
    }
    public int SubtractStat(Stats stats, int amount)
    {
        return AddStat(stats, -amount);
    }
}
