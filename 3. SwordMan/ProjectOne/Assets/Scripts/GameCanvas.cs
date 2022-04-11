using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject pauseCanvas;
    public void PauseHandler()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
