using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health = 10;
    public AudioClip sfx;
    public AudioSource src;
    public int rewardVal;
    public GameObject reward;

    bool dead = false;

    SpriteRenderer sr;
    BoxCollider2D bc;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(health <= 0 && !dead)
        {
            rewardVal += GameManager.instance.level;
            for(int i = 0; i < rewardVal; i++)
            {
                GameObject coin = (GameObject)Instantiate(reward, transform.position, Quaternion.identity);
                coin.GetComponent<Rigidbody2D>().velocity = (new Vector2(Random.Range(-2.0f, 2.0f), 4));
            }


            PlaySingle(sfx);
            dead = true;
        }
	}

    void TakeDamage(int damage)
    {
        print("take damage");
        health -= damage;
    }

    public void PlaySingle(AudioClip clip)
    {
        float randomPitch = Random.Range(0.95f, 1.05f);
        src.pitch = randomPitch;
        src.clip = clip;
        src.Play();

        sr.enabled = false;
        bc.enabled = false;
        Destroy(gameObject, src.clip.length);

    }
}
