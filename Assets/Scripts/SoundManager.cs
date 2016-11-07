using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource sfx;
    public AudioSource mus;
    public static SoundManager instance = null;
    public float lowRange = 0.95f;
    public float hiRange = 1.05f;

	// Use this for initialization
	void Awake () {
	    if(instance == null)
        {
            instance = this;
        }
        else
        {

            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    public void PlaySingle (AudioClip clip)
    {
        float randomPitch = Random.Range(lowRange, hiRange);
        sfx.pitch = randomPitch;
        sfx.clip = clip;
        sfx.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
