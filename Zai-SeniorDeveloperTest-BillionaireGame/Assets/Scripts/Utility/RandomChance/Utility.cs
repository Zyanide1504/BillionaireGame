using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{

    public string RandomWithChance(List<Random_Chance> randomlist)
    {
        float chanceSum = 0;

        for (int i = 0; i < randomlist.Count; i++)
        {
            var current = randomlist[i];
            chanceSum += current.chance_weight;
            if (i == 0)
            {
                current.minChance = 0;
                current.maxChance = current.chance_weight;

            }
            else
            {

                current.minChance = randomlist[i - 1].maxChance;
                current.maxChance = current.minChance + current.chance_weight;

            }

        }

        float rand = Random.Range(0, chanceSum);

        for (int i = 0; i < randomlist.Count; i++)
        {
            var current = randomlist[i];

            if (rand >= current.minChance && rand < current.maxChance)
            {
                return current.outPut;
            }
        }

        return null;

    }

}

[System.Serializable]
public class Random_Chance 
{
    public string outPut;
    public float chance_weight;
    [HideInInspector]
    public float minChance;
    [HideInInspector]
    public float maxChance;
}
