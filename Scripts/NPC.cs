using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Person
{
    private int relationship;

    public int GetRelationship()
    {
        return relationship;
    }
    public void AddRelationship(int addAmount)
    {
        relationship += addAmount;
    }
    public void SubtractRelationship(int subAmount)
    {
        if (relationship > 0)
        {
            relationship -= subAmount;
        }
    }
}

