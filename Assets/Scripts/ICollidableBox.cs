using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollidableBox
{
    float ColLeft { get; }
    float ColRight { get; }
    float ColTop { get; }
    float ColBotton { get; }
}
