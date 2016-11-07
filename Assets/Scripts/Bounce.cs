using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

    public float distToGround = 0.1f;
    Rigidbody2D rb2d;
    float time = 0.0f;
    public float jumpCooldown = 1.0f;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > 1)
        {
            time = 1.1f;
        }
	    if(IsGrounded() && time > jumpCooldown)
        {
            print("bounce");
            rb2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
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
