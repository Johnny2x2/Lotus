using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int MaxHealth = 100;

    public int CurrentHealth;

    [SerializeField]
    MonoBehaviour Deathcall;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }
    void Update()
    {
        if(CurrentHealth <= 0)
        {
            if(Deathcall != null)
            {
                Deathcall.SendMessage("OnDeath");
            }
        }
    }

    public void TakeDamage(int Amount)
    {
        CurrentHealth -= Amount;
    }
}
