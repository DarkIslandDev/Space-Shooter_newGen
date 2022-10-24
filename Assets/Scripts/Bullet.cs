using UnityEngine;

public enum BulletType
{
    laser
}

public class Bullet : MonoBehaviour
{
    public BulletType bulletType;
    public float speed = 8;
    private Vector2 position;
    private Vector2 max;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        position = transform.position;
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        max = camera.ViewportToWorldPoint(new Vector2(1, 1));
        transform.position = position;
        
        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Entity"))
        {
            LevelManager.instance.currentLevel.levelScore += 100;
            Destroy(gameObject);
        }
    }
}