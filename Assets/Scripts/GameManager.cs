using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform m_tWallInitPoint; //建立城牆的起始位子
    [SerializeField] private float m_fWallInterval = 2.5f; //城牆的間隔
    [SerializeField] private List<int> m_iListWallPos = new List<int> { 0, 2, 4, 6 }; //城牆與原點相離多少間隔
    [SerializeField] private Transform m_tEnemyInitPoint; //生成敵人的起始位子
    [SerializeField] private Vector2 m_v2EnemyInterval = new Vector2(1, 1); //敵人的間隔
    [SerializeField] private int m_iLengthX = 5;
    [SerializeField] private int m_iLengthY = 5;
    [SerializeField] private Collider2D m_col2DLeftBorder; //畫面左邊邊界
    [SerializeField] private Collider2D m_col2DRightBorder; //畫面左邊邊界
    private List<EnemyUnit> m_listEnemyUnit = new List<EnemyUnit>();
    private Vector2 v2EnemyMovements = Vector2.left; //需要修改

    // Use this for initialization
    void Start()
    {
        foreach (int iPos in m_iListWallPos)
        {
            GameObject wallPrefab = UnitPool.Instance.GetWallPrefab();
            wallPrefab.transform.SetParent(m_tWallInitPoint);
            wallPrefab.transform.localPosition = new Vector2((iPos * m_fWallInterval), 0);
        }
        for (int i = 0; i < m_iLengthY; i++)
        {
            for(int j = 0; j < m_iLengthX; j++)
            {
                GameObject enemyPrefab = UnitPool.Instance.GetEnemyPrefab(0);
                m_listEnemyUnit.Add(enemyPrefab.GetComponent<EnemyUnit>());
                enemyPrefab.transform.SetParent(m_tEnemyInitPoint);
                Vector2 enemyPos = Vector2.zero;
                enemyPos.x += m_v2EnemyInterval.x * j;
                enemyPos.y -= m_v2EnemyInterval.y * i;
                enemyPrefab.transform.localPosition = enemyPos;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (EnemyUnit enemy in m_listEnemyUnit)
        {
            if(m_col2DLeftBorder.IsTouching(enemy.GetComponent<Collider2D>() ) ) //耗能，須更改
            {
                v2EnemyMovements = Vector2.right;
            }
            else if (m_col2DRightBorder.IsTouching(enemy.GetComponent<Collider2D>())) //耗能，須更改
            {
                v2EnemyMovements = Vector2.left;
            }
        }
        foreach (EnemyUnit enemy in m_listEnemyUnit)
        {
            enemy.Move(v2EnemyMovements);
        }
    }
}
