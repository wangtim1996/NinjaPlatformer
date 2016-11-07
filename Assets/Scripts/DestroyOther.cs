using UnityEngine;
using System.Collections;

public class DestroyOther : MonoBehaviour {

    public GameObject portal;
    public float xlo = -7.0f;
    public float xhi = 7.0f;
    public float ylo = 3.5f;
    public float yhi = 4.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            float x = Random.Range(xlo, xhi);
            float y = Random.Range(ylo, yhi);
            other.gameObject.transform.position = new Vector3(x,y,0);
            Instantiate(portal, other.gameObject.transform.position, Quaternion.identity);
            return;
        }
        if(other.gameObject.tag == "Player")
        {
            GameManager.instance.p1gameover = true;
        }
        if (other.gameObject.tag == "Player2")
        {
            GameManager.instance.p2gameover = true;
        }
        Destroy(other.gameObject, 3.0f);
    }
}
