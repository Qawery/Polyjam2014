using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class CreateMap : MonoBehaviour {

    //public Transform brick; 
    public Transform collumn; 
    //public Transform player;
    //To jest caly murek w 1 miejscu. CYA!
    List<Transform> lista = new List<Transform>(); 

	// Use this for initialization
	/*
    void create_borders()
    {
        for (int x = -50; x <= 50; x++)
        {
            var tmp = Instantiate(brick, new Vector3(x, 1.0f, -50.0f), Quaternion.identity) as Transform;
            var tmp1 = Instantiate(brick, new Vector3(-50.0f, 1.0f, x), Quaternion.identity) as Transform;
            var tmp2 = Instantiate(brick, new Vector3(x, 1.0f, 50.0f), Quaternion.identity) as Transform;
            var tmp3 = Instantiate(brick, new Vector3(50.0f, 1.0f, x), Quaternion.identity) as Transform;
            //lista.Add(tmp);
        }
    }*/ 
    
    void create_columns()
    { 
        for (int i = 0; i < 100; i++)
        { 
            int x = Random.Range(-49, 49);
            int y = Random.Range(-49, 49); 
            var tmp = Instantiate(collumn, new Vector3(x, 1.0f, y), Quaternion.identity) as Transform;
        } 
    } 

    void Start () {
        //create_borders();
        create_columns(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
