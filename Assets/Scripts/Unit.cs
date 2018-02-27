using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum ETeam
    {
        Player,
        Enemy,
        None
    }
    [SerializeField] private float m_fMaxHp = 1;
    [SerializeField] private float m_fSpeed = 5;
    [SerializeField] private float m_fAttack = 1;
    [SerializeField] private SpriteRenderer m_spriteRendererFace; //外貌
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    private float m_fCurrentHp;
    private ETeam m_eTeam = ETeam.None;

    private void Awake()
    {
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
    public void Hit(float _fDamage)
    {
        m_fCurrentHp -= _fDamage;
        if(m_fCurrentHp < 0)
        {
            m_fCurrentHp = 0;
        }
        Debug.Log("痛");
    }
}