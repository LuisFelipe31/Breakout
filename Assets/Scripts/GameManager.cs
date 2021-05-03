using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    [SerializeField] private GameObject[] spawnPoints = new GameObject[65];
    [SerializeField] private GameObject bricksContainer;
    private GameObject thisBrick;
    private GameObject ball;
    public GameObject powerUpsContainer;

    private Brick thisBrickScript;

    private Racket player;
    private UiManager ui;

    private int points;
    private int combo;
    private int lives;
    private int brickAmount = 0;

    private void Awake()
    {
        player = GameObject.Find("Racket").GetComponent<Racket>();
        ui = GameObject.Find("Canvas").GetComponent<UiManager>();
        powerUpsContainer = GameObject.Find("PowerUps");
    }

    void Start()
    {
        StartCoroutine(SpawnBricksRoutine());
        lives = 3;
        combo = 1;
        points = 0;
    }

    private IEnumerator SpawnBricksRoutine() 
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            thisBrick = Instantiate(brick, spawnPoints[i].transform.position, Quaternion.identity);
            thisBrick.transform.parent = bricksContainer.transform;
            thisBrickScript = thisBrick.GetComponent<Brick>();

            /* if(i < 13) 
             {
                 thisBrickScript.Lives = 3;
             }
             else if(i > 12 && i < 39) 
             {
                 thisBrickScript.Lives = 2;
             }
             else 
             {
                 thisBrickScript.Lives = 1;
             }*/
            
            // FOR EXTRA BRICK TYPES COMMENT THIS LINE AND UNCOMMENT THE LINES ABOVE IT
            thisBrickScript.Lives = 1;
            
            brickAmount++;

            yield return new WaitForSeconds(0.05f);
        }
        player.SpawnBall();
        player.canMove = true;
    }
    
    public void AddPoints() 
    {
        points += combo;
        combo++;

        ui.updatePoints(points);
    }

    public void RestartCombo() 
    {
        combo = 1;
    }

    public void UpdateLives() 
    {
        lives--;
        ui.UpdateLives(lives);
        if(lives <= 0) 
        {
            Destroy(player.gameObject);
            bricksContainer.SetActive(false);
            ui.GameOver();
        }
        else 
        {
            player.SpawnBall();
        }
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void ReduceBrickAmount() 
    {
        brickAmount--;

        if(brickAmount <= 0) 
        {
            ball = GameObject.Find("Ball(Clone)");
            Destroy(player.gameObject);
            Destroy(ball);
            bricksContainer.SetActive(false);
            ui.Victory();
        }
    }

}
