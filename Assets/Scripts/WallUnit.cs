using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUnit : Unit
{
    public delegate void Explode(GameObject _gameObject);
    [SerializeField] private Sprite m_normalWall;
    [SerializeField] private Sprite m_damagedWall;
    private Explode m_explode;

    protected WallUnit()
    {
    }

    private void Awake()
    {
        m_fCurrentHp = m_fMaxHp;
    }

    public void SetWall(Explode _explode)
    {
        this.Team = Unit.ETeam.None;
        m_fCurrentHp = m_fMaxHp;
        this.SetFace(m_normalWall);
        this.SetExplode(_explode);
    }

    public override void Hit(float _fDamage)
    {
        m_fCurrentHp -= _fDamage;
        if (m_fCurrentHp <= m_fMaxHp / 2)
        {
            this.SetFace(m_damagedWall);
        }
        if (m_fCurrentHp <= 0)
        {
            GameObject explodePrefab = UnitPool.Instance.GetExplodePrefab();
            explodePrefab.transform.position = this.transform.position;
            if (m_explode != null)
            {
                m_explode.Invoke(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void SetExplode(Explode _explode)
    {
        m_explode = _explode;
    }

}
