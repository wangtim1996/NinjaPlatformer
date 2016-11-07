using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameManager : MonoBehaviour {
    public Text gameOverText;
    public Text pointsText;
    public Text p2readyText;
    public bool gameover = true;
    public static GameManager instance = null;
    public GameObject enemyContainer;
    int points = 0;
    public int level = 0;
    int currentLevel = -1;
    public bool doneSpawning = false;

    public string[] levelList;
    public GameObject[] enemyList;

    public bool player2 = false;
    public bool p2gameover = true;
    public bool p1gameover = true;

    public ArrayList scores;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {

            Destroy(gameObject);
        }
        LoadScores();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
        if(player2)
        {
            gameover = p1gameover && p2gameover;
        }
        else
        {
            gameover = p1gameover;
        }
        if(enemyContainer == null)
        {
            enemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer");
        }
        if(!gameover && SceneManager.GetActiveScene().name != "mainmenu" && SceneManager.GetActiveScene().name != "credits" && SceneManager.GetActiveScene().name != "highscores")
        {
            if (pointsText == null)
            {
                pointsText = GameObject.FindGameObjectWithTag("PointsText").GetComponent<Text>();
            }

            pointsText.text = "Points: " + points;
        }
	    if(gameover && SceneManager.GetActiveScene().name != "mainmenu" && SceneManager.GetActiveScene().name != "credits" && SceneManager.GetActiveScene().name != "highscores")
        {
            if(gameOverText == null)
            {
                gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<Text>();
            }
            
            gameOverText.text = "Game Over: Press <Enter> or Start to Reset";
            if (Input.GetButtonDown("StartP1"))
            {
                SceneManager.LoadScene("mainmenu", LoadSceneMode.Single);
                gameover = true;
                p1gameover = true;
                p2gameover = true;
                doneSpawning = false;

                scores.Add(points);
                SaveScores(scores);
                points = 0;
                level = 0;
            }
        }
        else if(SceneManager.GetActiveScene().name != "mainmenu" && SceneManager.GetActiveScene().name != "credits" && SceneManager.GetActiveScene().name != "highscores" && doneSpawning && enemyContainer.transform.childCount == 0 )
        {
            if (gameOverText == null)
            {
                gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<Text>();
            }
            gameover = false;
            p1gameover = false;
            p2gameover = false;
            doneSpawning = false;
            gameOverText.text = "Level COMPLETE";
            level++;
            points += level * 10;
            
            Invoke("LoadRandomLevel", 2.0f);
        }

        if (SceneManager.GetActiveScene().name == "mainmenu")
        {
            if(Input.GetButtonDown("StartP1"))
            {
                LoadRandomLevel();
            }
            else if(Input.GetButtonDown("SwapP1"))
            {
                SceneManager.LoadScene("credits", LoadSceneMode.Single);
            }
            else if(Input.GetKeyDown(KeyCode.H))
            {
                SceneManager.LoadScene("highscores", LoadSceneMode.Single);
            }

            if(p2readyText == null)
            {
                p2readyText = GameObject.FindGameObjectWithTag("P2Ready").GetComponent<Text>();
            }
            if (Input.GetButtonDown("FireP2"))
            {
                player2 = true;
                p2readyText.text = "Player 2 Ready";
                print("p2 on");
            }
            if (Input.GetButtonDown("JumpP2"))
            {
                player2 = false;
                p2readyText.text = "Player 2 Press Shoot to Join";
                print("p2 off");    
            }
            
        }

        if ((SceneManager.GetActiveScene().name == "credits" || SceneManager.GetActiveScene().name == "highscores") && Input.GetButtonDown("JumpP1"))
        {
            print("wtf");
            SceneManager.LoadScene("mainmenu", LoadSceneMode.Single);
        }

        
    }

    public void AddPoints(int x)
    {
        points += x;
        if (pointsText == null)
        {
            pointsText = GameObject.FindGameObjectWithTag("PointsText").GetComponent<Text>();
        }

        pointsText.text = "Points: " + points;
    }

    public void LoadRandomLevel()
    {
        int levelIndex = UnityEngine.Random.Range(0, levelList.Length);
        print("loading level " + levelIndex);
        //while ((levelIndex = Random.Range(0, levelList.Length)) != currentLevel);
        SceneManager.LoadScene(levelList[levelIndex], LoadSceneMode.Single);
        gameover = false;
        p1gameover = false;
        p2gameover = false;
        doneSpawning = false;

    }

    public void SaveScores(ArrayList scores)
    {
        print("SAVING");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/scores.dat");

        HighScores hs = new HighScores();
        hs.scores = scores;

        bf.Serialize(file, hs);
        file.Close();
    }

    public void LoadScores()
    {
        if(File.Exists(Application.persistentDataPath + "/scores.dat"))
        {
            print("LOADING");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/scores.dat", FileMode.Open);
            HighScores hs = (HighScores)bf.Deserialize(file);

            scores = hs.scores;
            file.Close();

            foreach(int i in hs.scores)
            {
                print("LOADING " + i);
            }
        }
        else
        {
            scores = new ArrayList();
        }
        
    }

}

[Serializable]
class HighScores
{
    public ArrayList names;
    public ArrayList scores;
}
