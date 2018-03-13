using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgBase : MonoBehaviour
{
    [SerializeField] private List<Sprite> m_playerFaces;
    [SerializeField] private List<Sprite> m_enemyFaces;
    [SerializeField] private Sprite m_normalWall;
    [SerializeField] private Sprite m_damagedWall;
    [SerializeField] private Sprite m_bullet;
    private static ImgBase g_Instance;

    private ImgBase() { } //先將建構子宣告私有化，避免亂生孩子

    private void Awake()
    {
        g_Instance = this;
        DontDestroyOnLoad(this); //不要讓該物件在之後被刪掉，因為圖像池到哪都用得上
    }

    public static ImgBase Instance
    {
        get
        {
            return g_Instance;
        }
    }

    public List<Sprite> PlayerFaces
    {
        get
        {
            return m_playerFaces;
        }
    }//玩家外觀的列表
    public List<Sprite> EnemyFaces
    {
        get
        {
            return m_enemyFaces;
        }
    }//敵人外觀的列表
    public Sprite NormalWall
    {
        get
        {
            return m_normalWall;
        }
    }//普通牆壁
    public Sprite DamagedWall
    {
        get
        {
            return  m_damagedWall;
        }
    }//受損牆壁
    public Sprite Bullet
    {
        get
        {
            return m_bullet;
        }
    }//子彈外觀
}
