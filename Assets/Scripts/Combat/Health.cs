using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;

    public void TakeDamage(float ammount)
    {
        health -= ammount;
        if (health < 0) health = 0;
        print(health);
    }
}
