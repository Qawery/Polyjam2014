using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	int damage=1;
    int enemyLayer;

	// Use this for initialization
	void Start ()
	{
        enemyLayer = LayerMask.NameToLayer("enemy");
		gameObject.GetComponent<ParticleSystem>().Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
			collision.gameObject.GetComponent<EnemyBehaviour>().recieveDamage(damage);
			Destroy(gameObject); 
        }
		else if(collision.gameObject.layer == LayerMask.NameToLayer("bullet"))
		{
			//kolizja z innym pociskiem, nie rb nic.
		}
		else
		{
			Destroy(gameObject); 
		}
        
    } 
}
