using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class SceneTransition : MonoBehaviour
{
    public void LoadWinScene()
    {
        // Enable cursor before switching screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Load scene
        SceneManager.LoadScene("WIN");
    }

    public void LoadNextLevel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (SceneManager.GetActiveScene().name == "Bane 1")
        {
            SceneManager.LoadScene("Bane 2");
           
        }
        else
        {
            SceneManager.LoadScene("Bane 3");
        }
    }
    public void LoadMainLevel()
    {
        // No cursor while playing
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Load scene
        SceneManager.LoadScene("Bane 1");
    }

  public void LoadSecondLevel()
    {
        // No cursor while playing
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Load scene
        SceneManager.LoadScene("Bane 2");
    }

    public void LoadThirdLevel()
    {
        // No cursor while playing
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Load scene
        SceneManager.LoadScene("Bane 3");
    }
    public void LoadGameOverScene()
    {
        // Enable cursor before switching screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Load scene
        SceneManager.LoadScene("Gameover");
    }

    public void LoadMainMenu()
    {
        // Enable cursor before switching screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Load scene
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
