using System.Collections; 
using TMPro; 
using UnityEngine; 

public class TimerScript : MonoBehaviour 
{
    [SerializeField] private float targetTime = 90.0f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI CountDown;

    [Header("Manager")]
    [SerializeField] private UiManager uiManager;
    [SerializeField] private GameObject ingameUI;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private Rigidbody2D playerRb;
    
    private bool timerActive = false;
    

    void Start()
    {
        if (playerRb == null)
            playerRb.simulated = false;
        
        if (characterController == null)
            characterController.canMove = false;
        
        StartCoroutine(countDown(3));
    }

    IEnumerator countDown(int count)
    {
        CountDown.gameObject.SetActive(true);

        for (int i = count; i > 0; i--)
        {
            CountDown.text = i.ToString(); 
            yield return new WaitForSeconds(1);
        }

        CountDown.text = "GO!";
        yield return new WaitForSeconds(1);
        
        CountDown.gameObject.SetActive(false);
        
        if(playerRb != null)
            playerRb.simulated = true;
        
        if (characterController != null)
            characterController.canMove = true;

        StartGame();

    }

    public void StartGame()
    {
        characterController.canMove = true;
        timerActive = true;
        timerText.gameObject.SetActive(true);
    }

    void Update()
    {
        
        if (timerActive)
        {
            targetTime -= Time.deltaTime;
            timerText.text = targetTime.ToString("0.00");

            if (targetTime < 0.1f)
            {
                timerStop(); 
            }
        }
    }

    public void timerStop()
    {
        timerText.gameObject.SetActive(false);
        timerActive = false;
        uiManager.ShowGameOverPanel();
    }
    
    public void BeginCountDown() 
    { 
        StartCoroutine(countDown(3));
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("win")) 
        {
            Debug.Log(message: "player wins"); 
            Destroy(other.gameObject);
            uiManager.ShowWinPanel();
            timerActive = false;
            if (playerRb != null)
                playerRb.linearVelocity = Vector2.zero;
        }
    }

    public void ResetTimer()
    {
        StopAllCoroutines();
        targetTime = 90.0f;
        timerText.text = targetTime.ToString("0.00");

        timerText.gameObject.SetActive(true);
        CountDown.gameObject.SetActive(true);
        
        if (playerRb != null)
            playerRb.simulated = false;
        
        if (characterController != null)
            characterController.canMove = false;
        
        StartCoroutine(countDown(3));
    }
}
