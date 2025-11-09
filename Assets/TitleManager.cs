using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management



public class TitleManager : MonoBehaviour
{

    public GameObject settings_panel;
    public GameObject controls_panel;

    // Call this function from your "Start Game" button
    public void StartGame()
    {
        // Replace "GameScene" with the name of your main game scene
        SceneManager.LoadScene("GameScene");
    }

    // Call this function from your "Exit Game" button
    public void ExitGame()
    {
        // This will quit the application
        Debug.Log("Quit Game"); // Useful for testing in the editor
        Application.Quit();
    }

    public void SettingsBTN()
    {
        Debug.Log("clicked settings button");
        settings_panel.SetActive(true);
    }
    public void ExitSettings()
    {
        Debug.Log("Clicked exit setting");
        settings_panel.SetActive(false);
    }
    public void ControlsBTN()
  {
        controls_panel.SetActive(true);
  }

    public void ExitControlsBTN()
  {
        controls_panel.SetActive(false);
  }
}
