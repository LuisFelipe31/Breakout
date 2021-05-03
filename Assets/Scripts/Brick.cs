using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    private SpriteRenderer _sr;

    private int lives;

    [SerializeField] private GameObject[] powerUp;

    private GameManager gm;

    public int Lives 
    {
        get => lives;

        set 
        {
            if(lives != value) 
            {
                lives = value;
                UpdateColor(lives);
            }

        }
    }

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void TakeDamage() 
    {
        Lives--;
        if(Lives > 0) 
        {
            UpdateColor(Lives);
        }
        else 
        {
            SpawnPowerUp();
            Destroy(gameObject);
        }
    }

    public void UpdateColor(int currentLives) 
    { 
        if(currentLives == 3) 
        {
            _sr.color = Color.red;
        }
        else if(Lives == 2) 
        {
            _sr.color = Color.yellow;
        }
        else if(Lives == 1) 
        {
            _sr.color = Color.green;
        }
    }

    private void SpawnPowerUp() 
    {
        int chanceToSPawn = Random.Range(1, 101);

        if(chanceToSPawn <= 10) 
        {
            Instantiate(powerUp[Random.Range(0, powerUp.Length)], transform.position, Quaternion.identity, gm.powerUpsContainer.transform);
        }
    }
}
