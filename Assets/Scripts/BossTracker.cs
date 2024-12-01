using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTracker : MonoBehaviour
{
    public int defeatedBossCount = 0; // Tracks the number of defeated bosses
    public int totalBosses = 2; // Set this to the number of bosses in the dungeon
    public GameObject YouWin; // Assign the hole prefab in the Inspector

    public void BossDefeated()
    {
        defeatedBossCount++;
        if (defeatedBossCount >= totalBosses)
        {
            Destroy(YouWin);
        }
    }
}