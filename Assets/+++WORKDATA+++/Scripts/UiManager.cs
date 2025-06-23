using System.Collections; 
using System.Threading; 
using TMPro; 
using UnityEngine; 
using UnityEngine.SceneManagement; // Allows loading and reloading scenes
using UnityEngine.UI; 

public class UiManager : MonoBehaviour 
{
    [SerializeField] private TMP_Text scoreText; 

    [SerializeField] private GameObject MainMenuPanel; 

    [SerializeField] private GameObject gameOverPanel; 

    [SerializeField] private GameObject WinPanel; 

    [SerializeField] private Button startGameButton; 

    [SerializeField] private Button buttonReloadLevel; 

    [SerializeField] private GameObject gameCanvas;

    [SerializeField] private GameObject ingameUI; // Canvas that shows gameplay UI

    private int scoreAmount; // current score value

    void MainMenu() 
    {
        MainMenuPanel.SetActive(true); // Show the main menu panel
        startGameButton.onClick.AddListener(StartGame); // Assign StartGame() to the start button click
        gameOverPanel.SetActive(false); // Hide the game over panel
        buttonReloadLevel.onClick.AddListener(ReloadLevel); // Assign ReloadLevel() to the reload button click
        WinPanel.SetActive(false); // Hide the win panel
        ingameUI.SetActive(false);
    }

    private void Start() 
    {
        MainMenu(); // Show main menu
        gameCanvas.SetActive(false); // Hide gameplay UI at start
    }

    void StartGame() // Called when start button is pressed
    {
        MainMenuPanel.SetActive(false); // Hide the main menu panel
        gameCanvas.SetActive(true); // Show the in-game UI
        ingameUI.SetActive(true);
        
    }

    void ReloadLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //SceneManager gets the currently open scene and reloads it
    }

    public void UpdateCoinText(int scoreAmount) // Updates the score text on screen
    {
        scoreText.text = scoreAmount.ToString(); // Converts score to string and displays it
    }

    public void ShowGameOverPanel() 
    {
        gameOverPanel.SetActive(true); // Enable the game over panel
        ingameUI.SetActive(false);
    }

    public void ShowWinPanel() 
    {
        WinPanel.SetActive(true); // Enable the win panel
        ingameUI.SetActive(false);
    }
}
