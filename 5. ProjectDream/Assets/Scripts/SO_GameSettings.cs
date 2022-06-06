using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameLanguage
{
    ru,
    eng
}

[CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptble Objects/GameSettings")]
public class SO_GameSettings : ScriptableObject
{
    public GameLanguage gameLanguage;
    public float wordsPerSeconds;
}




