using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float StartingHealth;
    private float health;
    public float Health{
        get{
            return health;
        }
        set{
            health = value;
            Debug.Log(health);
            if(health <= 0f){
                Destroy(gameObject);
            }
        }
    }
    public float GetHealth() {
        return health;
    }
    void Start()
    {
        if (StartingHealth <= 0) {
            Debug.LogWarning("Starting health is not set for " + gameObject.name);
        }
        Health = StartingHealth;   
    }
    
}

