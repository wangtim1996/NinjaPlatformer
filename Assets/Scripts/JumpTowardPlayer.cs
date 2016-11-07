using UnityEngine;
using System.Collections;

public class JumpTowardPlayer : MonoBehaviour {
    public float distToGround = 0.1f;
    GameObject target;
    Rigidbody2D rb2d;
    float time = 0.0f;
    public float jumpCooldown = 3.0f;

    EnemyMovement em;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        em = GetComponent<EnemyMovement>();

        if(GameManager.instance.player2 && !GameManager.instance.p2gameover)
        {
            GameObject p2 = GameObject.FindGameObjectWithTag("Player2");
            if(Vector3.SqrMagnitude(p2.transform.position - transform.position) < Vector3.SqrMagnitude(target.transform.position - transform.position))
            {
                target = p2;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > jumpCooldown)
        {
            time = jumpCooldown + 1;
        }
        if (IsGrounded() && time > jumpCooldown)
        {
            print("pounce");

            if (target != null)
            {
                RaycastHit2D hit;
                Vector2 dir = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);

                hit = Physics2D.Raycast(transform.position, dir);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Player2")
                    {
                        if(hit.collider.gameObject.transform.position.x < transform.position.x && em.flipped > 0 || hit.collider.gameObject.transform.position.x > transform.position.x && em.flipped < 0)
                        {
                            em.flipped = -em.flipped;
                            em.sr.flipX = !em.sr.flipX;
                        }


                        Vector2 move = dir.normalized;
                        move = move * 3;
                        move = move + new Vector2(0, 2);
                        rb2d.AddForce(move, ForceMode2D.Impulse);

                        target = GameObject.FindGameObjectWithTag("Player");

                        if (GameManager.instance.player2 && !GameManager.instance.p2gameover)
                        {
                            GameObject p2 = GameObject.FindGameObjectWithTag("Player2");
                            if (Vector3.SqrMagnitude(p2.transform.position - transform.position) < Vector3.SqrMagnitude(target.transform.position - transform.position))
                            {
                                target = p2;
                            }
                        }
                    }
                }

            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Player");

                if (GameManager.instance.player2 && !GameManager.instance.p2gameover)
                {
                    GameObject p2 = GameObject.FindGameObjectWithTag("Player2");
                    if (Vector3.SqrMagnitude(p2.transform.position - transform.position) < Vector3.SqrMagnitude(target.transform.position - transform.position))
                    {
                        target = p2;
                    }
                }
            }




            time = 0;
        }





        
	
	}
    bool IsGrounded()
    {
        bool ret = Physics2D.Raycast(transform.position - new Vector3(0.2f, 0, 0), -Vector3.up, distToGround + 0.1f);
        bool ret2 = Physics2D.Raycast(transform.position + new Vector3(0.2f, 0, 0), -Vector3.up, distToGround + 0.1f);
        //print("GROUNDED " + ret + ret2);
        return ret || ret2;
    }
}
