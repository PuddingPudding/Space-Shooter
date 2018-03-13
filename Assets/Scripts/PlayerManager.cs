using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform m_tPlayerSpawnPoint; //生成玩家的位子
    [SerializeField] private Sprite m_spritePlayerFace;
    [SerializeField] private int m_iMaxBulletNum = 3; //場上玩家的最大子彈數
    [SerializeField] private float m_fShootingInterval = 0.5f; //射擊間隔
    [SerializeField] private float m_fReviveTime = 3; //重生時間
    [SerializeField] private Vector2 m_v2BulletOffset = new Vector2(0, 0.4f);
    [SerializeField] private int m_iMaxLife = 3;
    private Unit m_player;
    private int m_iCurrentLife = 3;
    private List<BulletScript> m_playerBullets = new List<BulletScript>();
    private float m_fShootingIntervalCounter = 0;
    private float m_fReviveTimeCounter = 0; //重生時間
    private bool m_bReviving = false; //是否正在重生

    // Use this for initialization
    void Start()
    {
        RevivePlayer();
        m_iCurrentLife = m_iMaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v2FinalSpeed = Vector2.zero;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            v2FinalSpeed += Vector2.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            v2FinalSpeed += Vector2.left;
        }
        m_player.Move(v2FinalSpeed);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            m_player.Hit(1);
        }

        for (int i = 0; i < m_playerBullets.Count; i++)
        {
            if (!m_playerBullets[i].isActiveAndEnabled) //若子彈已經消失
            {
                m_playerBullets.RemoveAt(i);
            }
        }

        if (!m_player.isActiveAndEnabled) //若死亡了
        {
            if (!m_bReviving)
            {
                m_iCurrentLife--;
                m_bReviving = true;
            }
            if (m_iCurrentLife > 0 && m_fReviveTimeCounter < m_fReviveTime && this.m_bReviving)
            {
                m_fReviveTimeCounter += Time.deltaTime;
                if (m_fReviveTimeCounter >= m_fReviveTime)
                {
                    RevivePlayer();
                    m_fReviveTimeCounter = 0;
                }
            }
        }
        m_fShootingIntervalCounter += Time.deltaTime;
    }

    public int Life
    {
        get { return this.m_iCurrentLife; }
    }

    private void RevivePlayer()
    {
        GameObject gObjPlayer = UnitPool.Instance.GetPrefab(UnitPool.EPrefabType.Player);
        m_player = gObjPlayer.GetComponent<PlayerUnit>();
        m_player.transform.SetParent(m_tPlayerSpawnPoint);
        m_player.transform.localPosition = Vector2.zero;
        m_bReviving = false;
    }

    public void Shoot()
    {
        if (m_playerBullets.Count < m_iMaxBulletNum && m_fShootingIntervalCounter >= m_fShootingInterval && m_player.isActiveAndEnabled)
        {
            m_fShootingIntervalCounter = 0;
            GameObject bulletPrefab = BulletPool.Instance.GetBulletPrefab(m_player.Team);
            BulletScript bulletScript = bulletPrefab.GetComponent<BulletScript>();
            bulletScript.SetReuse(BulletPool.Instance.BackToBulletPool); //再生出子彈後，接著告訴那顆子彈在消失後該歸還至何處
            bulletScript.InitAndShoot(this.transform.up, (Vector2)m_player.transform.position + m_v2BulletOffset, Unit.ETeam.Player);
            m_playerBullets.Add(bulletScript);
        }
    }
}
