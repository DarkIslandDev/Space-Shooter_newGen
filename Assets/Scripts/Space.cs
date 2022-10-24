using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Space : MonoBehaviour
{
    public GameObject star;
    public int maxStars;

    private Color[] starColors =
    {
        new Color(0.5f, 0.5f, 1f), // blue
        new Color(0,1f,1f), // green
        new Color(1f,1f,0), // yellow
        new Color(1f,0,0), //red
    };

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;

        Vector2 min = camera.ViewportToWorldPoint(new Vector2(0,0));

        Vector2 max = camera.ViewportToWorldPoint(new Vector2(1, 1));

        for (int i = 0; i < maxStars; i++)
        {
            GameObject strGO = Instantiate(star, transform.position, Quaternion.identity);
            
            strGO.transform.SetParent(transform);
            strGO.transform.localScale = new Vector3(1, 1, 1);
            
            strGO.GetComponent<Image>().color = starColors[i % starColors.Length];

            strGO.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            strGO.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);
        }
    }
}