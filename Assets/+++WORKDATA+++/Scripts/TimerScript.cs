using System.Collections; 
using TMPro; 
using UnityEngine; 

public class TimerScript : MonoBehaviour 
{
    [SerializeField] private float targetTime = 90.0f; // Total time for countdown
    [SerializeField] private TextMeshProUGUI timerText; // Reference to UI text that shows remaining time

    [Header("Manager")]
    [SerializeField] private UiManager uiManager; // Reference to the UI manager to show panels

    [SerializeField] private TextMeshProUGUI CountDown; // Reference to countdown number UI

    private Rigidbody2D rb2d; //for player movement

    private bool canMove = true; 
    private bool timerActive = false; // timer is not running

    public void BeginCountDown() 
    {
        StartCoroutine(countDown(3)); // Begin coroutine with a 3-second countdown
    }

    void Start() 
    {
        
    }

    IEnumerator countDown(int seconds) // Coroutine that counts down before starting the timer
    {
        timerText.gameObject.SetActive(true); 
        CountDown.gameObject.SetActive(true); 
        canMove = false; // Disable player movement during countdown
        int count = seconds; // Set local countdown timer

        while (count > 0) // Loop until countdown reaches 0
        {
            CountDown.text = count.ToString(); // Update countdown text
            yield return new WaitForSeconds(1); // Wait 1 second
            count--; // Decrease the count
        }

        StartGame(); // Start actual gameplay
    }

    public void StartGame() // Called after countdown finishes
    {
        CountDown.gameObject.SetActive(true); 
        BeginCountDown();
        canMove = true; // Allow movement again
        Destroy(CountDown.gameObject); // Remove countdown number from UI
        timerActive = true; // Start main timer
        timerText.gameObject.SetActive(true); // Ensure the timer UI is visible
    }

    void Update() // Called once per frame
    {
        if (timerActive) // Only update timer if it’s running
        {
            targetTime -= Time.deltaTime; // Subtract time passed since last frame
            timerText.text = targetTime.ToString("0.00"); // Update UI

            if (targetTime < 0.1f) // If timer runs out
            {
                timerStop(); 
            }
        }
    }

    public void timerStop() // Called when the timer ends
    {
        timerText.gameObject.SetActive(false); // Hide timer text
        timerActive = false; // Stop timer updates
        uiManager.ShowGameOverPanel(); // Show the Game Over panel
        Destroy(timerText.gameObject); // Destroy the timer UI element
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("win")) 
        {
            Debug.Log(message: "player wins"); 
            Destroy(other.gameObject); // Remove the win trigger
            uiManager.ShowWinPanel(); // Show win panel
            canMove = false; // Prevent further player movement
            timerActive = false; // Stop the timer
            rb2d.linearVelocity = Vector2.zero; // Stop player movement
        }
    }

    public void ResetTimer() // Called to restart the timer (e.g. on level reset)
    {
        StopAllCoroutines(); // Stop any active coroutines (including countdowns)
        targetTime = 90.0f; // Reset timer value
        timerText.text = targetTime.ToString("0.00"); // Update UI to reflect reset value

        timerText.gameObject.SetActive(true); // Ensure timer UI is active again
        CountDown.gameObject.SetActive(true); // Show countdown UI
        StartCoroutine(countDown(3)); // Restart pre-game countdown
    }
}
