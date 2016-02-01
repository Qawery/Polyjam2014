using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class Shooting : MonoBehaviour
{
    public Animator animator_;
    public GameObject bullet;
    public int speed;
	public bool alive;

	public Light barrelFlash;
	public int flashTimeout = 0;

    public AudioClip shotSound;
    public AudioClip reloadSound;

    int numBullets;
    bool shooting; 

    // Use this for initialization 
    void Start()
    {
        numBullets = 1;
        shooting = true;
		alive = true;
		speed = 10000;
		barrelFlash.enabled = false;

        animator_.SetBool("isShooting", false); 
    } 

    // Update is called once per frame
    void Update()
    {
		if(alive)
		{
	        if (Input.GetMouseButtonDown(0) && shooting == true) 
	        {
	            var b = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity) as GameObject; 

	            //b.transform.Rotate(new Vector3(90, transform.rotation.eulerAngles.y, 180)); 
	            b.GetComponent<Rigidbody>().AddForce(transform.forward * speed);

	            numBullets--; 

	            GetComponent<AudioSource>().PlayOneShot(shotSound); 

				gameObject.GetComponent<ParticleSystem>().Play();
				barrelFlash.enabled = true; 
                animator_.SetBool("isShooting", true); 
	        } 
	        
	        if (numBullets <= 0) 
	        { 
	            shooting = false; 
	            GetComponent<AudioSource>().PlayOneShot(reloadSound); 
	            numBullets = 1;
	            Invoke("reloadFinished", 1); 
	        } 

			if(barrelFlash.enabled)
			{
				flashTimeout++;
				if(flashTimeout >= 5)
				{
					barrelFlash.enabled = false;
					flashTimeout = 0;
				}
			}

		}else
		{
			barrelFlash.enabled=false;
		}
    } 

    void reloadFinished()
    {
        animator_.SetBool("isShooting", false); 
        shooting = true; 
    }

	public void playerDeath()
	{
		alive = false;
	}
} 