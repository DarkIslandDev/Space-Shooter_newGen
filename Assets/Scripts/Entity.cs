using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EntityType
{
    Upgrade,
    Enemy
}
public class Entity : MonoBehaviour
{
    public EntityType entityType;
    public int lives;
    public float speed;
    private Vector2 min;
    private Vector2 position;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        speed = Random.Range(1.5f, 7f);
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;
        
        min = camera.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (entityType)
        {
            case EntityType.Enemy:
                switch (other.tag)
                {
                    case "Bullet":
                        lives--;
                        if (lives == 0)
                        {
                            Destroy(gameObject);
                        }
                        break;
                    case "Player":
                        Destroy(gameObject);
                        break;
                }
                break;
            case EntityType.Upgrade:
                if (other.CompareTag("Player"))
                {
                    //  TODO: bonus buffs
                    Destroy(gameObject);
                }
                break;
        }
    }
}