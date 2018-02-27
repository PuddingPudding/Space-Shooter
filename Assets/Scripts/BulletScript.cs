using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public delegate void Reuse(GameObject go); //定義某個函式規範 (回傳值，傳入的參數等等)
    public Reuse m_reuse = null; //宣告一個該函式規範的變數
    [SerializeField] private float m_fSpeed = 7.5f;
    [SerializeField] private float m_fLifeTime = 4;
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private float m_fDamage = 1;
    private Unit.ETeam m_eTeam;
    private float m_fExistTime;
    
    public Unit.ETeam Team
    {
        get
        {
            return m_eTeam;
        }
        set
        {
            m_eTeam = value; //這邊的value就表示傳入值
        }
    }

    public void InitAndShoot(Vector2 _v2Dir , Vector2 _v2Pos , Unit.ETeam _eTeam)
    {
        m_eTeam = _eTeam;
        m_fExistTime = 0;
        this.transform.position = _v2Pos;
        this.transform.up = _v2Dir; //將自己的頭(編輯畫面下的向上箭頭)轉向到跟輸入的向量一樣的角度
        m_rigidbody2D.velocity = this.transform.up * m_fSpeed;
    }

    private void Update()
    {
        if(this.isActiveAndEnabled)
        {
            m_fExistTime += Time.deltaTime;
            if(m_fExistTime >= m_fLifeTime)
            {
                m_fExistTime = 0;
                //BulletPool.g_uniqueInstance.BackToBulletPool(this.gameObject);
                //BP.BackToBulletPool(this.gameObject);
                m_reuse.Invoke(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_fExistTime = 0;
        //BulletPool.g_uniqueInstance.BackToBulletPool(this.gameObject);
        //BP.BackToBulletPool(this.gameObject);
        collision.gameObject.SendMessage("Hit", m_fDamage , SendMessageOptions.DontRequireReceiver);
        m_reuse.Invoke(this.gameObject);
    }
    public void SetReuse(Reuse _reuse)
    {
        m_reuse = _reuse;
    }
}
