using UnityEngine;
using System.Collections;

public class SpawnPlayer2 : MonoBehaviour {

    public GameObject player2;

	// Use this for initialization
	void Start () {
	    if(GameManager.instance.player2)
        {
            Instantiate(player2, transform.position, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
