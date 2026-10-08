using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    /// <summary>
    /// Load the game scene Level1 when the Play button is clicked
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
        
    }

    /// <summary>
    /// Quit the application when the Quit button is clicked
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Load the main menu scene when the Menu button is clicked
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");        
    }

    /// <summary>
    /// Load the character selection scene when the Select Character button is clicked
    /// </summary>
    public void GoToSelectCharacter()
    {        
        SceneManager.LoadScene("SelectCharacter");
    }

    /// <summary>
    /// Load the credits scene when the Credits button is clicked
    /// </summary>
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    /// <summary>
    /// Load the level selection scene when the Select Level button is clicked
    /// </summary>
    public void GoToSelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    /// <summary>
    /// Load the specified level scene when the Select Level button is clicked
    /// </summary>
    /// <param name="level"></param>
    public void GoToLevel(int level)
    {
        SceneManager.LoadScene($"Level{level}");
    }


    /// <summary>
    /// Store the selected character index in PlayerPrefs
    /// </summary>
    /// <param name="characterIndex"></param>
    public void SelectCharacter(int characterIndex)
    {        
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("Menu");
    }


}
