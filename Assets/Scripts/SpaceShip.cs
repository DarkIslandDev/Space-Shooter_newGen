using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    public int maxLives = 3;
    public int lives;
    public Canvas canvas;
    public RectTransform spaceShip;
    private RectTransform gun;
    public Slider healthBar;
    [SerializeField] private RectTransform bullet;
    
    private void Awake()
    {
        canvas = UIManager.instance.canvas;
        lives = maxLives;
        spaceShip = transform.GetChild(1).GetComponent<RectTransform>();
        gun = spaceShip.GetChild(0).GetComponent<RectTransform>();
        healthBar = transform.GetChild(0).GetComponent<Slider>();
        StartCoroutine(Shooting(0.2f));
    }

    private void Update()
    {
        // if (lives > maxLives)
        // {
        //     lives = maxLives;
        // }
        
        healthBar.maxValue = maxLives;
        healthBar.value = lives;
    }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     spaceShip.anchoredPosition += eventData.delta / canvas.scaleFactor;
    // }

    public IEnumerator Shooting(float timer)
    {
        var blt = Instantiate(bullet, gun.transform.position, Quaternion.identity);
        blt.transform.SetParent(canvas.transform);
        blt.localScale = new Vector3(1,1,1);
        blt.transform.position = gun.position;
        
        yield return new WaitForSeconds(timer);
        
        StartCoroutine(Shooting(timer));
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     other = spaceShip.GetComponent<Collider2D>();
    //     
    //     if (other.CompareTag("Entity"))
    //     {
    //         lives--;
    //         if (lives == 0)
    //         {
    //             Destroy(spaceShip.gameObject);
    //             GameManager.instance.SetGameState(GameState.GameOver);
    //             UIManager.instance.MenuPanelUIOnClick();
    //         }
    //     }
    // }
}
