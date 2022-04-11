using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteCanvas;
    [SerializeField] private GameObject messageUI;
    private bool _isLeverArmActivated = false; // переменная активирован ли рычаг
    public void FinsishLevel() // функция закончить уровень
    {
        if (_isLeverArmActivated)
        {
            gameObject.SetActive(false);
            levelCompleteCanvas.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            messageUI.SetActive(true);
        }
    }
    public void Activate() { // переменная активации
        _isLeverArmActivated = true; // активация тру
        messageUI.SetActive(false);
    }

}
