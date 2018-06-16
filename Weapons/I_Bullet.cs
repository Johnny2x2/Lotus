using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Bullet : MonoBehaviour {

    
    public float DestroyTime = 1f; //Bullet time alive

    public float BulletVelocity = 100f; //Bullet sppd

    public int Damage = 30; //Bullet Damage On Hit

    [SerializeField]
    private bool Exploding = false; //Am I exploding?

    [SerializeField]
    private GameObject Explosion; //Partical Effect

    Rigidbody rb; 
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce( transform.forward * BulletVelocity); //Spawn object with initial velocity == BulletVelocity
	}

    void Update()
    {
        Destroy(gameObject, DestroyTime); //Destroy After Some time
    }
	
    void OnTriggerEnter(Collider other)
    {
        if (Exploding){ //If bomb type
            Instantiate(Explosion, transform.position, Quaternion.identity); //Spawn Explosion
        }
        //If I hit someone with Health
        if (other.GetComponent<Health>() != null){
            other.GetComponent<Health>().TakeDamage(Damage);  //Do damage
            Debug.Log("Hit player and Did :" + Damage.ToString());
        }
        Destroy(gameObject); //Destroy Bullet
    }
}
