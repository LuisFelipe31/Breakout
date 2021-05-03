using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int powerUpID;

    private float fallSpeed = 3;

    private Racket player;

    private void Awake()
    {
        player = GameObject.Find("Racket").GetComponent<Racket>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Racket")) 
        {
            switch (powerUpID)
            {
                case 0:
                    player.AddBorders();
                    Destroy(gameObject);
                    break;
                default:
                    Debug.LogError("No valid power up id");
                    break;
            }
        }
        else if (collision.CompareTag("Miss")) 
        {
            Destroy(this.gameObject);
        }
    }
}
