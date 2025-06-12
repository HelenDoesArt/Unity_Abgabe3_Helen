using UnityEngine;

public class CoinManager : MonoBehaviour
{
    
    
    [SerializeField] private int scoreAmount = 0;
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        scoreAmount = 0;
        uiManager.UpdateCoinText(scoreAmount);
    }
    
    public void AddCoin()
    {
        scoreAmount++;
        uiManager.UpdateCoinText(scoreAmount);
        if (scoreAmount >= 10)
        {
            uiManager.ShowWinPanel();
        }
    }
    
}