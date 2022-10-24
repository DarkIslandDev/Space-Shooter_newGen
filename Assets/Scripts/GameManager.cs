using System;
using UnityEngine;

public enum GameState
{
    Menu,
    Gameplay,
    GameOver,
    GameWin,
}
public class GameManager : MonoBehaviour
{
    #region Instance

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"кажеца вижу больше одного {instance.name}, иди удаляй другие или чини");
        }

        instance = this;

        gameState = GameState.Menu;
        UIManager.instance.scoreText.text = playerSO.score.ToString();
    }

    #endregion

    public GameState gameState;
    public PlayerSO playerSO;
    
    private void UpdateGameManagerState()
    {
        switch (gameState)
        {
            case GameState.Menu:
                Time.timeScale = 1;
                break;
            case GameState.Gameplay:
                
                Time.timeScale = 1;
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                break;
            case GameState.GameWin:
                Time.timeScale = 0;
                break;
        }
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
        UpdateGameManagerState();
        UIManager.instance.SwitchScoreText();
    }

    public void CreateLevel(int level)
    {
        if (LevelManager.instance.levels[level].levelState is LevelState.Unlocked or LevelState.Complite)
        {
            LevelManager.instance.SetLevel(level);
        }
        else
        {
            LevelManager.instance.ShowFloatingText();
        }
    }
}