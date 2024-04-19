using UnityEngine;

public class SceneMusicController : MonoBehaviour
{
    void Start()
    {
        // Check if the Music singleton exists
        if (Music.singleton != null)
        {
            // Get the current scene's name
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            // Decide which music to play based on the scene's name
            switch (sceneName)
            {
                case "Level 1 - Cell":
                    Music.singleton.ChangeState(ActiveState.cell); // Change the state to combat or any appropriate state
                    break;
                case "Level 2 - Yard":
                    Music.singleton.ChangeState(ActiveState.combat); // Change the state to cell or any appropriate state
                    break;
                case "Level 3 - Outer Yard":
                    Music.singleton.ChangeState(ActiveState.adventure); // Change the state to adventure or any appropriate state
                    break;
                default:
                    // If the scene name doesn't match any expected cases, you can handle it accordingly
                    Debug.LogWarning("Scene name not recognized for music control.");
                    break;
            }
        }
        else
        {
            // If the Music singleton doesn't exist, log a warning
            Debug.LogWarning("Music singleton not found. Make sure the Music script is attached to a GameObject in the scene.");
        }
    }
}
