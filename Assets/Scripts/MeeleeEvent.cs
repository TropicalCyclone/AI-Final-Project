using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeEvent : MonoBehaviour
{
    NewAiBehaviour aiBehaviour;
    private NewAiBehaviour _enemyInRange;
    [SerializeField] private MeeleeEvent meelee;
    // Start is called before the first frame update
    void Start()
    {
        aiBehaviour = GetComponentInParent<NewAiBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Swing()
    {
        if (_enemyInRange)
        {
            if (meelee)
            {
                meelee.Swing();
                return;
            }
            else
            {
                if(_enemyInRange)
                _enemyInRange.TakeDamage(aiBehaviour._damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (aiBehaviour != null)
        {
            if (other.gameObject.tag == aiBehaviour.tag)
            {
                _enemyInRange = other.gameObject.GetComponent<NewAiBehaviour>();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == aiBehaviour.tag)
        {
            _enemyInRange = null;
        }
    }

    public void Death()
    {
        Destroy(aiBehaviour.gameObject);
    }
}
