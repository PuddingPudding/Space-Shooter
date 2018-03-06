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
    [SerializeField] private float m_fEnemyShootingInterval = 2; //敵人的射擊間隔
    [SerializeField] private int m_iShooringEnemyNum = 2; //每次射擊的敵人數量
    private List<EnemyUnit> m_listEnemyUnit = new List<EnemyUnit>();
    private Vector2 v2EnemyMovements = Vector2.left; //需要修改
    private float m_fEnemyShootingCounter; //射擊間隔計數器

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

        for (int i = 0; i < m_iLengthY; i++) //在建立完敵人列表後
        {
            for (int j = 0; j < m_iLengthX; j++)
            {
                int iSetting = i * m_iLengthX + j; //依照順序找出各別需要設定的格子，接著再去依照八個方位設定關係
                int iNeighbor; //用來記錄周遭八方位之座標

                iNeighbor = iSetting - 1;
                if (iNeighbor >= i * m_iLengthX)
                {
                    m_listEnemyUnit[iSetting].SetNeighbor(m_listEnemyUnit[iNeighbor], EDirection.Left);
                }
                iNeighbor = iSetting + 1;
                if (iNeighbor < (i + 1) * m_iLengthX)
                {
                    m_listEnemyUnit[iSetting].SetNeighbor(m_listEnemyUnit[iNeighbor], EDirection.Right);
                }
                iNeighbor = iSetting - m_iLengthX;
                if (iNeighbor >= 0)
                {
                    m_listEnemyUnit[iSetting].SetNeighbor(m_listEnemyUnit[iNeighbor], EDirection.Up);
                }
                iNeighbor = iSetting + m_iLengthX;
                if (iNeighbor < m_iLengthX * m_iLengthY)
                {
                    m_listEnemyUnit[iSetting].SetNeighbor(m_listEnemyUnit[iNeighbor], EDirection.Down);
                }
                iNeighbor = iSetting - m_iLengthX - 1;
                if (iNeighbor >= (i - 1) * m_iLengthX && iNeighbor >= 0)
                {
                    m_listEnemyUnit[iSetting].SetNeighbor(m_listEnemyUnit[iNeighbor], EDirection.TopLeft);
                }
                iNeighbor = iSetting + m_iLengthX + 1;
                if (iNeighbor < (i + 1) * m_iLengthX && iNeighbor < m_iLengthX * m_iLengthY)
                {
                    m_listEnemyUnit[iSetting].SetNeighbor(m_listEnemyUnit[iNeighbor], EDirection.BottomRight);
                }
                iNeighbor = iSetting - m_iLengthX + 1;
                if (iNeighbor < i * m_iLengthX && iNeighbor >= 0)
                {
                    m_listEnemyUnit[iSetting].SetNeighbor(m_listEnemyUnit[iNeighbor], EDirection.TopRight);
                }
                iNeighbor = iSetting + m_iLengthX - 1;
                if (iNeighbor >= (i + 1) * m_iLengthX && iNeighbor < m_iLengthX * m_iLengthY)
                {
                    m_listEnemyUnit[iSetting].SetNeighbor(m_listEnemyUnit[iNeighbor], EDirection.BottomLeft);
                }
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
        m_fEnemyShootingCounter += Time.deltaTime;
        if(m_fEnemyShootingCounter >= m_fEnemyShootingInterval)
        {
            m_fEnemyShootingCounter -= m_fEnemyShootingInterval;
            EnemyShoot(m_iShooringEnemyNum);
        }
    }

    public void EnemyShoot(int _shootingNum)
    {
        List<Transform> shootableEnemy = new List<Transform>(); //將所有可以進行射擊敵人們找出來

        for(int i = 0; i < m_listEnemyUnit.Count; i++)
        {
            if(!m_listEnemyUnit[i].HasNeighborThorough(EDirection.Down) && m_listEnemyUnit[i].isActiveAndEnabled)//如果該敵人的下方完全沒有其他敵人，將他列入可射擊的敵人陣列
            {
                Debug.Log(i + "號可以射");
                shootableEnemy.Add(m_listEnemyUnit[i].transform);
            }
        }

        for(int i = 0; i < _shootingNum && shootableEnemy.Count > 0; i++)
        {
            int iRandom = Random.Range(0, shootableEnemy.Count);
            GameObject bulletPrefab = BulletPool.Instance.GetBulletPrefab(Unit.ETeam.Enemy);
            Vector2 v2BulletPosition = (Vector2)shootableEnemy[iRandom].position - new Vector2(0,0.5f);
            bulletPrefab.GetComponent<BulletScript>().SetReuse(BulletPool.Instance.BackToBulletPool);
            bulletPrefab.GetComponent<BulletScript>().InitAndShoot(Vector2.down, v2BulletPosition, Unit.ETeam.Enemy);
            shootableEnemy.RemoveAt(iRandom); //射完便將該敵人排除在可射擊名單外
        }
    }
}
