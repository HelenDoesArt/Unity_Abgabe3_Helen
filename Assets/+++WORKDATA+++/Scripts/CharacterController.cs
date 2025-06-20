using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpforce = 8f;
    //jumpforce and speed are both accessible in the unity editor through a serialized field
    //jumpforce determines the height/ force used by the character to jump
    private float direction = 0f;
    //the direction is first set to zero
    private Rigidbody2D rb2d;

    [Header("GroundCheck")]
    [SerializeField] private Transform transformgroundCheck;
    [SerializeField] private LayerMask layerGround;     
    
    [Header ("Manager")]
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private UIManager uiManager;
    
    private bool canMove = true; 
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            direction = 0f;
                 
            if (Keyboard.current.aKey.isPressed)
            {
                direction = -1;
            }
                 
            if (Keyboard.current.dKey.isPressed)
            {
                direction = 1;
            }
         
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }
                 
            rb2d.linearVelocity = new Vector2(direction * speed, rb2d.linearVelocity.y);
                 
        }
        
    }

    void Jump()
    {
        if (Physics2D.OverlapCircle(transformgroundCheck.position, 0.3f, layerGround))
        {
            rb2d.linearVelocity = new Vector2(x:0,y:jumpforce);
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(message:"Wir sind mit etwas kollidiert!");

        if (other.CompareTag("coin"))
        {
            Debug.Log(message:"Es war eine Münze");
            Destroy(other.gameObject);
            coinManager.AddCoin();
        }

        else if (other.CompareTag("obstacle"))
        {
            Debug.Log(message: "Es war ein obstacle");
            uiManager.ShowGameOverPanel();
            rb2d.linearVelocity = Vector2.zero;
            canMove = false;
            
        }
        
        else if (other.CompareTag("win"))
        {
            Debug.Log(message:"player wins");
            uiManager.ShowWinPanel();
            canMove = false;
        }
    }
    
}

