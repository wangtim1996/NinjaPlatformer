using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {
    public float t = 2;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, t);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
