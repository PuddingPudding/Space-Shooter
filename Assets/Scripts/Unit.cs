using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public enum ETeam
    {
        Player,
        Enemy,
        None
    }
    [SerializeField] protected float m_fMaxHp = 1;
    [SerializeField] protected float m_fSpeed = 5;
    [SerializeField] protected float m_fAttack = 1;
    [SerializeField] protected SpriteRenderer m_spriteRendererFace; //外貌
    [SerializeField] protected Rigidbody2D m_rigidbody2D;
    protected float m_fCurrentHp;
    protected ETeam m_eTeam = ETeam.None;

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

    public void SetFace(Sprite _spriteNewFace)
    {
        m_spriteRendererFace.sprite = _spriteNewFace;
    }

    public void Move(Vector2 _v2Dir)
    {
        m_rigidbody2D.velocity = _v2Dir.normalized * m_fSpeed;
    }
    public void Move(Vector2 _v2Dir , float _fTimes)
    {
        m_rigidbody2D.velocity = _v2Dir.normalized * m_fSpeed * _fTimes;
    }
    public abstract void Hit(float _fDamage);
}