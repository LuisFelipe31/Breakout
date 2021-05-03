using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    public bool canMove;
    private bool hasBorders;

    private SpriteRenderer _sr;

    [SerializeField] private Ball ball;
    
    private GameObject thisBall;
    [SerializeField] private GameObject borders;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove) 
        {
            Movement();
        }
    }

    private void Movement()
    {
        bool rightmovement = Input.GetKey(KeyCode.RightArrow);
        bool leftmovement = Input.GetKey(KeyCode.LeftArrow);

        if (rightmovement)
        {
            transform.Translate(new Vector2(speed, 0) * Time.deltaTime);
        }
        else if (leftmovement)
        {
            transform.Translate(new Vector2(-speed, 0) * Time.deltaTime);
        }

        if (!hasBorders) 
        {
            if (transform.position.x >= 5.45f)
            {
                transform.position = new Vector2(5.45f, transform.position.y);
            }
            if (transform.position.x <= -5.45f)
            {
                transform.position = new Vector2(-5.45f, transform.position.y);
            }
        }
        else 
        {
            if (transform.position.x >= 5.05f)
            {
                transform.position = new Vector2(5.05f, transform.position.y);
            }
            if (transform.position.x <= -5.05f)
            {
                transform.position = new Vector2(-5.05f, transform.position.y);
            }
        }

    }

    public void SpawnBall() 
    {
        thisBall = Instantiate(ball.gameObject,
            new Vector2(transform.position.x, transform.position.y + 0.5f),
            Quaternion.identity);

        thisBall.transform.parent = transform;
    }

    public void AddBorders() 
    {
        borders.SetActive(true);
        hasBorders = true;
    }

    public void RemoveBorders() 
    {
        borders.SetActive(false);
        hasBorders = false;
    }
}
