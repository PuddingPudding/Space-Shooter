using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrange : MonoBehaviour
{
    public static void SquareArrangement(List<ISurrounding> _isListArrange, int _iLength)
    {
        Arrange.RectArrangement(_isListArrange, _iLength, _iLength);
    }

    public static void RectArrangement(List<ISurrounding> _isListArrange, int _iLengthX , int _iLengthY)
    {
        for (int i = 0; i < _iLengthY; i++) //在建立完敵人列表後
        {
            for (int j = 0; j < _iLengthX; j++)
            {
                int iSetting = i * _iLengthX + j; //依照順序找出各別需要設定的目標，接著再去依照八個方位設定關係
                int iNeighbor; //用來記錄周遭八方位之座標

                iNeighbor = iSetting - 1;
                if (iNeighbor >= i * _iLengthX)
                {
                    _isListArrange[iSetting].SetNeighbor(_isListArrange[iNeighbor], EDirection.Left);
                }
                iNeighbor = iSetting + 1;
                if (iNeighbor < (i + 1) * _iLengthX)
                {
                    _isListArrange[iSetting].SetNeighbor(_isListArrange[iNeighbor], EDirection.Right);
                }
                iNeighbor = iSetting - _iLengthX;
                if (iNeighbor >= 0)
                {
                    _isListArrange[iSetting].SetNeighbor(_isListArrange[iNeighbor], EDirection.Up);
                }
                iNeighbor = iSetting + _iLengthX;
                if (iNeighbor < _iLengthX * _iLengthY)
                {
                    _isListArrange[iSetting].SetNeighbor(_isListArrange[iNeighbor], EDirection.Down);
                }
                iNeighbor = iSetting - _iLengthX - 1;
                if (iNeighbor >= (i - 1) * _iLengthX && iNeighbor >= 0)
                {
                    _isListArrange[iSetting].SetNeighbor(_isListArrange[iNeighbor], EDirection.TopLeft);
                }
                iNeighbor = iSetting + _iLengthX + 1;
                if (iNeighbor < (i + 1) * _iLengthX && iNeighbor < _iLengthX * _iLengthY)
                {
                    _isListArrange[iSetting].SetNeighbor(_isListArrange[iNeighbor], EDirection.BottomRight);
                }
                iNeighbor = iSetting - _iLengthX + 1;
                if (iNeighbor < i * _iLengthX && iNeighbor >= 0)
                {
                    _isListArrange[iSetting].SetNeighbor(_isListArrange[iNeighbor], EDirection.TopRight);
                }
                iNeighbor = iSetting + _iLengthX - 1;
                if (iNeighbor >= (i + 1) * _iLengthX && iNeighbor < _iLengthX * _iLengthY)
                {
                    _isListArrange[iSetting].SetNeighbor(_isListArrange[iNeighbor], EDirection.BottomLeft);
                }
            }
        }
    }
    
}
