using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public abstract class Timer : MonoBehaviour
{
    UIManager uIManager;
    
    [SerializeField] private TMP_Text timerText;
    private float startTime;
    
    private static bool timerFinished = false;    
    
    public float t;
    
     void Start()
    {
        
    }

    float currCountdownValue;
     public float countdownValue = 10;
     
     public IEnumerator StartCountdown()
     {
         Debug.Log("Countdown started");
         
         //wait for 2 seconds
         yield return new WaitForSeconds(2f);
         
         Debug.Log("Waited for 2 seconds)");
         
         currCountdownValue = countdownValue;
         while (currCountdownValue > 0)
         {
             Debug.Log("Countdown: " + currCountdownValue);
             yield return new WaitForSeconds(1.0f);
             currCountdownValue--;
         }
     }
     
    // Update is called once per frame
    void Update()
    {
        if (timerFinished)
            return;
        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f1");
        
        timerText.text = minutes + ":" + seconds;

        if (t >= 0)
        {
            GameObject.Find("GameOverPanel").GetComponent<UIManager>();
        }
    }
    
}


