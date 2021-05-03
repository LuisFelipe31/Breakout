using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] private Text points;
    [SerializeField] private Text lives;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject victoryScreen;
    // Start is called before the first frame update
    public void updatePoints(int point) 
    {
        if(point < 10) 
        {
            points.text = "00" + point.ToString();
        }
        else if(point < 100) 
        {
            points.text = "0" + point.ToString();
        }
        else
        {
            points.text = point.ToString();
        }
    }

    public void UpdateLives(int life) 
    {
        lives.text = life.ToString();
    }

    public void GameOver() 
    {
        gameOverScreen.SetActive(true);
    }

    public void Victory() 
    {
        victoryScreen.SetActive(true);
    }
}
