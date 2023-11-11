using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEvent : MonoBehaviour
{

    NewAiBehaviour newAiBehaviour;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        newAiBehaviour = GetComponentInParent<NewAiBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShootArrow()
    {

        GameObject arrowFX = Instantiate(newAiBehaviour.Bullet, newAiBehaviour.SpawnPoint.position, newAiBehaviour.SpawnPoint.rotation);
        Rigidbody bulletRigidbody = arrowFX.GetComponent<Rigidbody>();
        if (!bulletRigidbody)
        {
            bulletRigidbody = arrowFX.AddComponent<Rigidbody>();
        }
        HS_ProjectileMover hS_ProjectileMover = arrowFX.GetComponent<HS_ProjectileMover>();
        if (hS_ProjectileMover != null)
            hS_ProjectileMover.Damage = newAiBehaviour._damage;
        //bulletRigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public void Death()
    {
        Destroy(newAiBehaviour.gameObject);
    }
}
