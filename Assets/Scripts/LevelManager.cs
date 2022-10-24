using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour, IDataPersistence
{
    #region Instance

    public static LevelManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"кажеца вижу больше одного {instance.name}, иди удаляй другие или чини");
        }

        instance = this;
    }

    #endregion

    public List<LevelSO> levels;
    public LevelSO currentLevel;

    public GameObject enemySpawnHolder;
    public GameObject floatingTextPrefab;
    
    private GameObject enemySpawnHolderClone;
    private GameObject playerGOClone; 
    public FloatingText floatingTextClone;

    public int activeLevel;
    private int countOfLevels;

    private UIManager uiManager;
    
    private void Start()
    {
        uiManager = UIManager.instance;
        countOfLevels = levels.Count;

        if (activeLevel == 0)
        {
            uiManager.EnableLadder();
        }
        else
        {
            uiManager.DisableLadder();
        }
        
        if (levels[activeLevel].levelState == LevelState.Complite && levels[activeLevel + 1].levelState != LevelState.Complite)
        {
            levels[activeLevel + 1].levelState = LevelState.Unlocked;
        }
    }

    public void SetLevel(int level)
    {
        if (floatingTextClone == null)
        {
            activeLevel = level;
        
            if (activeLevel == 0)
            {
                uiManager.EnableLadder();
                DestroyGameObjectInLevel();
            }
            else
            {
                currentLevel = levels[level];
                uiManager.DisableLadder();
                CreateEnemySpawner();
                CreatePlayer();
                GameManager.instance.SetGameState(GameState.Gameplay);
            }
        }
    }

    private void CreateEnemySpawner()
    {
        enemySpawnHolderClone = Instantiate(enemySpawnHolder);
        enemySpawnHolderClone.transform.SetParent(uiManager.canvas.transform);
        enemySpawnHolderClone.transform.localScale = Vector3.one;
        RectTransform rt = enemySpawnHolderClone.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = Vector2.zero;
    }

    private void CreatePlayer()
    {
        playerGOClone = Instantiate(GameManager.instance.playerSO.playerShipGO);
        playerGOClone.transform.SetParent(uiManager.canvas.transform);
        playerGOClone.transform.localScale = Vector3.one;
    }

    public void DestroyGameObjectInLevel()
    {
        Destroy(enemySpawnHolderClone);
        Destroy(playerGOClone);
    }

    public void ShowFloatingText()
    {
        if (floatingTextClone == null)
        {
            GameObject ftc = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            floatingTextClone = ftc.GetComponent<FloatingText>();
        }
    }

    public void SetLevelState()
    {
        if (levels[activeLevel + 1].levelState is LevelState.Complite or LevelState.Unlocked)
        {
            currentLevel.levelState = LevelState.Complite;
            var b = levels[activeLevel + 1].levelState is LevelState.Complite or LevelState.Unlocked;
            
        }
        else
        {
            currentLevel.levelState = LevelState.Complite;
            levels[activeLevel + 1].levelState = LevelState.Unlocked;
        }
    }

    public void LoadData(GameData data)
    {
        this.levels[0].levelState = (LevelState)data.level0State;
        this.levels[1].levelState = (LevelState)data.level1State;
        this.levels[2].levelState = (LevelState)data.level2State;
        this.levels[3].levelState = (LevelState)data.level3State;
        this.levels[4].levelState = (LevelState)data.level4State;
        this.levels[5].levelState = (LevelState)data.level5State;
        
        this.levels[0].levelScore = data.saveScore;
        this.levels[1].levelScore = data.level1Score;
        this.levels[2].levelScore = data.level2Score;
        this.levels[3].levelScore = data.level3Score;
        this.levels[4].levelScore = data.level4Score;
        this.levels[5].levelScore = data.level5Score;
        
    }

    public void SaveData(GameData data)
    {
        data.level0State = (GameData.LevelState)LevelState.Complite;
        data.level1State = (GameData.LevelState)this.levels[1].levelState;
        data.level2State = (GameData.LevelState)this.levels[2].levelState;
        data.level3State = (GameData.LevelState)this.levels[3].levelState;
        data.level4State = (GameData.LevelState)this.levels[4].levelState;
        data.level5State = (GameData.LevelState)this.levels[5].levelState;

        data.saveScore = this.levels[0].levelScore;
        data.level1Score = this.levels[1].levelScore;
        data.level2Score = this.levels[2].levelScore;
        data.level3Score = this.levels[3].levelScore;
        data.level4Score = this.levels[4].levelScore;
        data.level5Score = this.levels[5].levelScore;
    }
}