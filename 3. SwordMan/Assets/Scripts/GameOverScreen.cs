using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestarnHandler()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        TimeRecovery();
    }
    public void ExitHandler()
    {
        SceneManager.LoadScene(0);
        TimeRecovery();
    }

    private void TimeRecovery()
    {
        Time.timeScale = 1f;
    }

}
