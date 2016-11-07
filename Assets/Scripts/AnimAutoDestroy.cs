using UnityEngine;
using System.Collections;

public class AnimAutoDestroy : MonoBehaviour {
    public float delay = 0;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
