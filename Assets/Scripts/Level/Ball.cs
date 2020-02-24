﻿using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rBody;
    private Paddle playerPaddle;

    private bool isFlying = false;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        playerPaddle = LevelManager.Instance.playerPaddle;

        float spawnY = playerPaddle.ballSpawnPoint.position.y + this.GetComponent<SpriteRenderer>().bounds.extents.y;
        transform.position = new Vector2(playerPaddle.ballSpawnPoint.position.x, spawnY);

        isFlying = false;
        //rBody.velocity = new Vector2(Random.Range(1f, 6f), Random.Range(1f, 7f));
    }

    private void Update()
    {
        if (!isFlying)
        {
            transform.position = new Vector2(playerPaddle.transform.position.x, transform.position.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                LaunchBall();
            }
        }
    }

    private void LaunchBall()
    {
        isFlying = true;

        rBody.velocity = new Vector2(Random.Range(-6f, 6f), Random.Range(1f, 7f));
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //Debug.DrawLine(transform.position, collider.transform.position, Color.red, 2f);
            Vector2 newDirection = transform.position - collider.transform.position;

            if (newDirection.y < 0.15f)
            {
                newDirection = new Vector2(newDirection.x, 0.15f);
            }

            rBody.velocity = newDirection.normalized * rBody.velocity.magnitude;
        }
    }
}