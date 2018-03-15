using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUnit : Unit
{
    private Sprite m_sprDamagedFace;

    protected WallUnit()
    {
    }

    private void Awake()
    {
    }

    public void SetDamagedFace(Sprite _sprDamagedFace)
    {
        this.m_sprDamagedFace = _sprDamagedFace;
    }

    public void Init(Sprite _spriteNewFace, Reuse _reuse, ETeam _team , Sprite _sprDamagedFace)
    {
        base.Init( _spriteNewFace,  _reuse,  _team);
        this.SetDamagedFace(_sprDamagedFace);
    }

    public override void Hit(float _fDamage)
    {
        m_fCurrentHp -= _fDamage;
        if (m_fCurrentHp <= m_fMaxHp / 2 && m_sprDamagedFace != null)
        {
            this.SetFace(m_sprDamagedFace);
        }
        if (m_fCurrentHp <= 0)
        {
            this.KillSelf(); //否則便自行解決
            if (m_explodeScript != null)
            {
                m_explodeScript.transform.position = this.transform.position;
                m_explodeScript.gameObject.SetActive(true); //讓爆炸發生，交由爆炸去處理自己死亡的事件
            }
        }
    }

}
