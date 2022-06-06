using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Диалоги героя
/// </summary>
public class PlayerDialog : MonoBehaviour, Interface_Dialog
{
    private TextMeshProUGUI _sub;
    [SerializeField]
    public TextMeshProUGUI sub { get { return (_sub); } set { _sub = value; } }
    private SO_GameSettings _gameSettings;
    public SO_GameSettings gameSettings { get { return (_gameSettings); } set { _gameSettings = value; } }
    public SO_GameSettings gameSettingsSerialize;
    private void Start()
    {
        sub = this.transform.root.GetComponentInChildren<TextMeshProUGUI>();
        gameSettings = gameSettingsSerialize;
    }

    public IEnumerator ShowDialog(SO_InteractObject data) 
    {
        string dialog;
        while (data.inkStory.canContinue)
        {
            dialog = data.inkStory.Continue();
            sub.text = dialog;
            int countWords = dialog.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            yield return new WaitForSeconds(countWords * gameSettings.wordsPerSeconds);
            sub.text = null;
        }
    }
  
    public void StartShowDialog(SO_InteractObject data)
    {
        StartCoroutine(ShowDialog(data));
    }

}
