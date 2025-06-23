using System.Collections; 
using TMPro; 
using UnityEngine;
using UnityEngine.InputSystem; // Enables Keyboard Input System 

public class CharacterController : MonoBehaviour 
{
    [SerializeField] private float speed = 4f; // Movement speed (editable from the inspector)
    [SerializeField] private float jumpforce = 8f; // Jumping power/force (editable in inspector)

    private float direction = 0f; // horizontal movement 

    private Rigidbody2D rb2d;
    private Animator animator;// Rigidbody2D component for physics

    [Header("GroundCheck")]
    [SerializeField] private Transform transformgroundCheck; //to detect if the player is on the ground
    [SerializeField] private LayerMask layerGround; // Layer that defines what counts as "ground"

    [Header("Manager")] 
    [SerializeField] private CoinManager coinManager; // Reference to my coin manager script
    [SerializeField] private UiManager uiManager; // Reference to my UI manager script
    [SerializeField] private TimerScript timerScript;

    public bool canMove = true; // default movement is enabled

    void Start() 
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>(); 
        canMove = true; // movement is enabled when starting game
    }

    void Update() // Called once per frame
    {

        if (canMove) // Only process input if movement is enabled
        {
            direction = 0f; // Reset movement direction each frame

            if (Keyboard.current.aKey.isPressed) // If 'A' key is pressed
            {
                direction = -1; // Move left
            }

            if (Keyboard.current.dKey.isPressed) // If 'D' key is pressed
            {
                direction = 1; // Move right
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame) // If space was just pressed this frame
            {
                Jump(); 
            }
            
            rb2d.linearVelocity = new Vector2(direction * speed, rb2d.linearVelocity.y); // horizontal movement + vertical velocity = movement
            animator.SetFloat("speed", Mathf.Abs(direction));
        }
        else
        {
            rb2d.linearVelocity = new Vector2(0f, rb2d.linearVelocity.y);
            animator.SetFloat("speed", 0);
        }
    }

    void Jump() // Method to make the player jump
    {
        if (Physics2D.OverlapCircle(transformgroundCheck.position, 0.3f, layerGround)) // If touching ground layer
        {
            rb2d.linearVelocity = new Vector2(x: 0, y: jumpforce); // use speed + direction to jump
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //when player collides with something
    {
        if (other.CompareTag("coin")) // If colliding with coin
        {
            Debug.Log(message: "player picked up coin!");
            Destroy(other.gameObject); // Remove the coin object once player collided with it
            coinManager.AddCoin(); // Tell coin manager to increase score
            canMove = true; // Ensure movement is still allowed
        }
        else if (other.CompareTag("diamond")) // If colliding with a diamond
        {
            Debug.Log(message: "player picked up diamond!");
            Destroy(other.gameObject); // Remove the diamond object when player collided with it
            coinManager.AddDiamond(); // Tell coin manager to increase score
            canMove = true; // Ensure movement is still allowed
        }
        else if (other.CompareTag("obstacle")) // If hitting an obstacle
        {
            Debug.Log(message: "player collided with an obstacle"); 
            uiManager.ShowGameOverPanel(); // Show the Game Over UI panel
            rb2d.linearVelocity = Vector2.zero; // Stop all movement
        }
        else if (other.CompareTag("win")) // If reaching the win condition
        {
            Debug.Log(message: "player wins"); 
            uiManager.ShowWinPanel(); // Show the Win UI panel
            canMove = false; // Freeze player movement
            
            rb2d.linearVelocity = Vector2.zero; // Stop motion
        }
    }
}
