using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject m_gameObjBulletPrefab; //每個子彈的遊戲物件原型
    [SerializeField] private List<GameObject> m_listBulletPrefab;
    private static BulletPool g_instance;

    private BulletPool() //先將建構子宣告私有化，避免人家亂生孩子
    {
        m_listBulletPrefab = new List<GameObject>();
    }

    public static BulletPool Instance //用getter讓其他人直接取得子彈池的唯一存在
    {
        get
        {
            return g_instance;
        }
    }

    private void Awake()
    {
        g_instance = this;
        DontDestroyOnLoad(this); //不要讓該物件在之後被刪掉，因為子彈池到哪都用得上
    }

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
    public GameObject GetBulletPrefab(Unit.ETeam _eTeam)
    {
        GameObject gameObjBullet = GetBulletPrefab();
        gameObjBullet.GetComponent<BulletScript>().Team = _eTeam;
        return gameObjBullet;
    }
    public void BackToBulletPool(GameObject _gameObjBullet)
    {
        m_listBulletPrefab.Add(_gameObjBullet);
        _gameObjBullet.SetActive(false);
    }
}