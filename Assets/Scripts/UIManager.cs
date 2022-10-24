using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Instance

    public static UIManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"кажеца вижу больше одного {instance.name}, иди удаляй другие или чини");
        }

        instance = this;
    }

    #endregion

    [HideInInspector] public Canvas canvas;
    private GameManager gameManager;

    public GameObject panelMenu;
    public GameObject mainMenuLvlLadder;
    public GameObject pauseButton;
    public TextMeshProUGUI scoreText;
    
    private void Start()
    {
        canvas = GetComponent<Canvas>();
        panelMenu.GetComponent<MenuUI>();
    }

    public void EnableLadder()
    {
        mainMenuLvlLadder.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void DisableLadder()
    {
        mainMenuLvlLadder.SetActive(false);
        pauseButton.SetActive(true);
    }

    private void Update()
    {
        SwitchScoreText();
    }

    public void SwitchScoreText()
    {
        switch (GameManager.instance.gameState)
        {
            case GameState.GameOver:
                scoreText.enabled = false; 
                break;
            case GameState.Gameplay:
                scoreText.enabled = true;
                scoreText.text = $"Score: {LevelManager.instance.currentLevel.levelScore}";
                break;
            case GameState.Menu:
                scoreText.enabled = true;
                scoreText.text = $"Score: {GameManager.instance.playerSO.score}";
                break;
            case GameState.GameWin:
                scoreText.enabled = true;
                scoreText.text = $"Score: {GameManager.instance.playerSO.score + LevelManager.instance.currentLevel.levelScore}";
                break;
        }
    }
    
    public void MenuPanelUIOnClick()
    {
        GameObject panel = Instantiate(panelMenu, transform.position, Quaternion.identity);
        panel.transform.SetParent(canvas.transform);
        panel.transform.localScale = Vector3.one;
        RectTransform rt = panel.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = Vector2.zero;
        panel.GetComponent<MenuUI>().CreateMenuPanel();
    }
}