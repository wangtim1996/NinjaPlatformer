using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ListHighScores : MonoBehaviour {

    public Text board;


	// Use this for initialization
	void Start () {
	    foreach(int i in GameManager.instance.scores)
        {
            print(i);
        }
        GameManager.instance.scores.Sort();
        int size = GameManager.instance.scores.Count;
        int numToList = (size < 8) ? size : 8;
        for(int i = 0; i < 8; i++)
        {

            board.text = board.text + GameManager.instance.scores[size - i - 1] + "\n";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
