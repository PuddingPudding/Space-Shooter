using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomCollision
{
    public static bool CollisionDetection(ICollidableBox _colA , ICollidableBox _colB)
    {
        bool bCollided = false;
        if(_colA.ColLeft < _colB.ColRight && _colA.ColRight > _colB.ColLeft  && _colA.ColTop > _colB.ColBotton && _colA.ColBotton < _colB.ColTop)
        {
            bCollided = true;
        }
        return bCollided;
    }
}
