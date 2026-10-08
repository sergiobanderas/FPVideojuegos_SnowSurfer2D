using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 1f; // Delay before reloading the scene
    [SerializeField] private ParticleSystem finishEffect; // Particle effect to play when the player crosses the finish line

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has crossed the finish line!");
            finishEffect.Play(); // Play the finish effect particles            
            
            Invoke(nameof(NextLevel), reloadDelay); // Reload the scene after the specified delay

        }
    }

    void NextLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        
        PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1); // Unlock the next level
        PlayerPrefs.Save();
        // Reload the current scene
        if (unlockedLevel > 4) // If the player has completed the last level, go back to the main menu
        {
            SceneManager.LoadScene("Menu"); //TODO: Go to Win Scene
        }
        else
        {
            SceneManager.LoadScene($"Level{unlockedLevel + 1}"); // Load the next level
        }
        
    }

}
