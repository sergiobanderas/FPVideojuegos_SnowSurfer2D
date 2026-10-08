using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float bootsSpeed = 35f;
    [SerializeField] private ParticleSystem snowEffect;
    [SerializeField] private ScoreManager scoreManager;
    
    SurfaceEffector2D surfaceEffector2D;
    Rigidbody2D rb;

    float baseSpeed;    
    InputAction moveAction;
    Vector2 moveInput;
    float previousRotation; // Store the previous rotation of the player
    float totalRotation; // Store the total rotation of the player
    int flipCount; // Store the number of flips performed by the player

    int activePowerUpsCount; // Track the number of active power-ups

    
    private bool canControlPlayer = true; // Flag to control player input

    public bool CanControlPlayer { get => canControlPlayer; set => canControlPlayer = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
        baseSpeed = surfaceEffector2D.speed; // Store the initial speed of the SurfaceEffector2D
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!canControlPlayer) return; // If player input is disabled, exit the method
        
        PlayerTorque();     
        BoostPlayer();
        CalculateFlips();        
        
    }

    /// <summary>
    /// Calculates the number of flips the player has performed based on their rotation 
    /// </summary>
    private void CalculateFlips()
    {
        // Get the current rotation of the player in degrees
        float currentRotation = transform.rotation.eulerAngles.z; 
        // Calculate the change in rotation since the last frame
        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation); 
        
        if (Math.Abs(totalRotation)> 340)
        {
            flipCount++; // Increment the flip count if the total rotation exceeds 360 degrees
            
            scoreManager.AddScore(flipCount*100); 

            totalRotation = 0; // Reset the total rotation for the next flip            
        }

        previousRotation = currentRotation; // Update the previous rotation for the next frame
        
    }

    /// <summary>
    /// Applies torque to the player based on the horizontal input from the Move action.
    /// </summary>
    void PlayerTorque()
    {
        moveInput = moveAction.ReadValue<Vector2>(); 
        if (moveInput.x < 0)
        {
            rb.AddTorque(torqueAmount);
        }
        else if (moveInput.x > 0)
        {
            rb.AddTorque(-torqueAmount);
        }
        
    }

    void BoostPlayer()
    {
        //Increase the player's speed when the up arrow key is pressed
        //Surface Efector speed is increased to bootsSpeed
        if (moveInput.y > 0)
        {     
            surfaceEffector2D.speed = bootsSpeed; 
        }
        else
        {         
            surfaceEffector2D.speed = baseSpeed; // Return to normal speed
        }
    }

    // Detects when the player collides with the floor and plays the snow effect
    void OnCollisionEnter2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if (collision.gameObject.layer == layerIndex)
        {            
            snowEffect.Play();            
        }
    }

    // Detects when the player exits the collision with the floor and stops the snow effect
    void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if (collision.gameObject.layer == layerIndex)
        {            
            snowEffect.Stop();
        }
    }

    public void ApplyPowerUp(PowerUpScriptableObject powerUpData)
    {
        activePowerUpsCount++; // Increment the count of active power-ups
        if (powerUpData.PowerUpType == "Speed")
        {
            baseSpeed += powerUpData.PowerUpValue; // Increase the base speed by the power-up value
            bootsSpeed += powerUpData.PowerUpValue; // Increase the boots speed by the power-up value
        }
    }

    public void DeactivatePowerUp(PowerUpScriptableObject powerUpData)
    {
        activePowerUpsCount--; // Decrement the count of active power-ups
        if (activePowerUpsCount == 0)
        {
            if (powerUpData.PowerUpType == "Speed")
            {
                baseSpeed -= powerUpData.PowerUpValue; // Decrease the base speed by the power-up value
                bootsSpeed -= powerUpData.PowerUpValue; // Decrease the boots speed by the power-up value
            }
        }
    }
}
