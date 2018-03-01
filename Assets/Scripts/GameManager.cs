using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform m_tWallInitPoint; //建立城牆的起始位子
    [SerializeField] private float m_fWallInterval = 2.5f; //城牆的間隔
    [SerializeField] private List<int> m_iListWallPos = new List<int> { 0, 2, 4, 6 }; //城牆與原點相離多少間隔

    // Use this for initialization
    void Start()
    {
        foreach (int iPos in m_iListWallPos)
        {
            GameObject wallPrefab = UnitPool.Instance.GetWallPrefab();
            wallPrefab.transform.SetParent(m_tWallInitPoint);
            wallPrefab.transform.localPosition = new Vector2((iPos * m_fWallInterval), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
