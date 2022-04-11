using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverArm : MonoBehaviour
{
    private Finish _finish; // переменная объект класса финиш
    private Animator _animator;
    private void Start()
    {
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>(); // присваиваем скрипт
        _animator = transform.GetComponentInChildren<Animator>();
    }
    public void ActivatedLeverArm(){ // функция активации рычага
        _finish.Activate(); // вызвать функцию
        _animator.SetTrigger("activate");
    }
 
}
