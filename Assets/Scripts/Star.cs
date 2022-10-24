using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed;
    private Vector2 min;
    private Vector2 max;
    private Vector2 position;
    private Camera camera;
    
    private void Awake()
    {
        camera = Camera.main;
    }
    
    private void Update()
    {
        position = transform.position;
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        transform.position = position;
        
        min = camera.ViewportToWorldPoint(new Vector2(0, 0));
        max = camera.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.y < min.y)
        {
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
    }
}