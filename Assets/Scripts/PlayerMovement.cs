using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    Animator anim;
    SpriteRenderer sr;
    Rigidbody2D rb2d;
    BoxCollider2D bc;
    bool canJump = true;
    bool dead = false;

    public float flipped = 1;
    public float speed = 1;
    public float distToGround = 0;
    public float jump = 5;

    public AudioSource sfx;
    public AudioClip jumpsfx;
    public AudioClip deathsfx;
    public Weapon currWeapon;

    public string horzInput;
    public string jumpInput;
    public string fireInput;

    public float lowRange = 0.95f;
    public float hiRange = 1.05f;



    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        /*
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Terrain"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyBullet"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pickup"), false);
        */
    }
	
	// Update is called once per frame
	void Update () {
        if(!dead)
        {
            float horz = Input.GetAxis(horzInput) * speed;
            if (horz != 0)
            {
                anim.SetBool("Walking", true);
                if (horz < 0)
                {
                    sr.flipX = true;
                    flipped = -1;
                }
                else
                {
                    sr.flipX = false;
                    flipped = 1;
                }
            }
            else
            {
                anim.SetBool("Walking", false);
            }

            horz *= Time.deltaTime;
            transform.Translate(horz, 0, 0);

            if (Input.GetButtonDown(jumpInput) && IsGrounded())
            {
                SoundManager.instance.PlaySingle(jumpsfx);
                rb2d.velocity = new Vector2(0, jump);
            }

            if (Input.GetButtonDown(fireInput))
            {
                currWeapon.Fire(Vector2.right * flipped, transform.position, rb2d.velocity);
            }
            if(Input.GetButtonUp(jumpInput) && rb2d.velocity.y > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y/2);
            }
        }
        

    }
    

    bool IsGrounded()
    {
        bool ret = Physics2D.Raycast(transform.position - new Vector3(0.2f, 0, 0), -Vector3.up, distToGround + 0.1f);
        bool ret2 = Physics2D.Raycast(transform.position + new Vector3(0.2f, 0, 0), -Vector3.up, distToGround + 0.1f);
        //print("GROUNDED " + ret + ret2);
        return ret || ret2;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBullet")
        {
            OnDeath(deathsfx);
        }
    }

    void OnDeath(AudioClip clip)
    {
        float randomPitch = Random.Range(lowRange, hiRange);
        sfx.pitch = randomPitch;
        sfx.clip = clip;
        sfx.Play();

        /*
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Terrain"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyBullet"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pickup"), true);
        */
        bc.enabled = false;

        dead = true;

        

        rb2d.AddForce(new Vector2(Random.Range(-5.0f, 5.0f), 7), ForceMode2D.Impulse);
        rb2d.freezeRotation = false;
        rb2d.AddTorque((randomPitch - 1) * 360, ForceMode2D.Impulse);

        if(gameObject.tag == "Player")
        {
            print("P1 died");
            GameManager.instance.p1gameover = true;
        }
        else if (gameObject.tag == "Player2")
        {
            print("P1 died");
            GameManager.instance.p2gameover = true;
        }


        //sr.enabled = false;
        Destroy(gameObject, 10.0f);
    }


}
