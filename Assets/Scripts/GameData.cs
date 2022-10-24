[System.Serializable]
public class GameData
{
    public int saveScore;
    public int level1Score;
    public int level2Score;
    public int level3Score;
    public int level4Score;
    public int level5Score;
    public LevelState level0State;
    public LevelState level1State;
    public LevelState level2State;
    public LevelState level3State;
    public LevelState level4State;
    public LevelState level5State;
    
    
    public enum LevelState
    {
        Locked, 
        Unlocked, 
        Complite
    }

    public LevelState levelState;

    public GameData()
    {
        this.saveScore = 0;
        this.level1Score = 0;
        this.level2Score = 0;
        this.level3Score = 0;
        this.level4Score = 0;
        this.level5Score = 0;
        this.level0State = levelState;
        this.level1State = levelState;
        this.level2State = levelState;
        this.level3State = levelState;
        this.level4State = levelState;
        this.level5State = levelState;
    }
}