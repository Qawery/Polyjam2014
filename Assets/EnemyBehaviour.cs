using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
	private bool isActive = false;
	private bool isAlive = true;
	private int pathFinding = 0;
	private int atakCooldown = 0;
	public virtual int damage{ get {return 0;}}
	public virtual int maxEnemyHP{ get {return 0;}}
	public int enemyHP;

    public AudioClip audioAlert;
    public AudioClip audioDeath;

    private Animator _animator;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
		enemyHP = maxEnemyHP;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if(isAlive)
		{
			var distanceToPlayerVector = (player.transform.position - this.transform.position);
			var distanceToPlayer = (player.transform.position - this.transform.position).magnitude;
	//        var sqrOfDistanceToPlayer = distanceToPlayerVector.sqrMagnitude;
			if(isActive)
			{
				if (distanceToPlayer < 3 && Vector3.Angle(distanceToPlayerVector, transform.forward) < 45f)
		        {
		            _animator.SetBool("IsAttacking", true);

					if ((atakCooldown++) % 30 == 0) 
					{
						//zabieranie zycia graczowi
						player.GetComponent<Character>().reciveDamage(damage);
					}

		        }
		        else
		        {
		            _animator.SetBool("IsAttacking", false);
					atakCooldown=0;
		        }

				if (distanceToPlayer <= 8) 
				{
					if ((pathFinding++) % 5 == 0) 
					{
						agent.SetDestination (player.transform.position);
					}
				} else 
				{
					if ((pathFinding++) % 30 == 0) 
					{
						agent.SetDestination (player.transform.position);
					}
				}
			}else if(distanceToPlayer <= 30)
			{
				isActive = true;
				alertSound();
			}
		}
    }

	//przeciwnik otrzymuje obrazenia
	public void recieveDamage(int strength)
	{
		if(gameObject.GetComponent<ParticleSystem>() != null)
		{
			gameObject.GetComponent<ParticleSystem>().Play();
		}
		if (enemyHP - strength <= 0) 
		{
			enemyHP = 0;
			death();
		}else
		{
			enemyHP = enemyHP - strength;
		}
	}

	//dzwiek aktywacji
	public void alertSound()
	{
        GetComponent<AudioSource>().PlayOneShot(audioAlert);
	}

	//dzwiek smierci
	public void death()
	{
		isAlive = false;
        _animator.Play("Death");
        GetComponent<AudioSource>().PlayOneShot(audioDeath);
        Destroy(gameObject, 1);
    }
}
