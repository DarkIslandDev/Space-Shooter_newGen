using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelState
{ 
    Locked, 
    Unlocked, 
    Complite
}

[CreateAssetMenu(fileName = "Level_", menuName = "Level")]
public class LevelSO : ScriptableObject
{
    public Scene scene;
    public int levelID;
    public LevelState levelState;
    public int levelScore;
    public int numberOfWaves;
    public bool hasBoss;
    public GameObject entityGO;
}