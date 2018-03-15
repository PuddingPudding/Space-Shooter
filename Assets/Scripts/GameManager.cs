using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform m_tWallInitPoint; //建立城牆的起始位子
    [SerializeField] private float m_fWallInterval = 2.5f; //城牆的間隔
    [SerializeField] private List<int> m_iListWallPos = new List<int> { 0, 2, 4, 6 }; //城牆與原點相離多少間隔
    [SerializeField] private PlayerManager playerManager;
    private List<WallUnit> m_listWallUnit = new List<WallUnit>();

    //interface A { }
    //class B : A { List<ISurrounding> aaa = null; public void add(ISurrounding i) { } }
    //class C : A { ISurrounding[] aaa = null; public void add(ISurrounding i) { } }
    //class D
    //{
    //    Arrange.RectArrangement(new B(), m_iLengthX, m_iLengthY);
    //    Arrange.RectArrangement(new C(), m_iLengthX, m_iLengthY);
    //}
    // Use this for initialization
    void Start()
    {
        foreach (int iPos in m_iListWallPos)
        {
            GameObject wallPrefab = UnitPool.Instance.GetPrefab(UnitPool.EPrefabType.Wall);
            wallPrefab.transform.SetParent(m_tWallInitPoint);
            wallPrefab.transform.localPosition = new Vector2((iPos * m_fWallInterval), 0);
            m_listWallUnit.Add(wallPrefab.GetComponent<WallUnit>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        CollideEvent();
    }

    private void CollideEvent() //碰撞事件，掃描整個場內的物件
    {
        List<BulletUnit> listPlayerBulletUnit = playerManager.BulletList;
        foreach (WallUnit wall in m_listWallUnit)
        {
            foreach(BulletUnit bullet in listPlayerBulletUnit)
            {
                if(CustomCollision.CollisionDetection(wall , bullet) ) //如果有任何的子彈碰觸到了任何牆壁
                {
                    wall.Hit(bullet.Attack);
                    bullet.KillSelf();
                }
            }
        }
    }

    public void EnemyShoot(int _shootingNum)
    {
    }
}
