using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float mageHealth, archerHealth, warriorHealth,ninjaHealth;
    public float mageDamage, archerDamage, warriorDamage, ninjaDamage;
    public float meleeRange;
    public float archerRange;
    public float mageRange;
    public float ninjaRange;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

}
