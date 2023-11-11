using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_Types
{
    Archer,
    Warrior,
    Mage,
    Ninja
}
public class NewAiBehaviour : MonoBehaviour
{
    Dictionary<AI_Types, int> aiModelIndices = new Dictionary<AI_Types, int>()
    {
        { AI_Types.Archer, 0 },
        { AI_Types.Warrior, 1 },
        { AI_Types.Mage, 2 },
        {AI_Types.Ninja,3 }
    };
    [Header("State")]
    public string currentState;
    public AI_Types aiTypes;
    [Header("References")]
    public List<GameObject> aiModel;
    public Animator animator;
    public NavMeshAgent agent;
    [Header("AttackRange")]
    public float range;
    [Header("EnemyValues")]
    public float _health;
    public float _damage;
    public new string tag = "Team1";
    [Header("VFX")]
    public GameObject Arrow;
    public GameObject MageBlast;
    public Transform spawnArcherFX;
    public Transform MageShotSpawn;

    private GameObject _bullet;
    private Transform _spawnPoint;
    private bool runOnce;
    public Transform SpawnPoint { get { return _spawnPoint; } }
    public GameObject Bullet { get { return _bullet; } }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Action<AI_Types> activateAiModel = (AI_Types aiType) => 
        {
            aiModel[aiModelIndices[aiType]].SetActive(true);
            animator = aiModel[aiModelIndices[aiType]].GetComponent<Animator>();
        };
        activateAiModel(aiTypes);

        switch (aiTypes)
        {
            case AI_Types.Archer:
                _bullet = Arrow;
                _spawnPoint = spawnArcherFX;
                _health = GameManager.Instance.archerHealth;
                range = GameManager.Instance.archerRange;
                _damage = GameManager.Instance.archerDamage;
                break;
            case AI_Types.Warrior:
                _health = GameManager.Instance.warriorHealth;
                range = GameManager.Instance.meleeRange;
                _damage = GameManager.Instance.warriorDamage;
                break;
            case AI_Types.Mage:
                _bullet = MageBlast;
                _spawnPoint = MageShotSpawn;
                _health = GameManager.Instance.mageHealth;
                range = GameManager.Instance.mageRange;
                _damage = GameManager.Instance.mageDamage;
                break;
            case AI_Types.Ninja:
                _health = GameManager.Instance.ninjaHealth;
                range = GameManager.Instance.ninjaRange;
                _damage = GameManager.Instance.ninjaDamage;
                break;
            default:
                break;
        }

    }

    private void FixedUpdate()
    {
        if(_health <= 0)
        {
            if(!runOnce)
            animator.SetTrigger("isDeath");
            runOnce = true;
        }
    }
    public float Health { get { return _health; } }
    
    public void TakeDamage(float damageAmount)
    {
        Debug.Log(aiModel + " Got damaged " + damageAmount + " HP! " + _health + " HP Remaining.");
        if (_health > 0)
        {
            _health -= damageAmount;
        }
        if(_health <= 0)
        {
            _health = 0;
            animator.SetBool("isDead",true);
        }
    }


    private void Update()
    {
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (animator != null)
        {
            AnimationClip currentClip = GetCurrentAnimatorClip(animator, 0);
            // Store the animation name as a string
            if (currentClip != null)
            {
                currentState = currentClip.name;
            }
            else
            {
                currentState = "No animation playing";
            }
        }
        else
        {
            currentState = "Animator reference not set";
        }
    }
    private AnimationClip GetCurrentAnimatorClip(Animator anim, int layer)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(layer);
        return anim.GetCurrentAnimatorClipInfo(layer)[0].clip;
    }

   
}
