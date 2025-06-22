using UnityEngine; 

public class Enemy : MonoBehaviour 
{
    [SerializeField] private GameObject pointA; // First patrol point
    [SerializeField] private GameObject pointB; // Second patrol point

    public float speed; // Movement speed of the enemy
    private Rigidbody2D rb; // Reference to the enemy's Rigidbody2D component
    private Transform currentPoint; // Target point the enemy is currently moving toward

    void Start() // Called before the first frame update
    {
        rb = GetComponent<Rigidbody2D>(); // Gets the Rigidbody2D component attached to the enemy
        currentPoint = pointA.transform; // Starts movement toward pointA
    }

    void Update() // Called once per frame
    {
        Vector2 direction = (currentPoint.position - transform.position); // Calculates the direction vector to the target point

        if (currentPoint == pointB.transform) // Checks if the current target is pointB
        {
            rb.linearVelocity = new Vector2(speed, 0); 
        }
        else // If the current target is not pointB but A
        {
            rb.linearVelocity = new Vector2(speed, 0); //moves with set speed
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform) // If close to pointB
        {
            currentPoint = pointA.transform; // Switch direction toward pointA
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform) // If close to pointA
        {
            currentPoint = pointB.transform; // Switch direction toward pointB
        }
    }
}