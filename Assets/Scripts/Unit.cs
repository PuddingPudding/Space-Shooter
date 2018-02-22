using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float m_fMaxHp = 1;
    [SerializeField] private float m_fSpeed = 5;
    [SerializeField] private float m_fAttack = 1;
    private float m_fCurrentHp;
    private Rigidbody2D m_rigidbody2D;

    private void Awake()
    {
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 _v2Dir)
    {
        m_rigidbody2D.velocity = _v2Dir.normalized * m_fSpeed;
    }
    public void Move(Vector2 _v2Dir , float _fTimes)
    {
        m_rigidbody2D.velocity = _v2Dir.normalized * m_fSpeed * _fTimes;
    }
    public void Hit(float _fDamage)
    {
        m_fCurrentHp -= _fDamage;
        if(m_fCurrentHp < 0)
        {
            m_fCurrentHp = 0;
        }
    }
}