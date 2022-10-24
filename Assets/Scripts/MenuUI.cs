using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject panelMenu;
    [HideInInspector] public Transform buttonsHolder;
    public TextMeshProUGUI panelText;
    public GameObject returnButton;
    public GameObject retryButton;
    public GameObject exitButton;
    public TMP_FontAsset font;
    
    public void CreateMenuPanel()
    {
        panelMenu = gameObject;
        panelText = panelMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
        buttonsHolder = panelMenu.transform.GetChild(2);
        var state = GameManager.instance.gameState;
        switch (state)
        {
            case GameState.GameWin:
                GameManager.instance.SetGameState(GameState.GameWin);
                panelText.text = "You WIN!";
                returnButton = new GameObject
                    ("Next Level Button",typeof(Image), typeof(Button), typeof(LayoutElement));
                ChangeTransform(returnButton);
                returnButton.GetComponent<Button>().onClick.AddListener(delegate
                {
                    LevelManager.instance.DestroyGameObjectInLevel();
                    GameManager.instance.CreateLevel(LevelManager.instance.activeLevel + 1);
                    DestroyMenuPanel(); 
                });
                break;
            case GameState.GameOver:
                GameManager.instance.SetGameState(GameState.GameOver);
                panelText.text = "Game Over";
                break;
            case GameState.Gameplay:
                GameManager.instance.SetGameState(GameState.Menu);
                returnButton = new GameObject
                    ("Return Button",typeof(Image), typeof(Button), typeof(LayoutElement));
                ChangeTransform(returnButton);
                returnButton.GetComponent<Button>().onClick.AddListener(delegate { DestroyMenuPanel(); });
                break;
        }
        
        retryButton = new GameObject
            ("Retry Button",typeof(Image), typeof(Button), typeof(LayoutElement));
        ChangeTransform(retryButton);
        retryButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            LevelManager.instance.DestroyGameObjectInLevel();
            GameManager.instance.CreateLevel(LevelManager.instance.activeLevel);
            GameManager.instance.SetGameState(GameState.Gameplay);
            DestroyMenuPanel(); 
        });

        exitButton = new GameObject
            ("Exit Button",typeof(Image), typeof(Button), typeof(LayoutElement));
        ChangeTransform(exitButton);
        exitButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            GameManager.instance.CreateLevel(0);
            GameManager.instance.SetGameState(GameState.Menu);
            DestroyMenuPanel();
        });
    }

    private void ChangeTransform(GameObject button)
    {
        button.transform.SetParent(buttonsHolder);
        button.transform.localScale = Vector3.one;
        GameObject buttonText = new GameObject("Button Text", typeof(TextMeshProUGUI));
        buttonText.transform.SetParent(button.transform);
        buttonText.transform.localScale = Vector3.one;
        string s = button.name;
        string[] parts = s.Split(' ');
        buttonText.GetComponent<TextMeshProUGUI>().text = parts[0];
        buttonText.GetComponent<TextMeshProUGUI>().color = Color.black;
        buttonText.GetComponent<TextMeshProUGUI>().font = font;
        buttonText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
    }

    public void DestroyMenuPanel()
    {
        Destroy(panelMenu);
    }
}