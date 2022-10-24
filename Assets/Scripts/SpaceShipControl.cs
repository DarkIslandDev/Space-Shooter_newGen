using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceShipControl : MonoBehaviour, IDragHandler
{
    private SpaceShip sp;

    private void Start()
    {
        sp = GetComponentInParent<SpaceShip>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        sp.spaceShip.anchoredPosition += eventData.delta / sp.canvas.scaleFactor;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
        {
            sp.lives--;
            if (sp.lives == 0)
            {
                Destroy(gameObject);
                GameManager.instance.SetGameState(GameState.GameOver);
                UIManager.instance.MenuPanelUIOnClick();
            }
        }
    }
}