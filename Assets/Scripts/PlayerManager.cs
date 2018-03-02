using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Unit m_player;
    [SerializeField] private Sprite m_spritePlayerFace;
    [SerializeField] private int m_iMaxBulletNum = 3; //場上玩家的最大子彈數
    [SerializeField] private float m_fShootingInterval = 0.5f; //射擊間隔
    [SerializeField] private Vector2 m_v2BulletOffset = new Vector2(0, 0.4f);
    private List<BulletScript> m_playerBullets = new List<BulletScript>();
    private float m_fShootingIntervalCounter = 0;

    // Use this for initialization
    void Start()
    {
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

        if(Input.GetKeyDown(KeyCode.Z) )
        {
            Shoot();
        }

        for(int i = 0; i<m_playerBullets.Count;i++)
        {
            if(!m_playerBullets[i].isActiveAndEnabled) //若子彈已經消失
            {
                m_playerBullets.RemoveAt(i);
            }
        }

        m_fShootingIntervalCounter += Time.deltaTime;
    }

    public void Shoot()
    {
        if(m_playerBullets.Count < m_iMaxBulletNum && m_fShootingIntervalCounter >= m_fShootingInterval)
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
