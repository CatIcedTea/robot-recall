using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Health>(out var health))
        {
            health.Damage(amount:10);

        }
    }
}
