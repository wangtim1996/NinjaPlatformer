using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage = 4;
    public AudioSource sfx;
    public AudioClip onHit;

    SpriteRenderer sr;
    BoxCollider2D bc;

    public float lowRange = 0.95f;
    public float hiRange = 1.05f;
    // Use this for initialization
    void Start () {
        sr = GetComponentInChildren<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter2D(Collision2D other)
    {
        PlaySingle(onHit);
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("TakeDamage", damage);
        }
        //Destroy(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        float randomPitch = Random.Range(lowRange, hiRange);
        sfx.pitch = randomPitch;
        sfx.clip = clip;
        sfx.Play();

        sr.enabled = false;
        bc.enabled = false;

        Destroy(gameObject, sfx.clip.length);

    }
}
