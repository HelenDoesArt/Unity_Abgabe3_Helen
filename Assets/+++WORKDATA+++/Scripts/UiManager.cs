using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Button buttonReloadLevel;
    private void Start()
    {
        gameOverPanel.SetActive(false);
        buttonReloadLevel.onClick.AddListener(ReloadLevel);
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
}