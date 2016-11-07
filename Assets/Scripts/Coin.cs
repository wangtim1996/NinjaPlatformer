using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

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
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            GameManager.instance.AddPoints(5);
            PlaySingle(onHit);
        }
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
