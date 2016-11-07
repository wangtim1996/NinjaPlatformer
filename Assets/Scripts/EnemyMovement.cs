using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public int flipped = -1;    //left
    public int speed = 3;
    Rigidbody2D rb2d;
    public SpriteRenderer sr;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if(Random.value < 0.5f)
        {
            flipped = -flipped;
            sr.flipX = !sr.flipX;
        }
    }
	
	// Update is called once per frame
	void Update () {
        float horz = flipped * speed;
        horz *= Time.deltaTime;
        transform.Translate(horz, 0, 0);

        //if(Physics2D.Raycast(transform.position, Vector3.right * flipped, 0.5f))

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (Physics2D.Raycast(transform.position, Vector3.right * flipped, 0.5f))
            {
                flipped = -flipped;
                sr.flipX = !sr.flipX;
            }
                
        }
    }
}
