using UnityEngine; 

public class CoinManager : MonoBehaviour 
{
    [SerializeField] private int scoreAmount = 0; 
    [SerializeField] private UiManager uiManager; 

    private void Start() 
    {
        scoreAmount = 0; // Resets the score to 0 at the start
        uiManager.UpdateCoinText(scoreAmount); // Updates the score text in the UI
    }
    
    public void AddCoin() //when coin is collected
    {
        scoreAmount++; // Increase the score by 1
        uiManager.UpdateCoinText(scoreAmount); // Update UI to the new score
    }

    public void AddDiamond() // when diamond is collected
    {
        scoreAmount += 2; // Increase the score by 2
        uiManager.UpdateCoinText(scoreAmount); // Update UI to the new score
    }
}