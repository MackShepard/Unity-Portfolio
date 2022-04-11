using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject poi;
    public GameObject[] panels;
    public float scrollspeed = -30f;
    public float motionMult = 0.25f;
    private float panelHit;
    private float depth;

    private void Start()
    {
        panelHit = panels[0].transform.localScale.y; // масштаб по y
        depth = panels[0].transform.position.z; // позиция z


        panels[0].transform.position = new Vector3(0,0,depth); // 1 панель по центру
        panels[1].transform.position = new Vector3(0,panelHit, depth);  // 2 панель над 1
    }
    void Update()
    {
        float tY, tX = 0;
        tY = Time.time * scrollspeed % panelHit; // таймер * шаг (в секунду) % предел (панель будет двигать со скорость шаг, до максимального предела)

        if (poi != null)
        {
            tX = -poi.transform.position.x * motionMult; // смещение в противоположную сторону от героя на 25%
        }

        panels[0].transform.position = new Vector3(tX, tY, depth); // 1 панель двигается от 0 до -160
       
        if (tY >= 0) // если панель 1 вернулась на изначальную позицию
        {
            panels[1].transform.position = new Vector3(tX, tY - panelHit, depth); // поставить 2 панель под 1
        }
        else
        {
            panels[1].transform.position = new Vector3(tX, tY + panelHit, depth); // иначе просто двигается под 1 панелью
        }
    }
}
    

