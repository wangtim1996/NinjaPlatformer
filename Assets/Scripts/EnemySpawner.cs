using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyContainer;
    public GameObject[] enemies;
    public GameObject[] spawners;
    int i = 0;
    public float rate;

	// Use this for initialization
	void Start () {
        enemies = new GameObject[5 + (2 * GameManager.instance.level)];
        for (int i = 0; i < 5 + (2 * GameManager.instance.level); i++)
        {
            int enemyIndex = Random.Range(0, GameManager.instance.enemyList.Length);
            enemies[i] = GameManager.instance.enemyList[enemyIndex];
            
        }
        
        InvokeRepeating("Spawn", 0,rate);
	}
	
	// Update is called once per frame
	void Update () {
        if (i >= enemies.Length)
        {
            foreach(GameObject g in spawners)
            {
                Animator anim = g.GetComponent<Animator>();
                anim.SetBool("Finished", true);
                Destroy(g, 1.0f);
            }
            GameManager.instance.doneSpawning = true;
            Destroy(gameObject, 1.0f);
        }
    }

    void Spawn()
    {
        if(i < enemies.Length)
        {

            int sindex = Random.Range(0, spawners.Length);
            print("spawner" + sindex);
            GameObject enemy = (GameObject)Instantiate(enemies[i], spawners[sindex].transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyHealth>().health += GameManager.instance.level;
            if(enemyContainer == null)
            {
                enemyContainer = GameObject.FindGameObjectWithTag("EnemySpawner");
            }
            enemy.transform.SetParent(enemyContainer.transform);
            i++;
        }
    }
}
