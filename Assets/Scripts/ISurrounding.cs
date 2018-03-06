using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISurrounding
{
    ISurrounding GetNeighbor(EDirection _eDir);
    void SetNeighbor(ISurrounding _anotherSurrounding , EDirection _eDir);
    bool HasNeighbor(EDirection _eDir); //回傳某個方位是否有鄰居
}

public enum EDirection
{
    Left,
    Right,
    Up,
    Down,
    TopLeft,
    BottomRight,
    TopRight,
    BottomLeft
}