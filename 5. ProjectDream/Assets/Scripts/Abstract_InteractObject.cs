
using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Абстрактный класс для интерактивных предметов
/// </summary>
public abstract class Abstract_InteractObject : MonoBehaviour
{
    public string itemName;
    public SO_Inventory _playerInventory;
    public PlayerDialog playerDialog;
    public GameObject player;

}
