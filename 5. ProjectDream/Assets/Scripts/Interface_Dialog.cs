using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// ��������� ��� ��������
/// </summary>
public interface Interface_Dialog 
{
    TextMeshProUGUI sub { get; set; }
    SO_GameSettings gameSettings { get; set; }

    IEnumerator ShowDialog(SO_InteractObject data);

    void StartShowDialog(SO_InteractObject data);
}
