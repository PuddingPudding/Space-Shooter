using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPool : MonoBehaviour
{
    public enum EPrefabType
    {
        Player,
        Enemy,
        Wall,
        Bullet,
        Explode
    }

    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private GameObject m_wallPrefab;
    [SerializeField] private GameObject m_explodePrefab;
    [SerializeField] private GameObject m_enemyPrefab;
    [SerializeField] private List<GameObject> m_listPlayerPrefab;
    [SerializeField] private List<GameObject> m_listWallPrefab;
    [SerializeField] private List<GameObject> m_listExplodePrefab;
    [SerializeField] private List<GameObject> m_listEnemyPrefab;
    private static UnitPool g_instance; //唯一存在

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

    public GameObject GetPrefab(EPrefabType _ePrefabType)
    {
        GameObject gameObj = null;
        switch(_ePrefabType)
        {
            case EPrefabType.Player:
                if(m_listPlayerPrefab.Count > 0)
                {
                    gameObj = m_listPlayerPrefab[0];
                    m_listPlayerPrefab.RemoveAt(0);
                }
                else
                {
                    gameObj = Instantiate(m_playerPrefab);
                }
                PlayerUnit playerTemp = gameObj.GetComponent<PlayerUnit>();
                playerTemp.Init(ImgBase.Instance.PlayerFaces[0], BackToPlayerPool, Unit.ETeam.Player);
                ExplodeScript explodeTemp = GetPrefab(EPrefabType.Explode).GetComponent<ExplodeScript>();
                playerTemp.SetExplode(explodeTemp);
                break;
            case EPrefabType.Explode:
                gameObj = this.GetExplodePrefab();
                break;
        }
        gameObj.SetActive(true);
        return gameObj;
    }

    public void BackToPlayerPool(GameObject _playerPrefab)
    {
        m_listPlayerPrefab.Add(_playerPrefab);
        _playerPrefab.SetActive(false);
    }

    public void BackToWallPool(GameObject _wallPrefab)
    {
        m_listWallPrefab.Add(_wallPrefab);
        _wallPrefab.SetActive(false);
    }

    public GameObject GetExplodePrefab()
    {
        GameObject explodePrefab = null;
        if (m_listExplodePrefab.Count > 0)
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

    public void BackToEnemyPool(GameObject _enemyPrefab) //目前每一個都分開寫，由於功能基本上一樣的關係，日後應該會考慮整理成一個函式
    {
        m_listEnemyPrefab.Add(_enemyPrefab);
        _enemyPrefab.SetActive(false);
    }
}
