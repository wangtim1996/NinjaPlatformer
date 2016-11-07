using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public GameObject bullet;
    public float speed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Fire(Vector2 dir, Vector3 pos, Vector3 initVel)
    {
        GameObject obj = (GameObject)Instantiate(bullet, pos, Quaternion.identity);
        Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
        rb2d.velocity = dir * speed +  new Vector2(initVel.x, 0);

        
    }
}
