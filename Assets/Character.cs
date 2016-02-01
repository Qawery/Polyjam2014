using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float angle; 
	private bool alive = true;

	public Light visionALongLight;	//latarka
	public Light visionAShortLight;
	public Light visionBLight;	//niebieska
	public Light visionBTopLight;
	public Light visionCLight;	//czerwona

	private int visionMode = 0;	//tryb wizji, 0-latarka, 1-niebieska, 2-czerwona

    public int VisionMode { get { return visionMode; } }

	public AudioClip visionA;//dzwieki do zmieniania wizji
	public AudioClip visionB;
	public AudioClip visionC;
		
	public AudioClip playerHit;
	public AudioClip playerDeath;
	public AudioClip playerStepA;
	public AudioClip playerStepB;
	public AudioClip playerStepFull;

    public int maxPlayerHP = 100;
	private int playerHP;
	private float speed = 1.5f;
    private GameObject mainCamera_;

    private Animator animator_; 
    private Animator animator_cycki; 
    private Animator animator_nogi; 

    public int getPlayerHp()
    {
        return playerHP; 
    }

	// Use this for initialization
	void Start ()
	{
        playerHP = maxPlayerHP;

		//ustawienie wizje na latarke
		visionALongLight.enabled = true;
		visionAShortLight.enabled = true;
		visionBLight.enabled = false;
		visionBTopLight.enabled = false;
		visionCLight.enabled = false;

        mainCamera_ = GameObject.Find("Main Camera"); 
        animator_ = GameObject.Find("laska").GetComponent<Animator>();

        //animator_ = 

        //animator_cycki = GameObject.Find("laska_cycki").GetComponent<Animator>();
        //animator_nogi = GameObject.Find("laska_nogi").GetComponent<Animator>(); 

        animator_.SetBool("isWalking", false);
        animator_.SetBool("isShooting", false);
        animator_.SetBool("isAttacked", false);
        animator_.SetBool("isDying", false);
        
        //animator_cycki.SetBool("isWalking", false);
        //animator_cycki.SetBool("isShooting", false);
        //animator_cycki.SetBool("isDying", false);

        //animator_nogi.SetBool("isWalking", false);
        //animator_nogi.SetBool("isShooting", false);
        //animator_nogi.SetBool("isDying", false); 
	}
	
	// Update is called once per frame
	void Update () 
    {
        //animator_.SetBool("isAttacked", false); 

		if(alive)
		{
			//obsluga zmiany wizji
			if(Input.GetKeyDown(KeyCode.Q))
			{
				visionMode++;
				if(visionMode >= 3)
				{
					visionMode = 0;
				}

				Debug.Log(visionMode);
				if(visionMode == 0)	//latarka
				{
					GetComponent<AudioSource>().PlayOneShot(visionA);
					visionALongLight.enabled = true;
					visionAShortLight.enabled = true;
					visionBLight.enabled = false;
					visionBTopLight.enabled = false;
					visionCLight.enabled = false;
				}
				if(visionMode == 1)	//niebieska
				{
					GetComponent<AudioSource>().PlayOneShot(visionB);
					visionALongLight.enabled = false;
					visionAShortLight.enabled = false;
					visionBLight.enabled = true;
					visionBTopLight.enabled = true;
					visionCLight.enabled = false;
				}
				if(visionMode == 2)	//czerwona
				{
					GetComponent<AudioSource>().PlayOneShot(visionC);
					visionALongLight.enabled = false;
					visionAShortLight.enabled = false;
					visionBLight.enabled = false;
					visionBTopLight.enabled = false;
					visionCLight.enabled = true;
				}
			}

			//obsluga chodzenia
	        Vector3 moveVector = new Vector3();
	        var hAxis = Input.GetAxis("Horizontal");
	        var vAxis = Input.GetAxis("Vertical");
	        if (hAxis != 0)
	        {
	            moveVector.x = hAxis > 0 ? 1f : -1f; 
                animator_.SetBool("isWalking", true); 

                //animator_cycki.SetBool("isWalking", true);
                //animator_nogi.SetBool("isWalking", true); 
	        }
	        if (vAxis != 0)
	        {
	            moveVector.z = vAxis > 0 ? 1f : -1f; 
                animator_.SetBool("isWalking", true); 

                //animator_cycki.SetBool("isWalking", true);
                //animator_nogi.SetBool("isWalking", true); 
	        }
            if (hAxis == 0 && vAxis == 0)
            {
                //animator_cycki.SetBool("isWalking", false);
                //animator_nogi.SetBool("isWalking", false);

                animator_.SetBool("isWalking", false);
            } 

	        transform.position += (moveVector * 0.1f)*speed;
	       
	        GetComponent<Rigidbody>().MovePosition(transform.position + moveVector * Time.deltaTime);

			//obsluga myszy
			var observationPoint = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
			observationPoint.y = 0.5f;
            //transform.Find("laska_cycki").LookAt(observationPoint); 
			transform.LookAt(observationPoint);
            //animator_.SetBool("isWalking", true); 

	        var cameraPosition = this.transform.position;
	        cameraPosition.Set(cameraPosition.x, 30f, cameraPosition.z);
	        Camera.main.transform.position = cameraPosition;
		}
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Finish")
        {
            mainCamera_.GetComponent<EndLevel>().setPlayerFinished(true); 
        }
    }

	void OnCollisionEnter(Collision collider)
	{
		if (collider.gameObject.tag == "Enemy") 
		{
            animator_.SetBool("isAttacked", true); 
			GetComponent<AudioSource>().PlayOneShot(playerHit); 
		}
	}

	//gracz otrzymuje obrazenia
	public void reciveDamage(int damage)
	{
		if(alive)
		{
			GetComponent<AudioSource>().PlayOneShot(playerHit);
			if(playerHP - damage <= 0)
			{
				playerHP = 0;
				death();
			}else 
			{
				playerHP = playerHP - damage;
			}
		}
	}

	//gracz umarl
	void death()
	{
		alive = false;

		GetComponent<AudioSource>().PlayOneShot(playerDeath);

		gameObject.GetComponentInChildren<Shooting> ().playerDeath();

		visionALongLight.enabled = false;
		visionAShortLight.enabled = false;
		visionBLight.enabled = false;
		visionBTopLight.enabled = false;
		visionCLight.enabled = false;
        mainCamera_.GetComponent<EndLevel>().setPlayerKilled(true);
        animator_cycki.SetBool("isDying", true);
        animator_nogi.SetBool("isDying", true); 
	}

	//uzywane do ochrony gracza
	public void setDead()
	{
		alive = false;
	}
}
