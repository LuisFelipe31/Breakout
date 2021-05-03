using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D rb;

    private Racket player;

    private GameManager gm;

    private float xSpeed;
    private float ySpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Racket").GetComponent<Racket>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.y > -0.5 && rb.velocity.y < 0.5)
        {
            if (collision.gameObject.CompareTag("Top")) 
            {
                rb.velocity = new Vector2(rb.velocity.x, -2);
            }
            else 
            {
                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -1);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 1);
                }
            }
        }

        if (collision.gameObject.CompareTag("Miss"))
        {
            Death();
        }
        else if (collision.gameObject.CompareTag("Brick")) 
        {
            collision.gameObject.GetComponent<Brick>().TakeDamage();
            gm.AddPoints();
            gm.ReduceBrickAmount();
        }
        else if (collision.gameObject.CompareTag("Racket")) 
        {
            gm.RestartCombo();

            if(transform.position.y >= collision.gameObject.transform.position.y - 0.1f) 
            {
                xSpeed = rb.velocity.x;
                ySpeed = Mathf.Abs(rb.velocity.y);

                rb.velocity = new Vector2(xSpeed, ySpeed);

                if (rb.velocity.x > -0.5 && rb.velocity.x < 0.5)
                {
                    if (rb.velocity.x < 0)
                    {
                        rb.velocity = new Vector2(-2, ySpeed);
                    }
                    else
                    {
                        rb.velocity = new Vector2(2, ySpeed);
                    }
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            if(transform.parent != null) 
            {
                LaunchBall();
            }
        } 
        if(transform.parent == null) 
        {
            if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) < 9 && Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) > 0)
            {
                if(rb.velocity.y > 0) 
                {
                    ySpeed = (10 - Mathf.Abs(rb.velocity.x));
                }
                else 
                {
                    ySpeed = (-10 + Mathf.Abs(rb.velocity.x));
                }
                rb.velocity = new Vector2(rb.velocity.x, ySpeed);
            }
        }
    }

    public void LaunchBall() 
    {
        xSpeed = Random.Range(-5, 5);
        ySpeed = 10 - Mathf.Abs(xSpeed);

        rb.velocity = new Vector2(xSpeed, ySpeed);

        transform.parent = null;
    }

    private void Death()
    {
        gm.RestartCombo();
        gm.UpdateLives();
        player.RemoveBorders();
        foreach (Transform child in gm.powerUpsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        Destroy(this.gameObject);
    }
}
