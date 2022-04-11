using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4f;
    public float endDisplayTime;
    public GameObject dialogBox;
    float timerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeSelf)
            if (Time.time > endDisplayTime)
                dialogBox.SetActive(false);
    }

    public void DisplayDialog()
    {
        dialogBox.SetActive(true);
        endDisplayTime = Time.time + displayTime;
    }
}
