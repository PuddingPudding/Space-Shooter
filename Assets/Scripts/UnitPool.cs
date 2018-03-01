using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPool : MonoBehaviour
{
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private GameObject m_wallPrefab;
    [SerializeField] private GameObject m_explodePrefab;
    [SerializeField] private List<GameObject> m_listPlayerPrefab;
    [SerializeField] private List<GameObject> m_listWallPrefab;
    [SerializeField] private List<GameObject> m_listExplodePrefab;
    private static UnitPool g_instance;

    private UnitPool() //先將建構子宣告私有化，避免人家亂生孩子
    {
    }

    public static UnitPool Instance //用getter讓其他人直接取得子彈池的唯一存在
    {
        get
        {
            return g_instance;
        }
    }

    private void Awake()
    {
        g_instance = this;
        m_listPlayerPrefab = new List<GameObject>();
        m_listWallPrefab = new List<GameObject>();
        m_listExplodePrefab = new List<GameObject>();
        DontDestroyOnLoad(this); //不要讓該物件在之後被刪掉，因為物件池到哪都用得上
    }

    public GameObject GetPlayerPrefab(int _iPlayerFace)
    {
        GameObject playerPrefab = null;
        if (m_listPlayerPrefab.Count > 0)
        {
            playerPrefab = m_listPlayerPrefab[0];
            m_listPlayerPrefab.RemoveAt(0);
        }
        else
        {
            playerPrefab = Instantiate(this.m_playerPrefab);
        }
        playerPrefab.GetComponent<PlayerUnit>().SetPlayer(_iPlayerFace, this.BackToPlayerPool);
        playerPrefab.SetActive(true);
        return playerPrefab;
    }
    public void BackToPlayerPool(GameObject _playerPrefab)
    {
        m_listPlayerPrefab.Add(_playerPrefab);
        _playerPrefab.SetActive(false);
    }

    public GameObject GetWallPrefab()
    {
        GameObject wallPrefab = null;
        if (m_listWallPrefab.Count > 0)
        {
            wallPrefab = m_listWallPrefab[0];
            m_listWallPrefab.RemoveAt(0);
        }
        else
        {
            wallPrefab = Instantiate(this.m_wallPrefab);
        }
        wallPrefab.GetComponent<WallUnit>().SetWall(BackToWallPool);
        wallPrefab.SetActive(true);
        return wallPrefab;
    }
    public void BackToWallPool(GameObject _wallPrefab)
    {
        m_listWallPrefab.Add(_wallPrefab);
        _wallPrefab.SetActive(false);
    }

    public GameObject GetExplodePrefab()
    {
        GameObject explodePrefab = null;
        if (m_listWallPrefab.Count > 0)
        {
            explodePrefab = m_listExplodePrefab[0];
            m_listExplodePrefab.RemoveAt(0);            
        }
        else
        {
            explodePrefab = Instantiate(this.m_explodePrefab);
        }
        explodePrefab.GetComponent<ExplodeScript>().SetReuse(this.BackToExplodePool);
        explodePrefab.SetActive(true);
        return explodePrefab;
    }
    public void BackToExplodePool(GameObject _explodePrefab)
    {
        m_listExplodePrefab.Add(_explodePrefab);
        _explodePrefab.SetActive(false);
    }
}
