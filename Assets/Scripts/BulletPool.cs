using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject m_gameObjBulletPrefab; //每個子彈的遊戲物件原型
    [SerializeField] private List<GameObject> m_listBulletPrefab;
    private static BulletPool m_instance;

    private BulletPool() //先將建構子宣告私有化，避免人家亂生孩子
    {
        m_listBulletPrefab = new List<GameObject>();
    }

    public static BulletPool Instance //用getter讓其他人直接取得子彈池的唯一存在
    {
        get
        {
            if (m_instance == null)
            {
                GameObject singleton = new GameObject();

                m_instance = singleton.AddComponent<BulletPool>();
                singleton.name = "[Singleton] " + typeof(BulletPool).ToString();

                DontDestroyOnLoad(singleton);

                Debug.Log("[Singleton] 有一個 " + typeof(BulletPool) +
                          " 類別的物件需要被生成，所以 '" + singleton +
                          "' 就被製造出來並扔到 DontDestroyOnLoad.");
            }
            else
            {
                Debug.Log("[Singleton] Using instance already created: " +
                          m_instance.gameObject.name);
            }
            return m_instance;
        }
    }

    private void Start()
    {
        m_instance = this;
    }

    //public static BulletPool g_uniqueInstance = new BulletPool();
    //[SerializeField] private GameObject m_gameObjBulletPrefab; //每個子彈的遊戲物件原型
    //[SerializeField] private List<GameObject> m_listBulletPrefab;

    //private BulletPool() //先將自己的建構子宣告私有化，避免別人亂生孩子
    //{
    //    m_listBulletPrefab = new List<GameObject>();
    //}
    //public BulletPool Instance
    //{
    //    get
    //    {
    //        if (g_uniqueInstance == null)
    //        {
    //            g_uniqueInstance = new BulletPool();
    //        }
    //        return g_uniqueInstance;
    //    }
    //}

    public GameObject GetBulletPrefab()
    {
        GameObject gameObjBullet;
        if (m_listBulletPrefab.Count > 0)
        {
            gameObjBullet = m_listBulletPrefab[m_listBulletPrefab.Count - 1];
            m_listBulletPrefab.RemoveAt(m_listBulletPrefab.Count - 1);
        }
        else
        {
            gameObjBullet = Instantiate(m_gameObjBulletPrefab);
        }
        gameObjBullet.SetActive(true);
        return gameObjBullet;
    }
    public void BackToBulletPool(GameObject _gameObjBullet)
    {
        m_listBulletPrefab.Add(_gameObjBullet);
        _gameObjBullet.SetActive(false);
    }
}