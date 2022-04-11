﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRoom : MonoBehaviour
{
    static public float ROOM_W = 16; // а
    static public float ROOM_H = 11;
    static public float WALL_T = 2;

    static public int MAX_RM_X = 9; 
    static public int MAX_RM_Y = 9;

    [Header ("Назначьте в инспекторе")]
    public bool keepInRoom = true;
    public float gridMult = 1;

    static public Vector2[] DOORS = new Vector2[] {
    new Vector2(14, 5),
    new Vector2(7.5f, 9),
    new Vector2(1, 5),
    new Vector2(7.5f, 1)
    };    


    // Местоположение этого персонажа в локальных координатах комнаты
    public Vector2 roomPos
    { // b
        get
        {
            Vector2 tPos = transform.position;
            tPos.x %= ROOM_W;
            tPos.y %= ROOM_H;
            return tPos;
        }
        set  
        {
            Vector2 rm = roomNum;
            rm.x *= ROOM_W;
            rm.y *= ROOM_H;
            rm += value;
            transform.position = rm;
        }
    }
    // В какой комнате находится этот персонаж?
    public Vector2 roomNum
    { //с
        get
        {
            Vector2 tPos = transform.position;
            tPos.x = Mathf.Floor(tPos.x / ROOM_W);
            tPos.y = Mathf.Floor(tPos.y / ROOM_H);
            return tPos;
        }
        set
        {
            Vector2 rPos = roomPos;
            Vector2 rm = value;
            rm.x *= ROOM_W;
            rm.y *= ROOM_H;
            transform.position = rm + rPos;
        }
    }

    void LateUpdate()
    {
        if (keepInRoom)
        {
            Vector2 rPos = roomPos;
            rPos.x = Mathf.Clamp(rPos.x, WALL_T, ROOM_W - 1 - WALL_T);
            rPos.y = Mathf.Clamp(rPos.y, WALL_T, ROOM_W - 1 - WALL_T);
            roomPos = rPos;
        }
    }

    // Вычисляет координаты узла сетки, ближайшего к данному персонажу
    public Vector2 GetRoomPosOnGrid(float mult = -1)
    {
        if (mult == -1)
        {
            mult = gridMult;
        }
        Vector2 rPos = roomPos;
        rPos /= mult;
        rPos.x = Mathf.Round(rPos.x);
        rPos.y = Mathf.Round(rPos.y);
        rPos *= mult;
        return rPos;
    }

}