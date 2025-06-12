using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private GameObject MainMenuPanel;
    
    [SerializeField] private GameObject gameOverPanel;
    
    [SerializeField] private GameObject WinPanel;
    
    [SerializeField] private Button startGameButton;

    [SerializeField] private Button buttonReloadLevel;

    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameCanvas;

    void MainMenu()
    {
        menuCanvas.SetActive(true);
        
        MainMenuPanel.SetActive(true);
        startGameButton.onClick.AddListener(StartGame);
        gameOverPanel.SetActive(false);
        buttonReloadLevel.onClick.AddListener(ReloadLevel);
        WinPanel.SetActive(false);
    }

    private void Start()
    {
        MainMenu();
        //menuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        
    }

    void StartGame()
    {
        menuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        
       // MainMenuPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        menuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        //SceneManager holt sich jetzt gerade offene Szene, die soll dann nochmal neu geladen werden 
    }

    public void UpdateCoinText(int newCoinCount)
    {
        scoreText.text = newCoinCount.ToString();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    
    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
    }
}