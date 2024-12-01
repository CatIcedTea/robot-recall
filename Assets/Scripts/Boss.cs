using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossTracker bossTracker; // Reference to the BossTracker script

    private void Start()
    {
        // Get the Health component and subscribe to the Died event
        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.Died.AddListener(OnBossDied);
        }
        else
        {
            Debug.LogError("Health component not found on the boss!");
        }
    }

    private void OnBossDied(int remainingHp)
    {
        // Notify the BossTracker that this boss has been defeated
        if (bossTracker != null)
        {
            bossTracker.BossDefeated();
        }
        else
        {
            Debug.LogError("BossTracker is not assigned for this boss!");
        }

        // Destroy the boss object after death
        Destroy(gameObject);
    }
}
