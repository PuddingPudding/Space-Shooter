using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit , ISurrounding //另外在實做 "周圍" 介面
{
    public delegate void Explode(GameObject _gameObject);
    [SerializeField] private List<Sprite> m_enemyFaces;
    private Explode m_explode;
    private EnemyUnit[] m_arrNeighbors = new EnemyUnit[System.Enum.GetNames(typeof(EDirection)).Length];
    //System.Enum.GetNames(typeof(EDirection)).Length是用來查有幾個方向用的

    public EnemyUnit(int _iFaceNum, Explode _explode)
    {
        this.Team = Unit.ETeam.Enemy;
        if (_iFaceNum < m_enemyFaces.Count)
        {
            this.SetFace(m_enemyFaces[_iFaceNum]);
        }
        this.SetExplode(_explode);
    }
    protected EnemyUnit()
    {
    }

    public void SetEnemy(int _iFaceNum, Explode _explode)
    {
        //this.Team = Unit.ETeam.Enemy;
        if (_iFaceNum < m_enemyFaces.Count)
        {
            this.SetFace(m_enemyFaces[_iFaceNum]);
        }
        this.SetExplode(_explode);
    }

    private void Awake()
    {
        m_fCurrentHp = m_fMaxHp;
    }

    public override void Hit(float _fDamage)
    {
        m_fCurrentHp -= _fDamage;
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

    public ISurrounding GetNeighbor(EDirection _eDir)
    {
        return this.m_arrNeighbors[(int)_eDir];
    }

    public void SetNeighbor(ISurrounding _anotherSurrounding, EDirection _eDir)
    {
        EnemyUnit anotherEnemy = (EnemyUnit)_anotherSurrounding;
        this.m_arrNeighbors[(int)_eDir] = anotherEnemy;
    }

    public bool HasNeighbor(EDirection _eDir)
    {
        bool hasNeighbor = false;
        if(this.m_arrNeighbors[(int)_eDir] != null)
        {
            if(this.m_arrNeighbors[(int)_eDir].isActiveAndEnabled)
            {
                hasNeighbor = true;
            }
        }
        return hasNeighbor;
    }

    public bool HasNeighborThorough(EDirection _eDir)
    {
        bool hasNeighbor = false;
        EnemyUnit enemyTemp = this;
        while (enemyTemp.GetNeighbor(_eDir) != null && !hasNeighbor)
        {
            if(enemyTemp.HasNeighbor(_eDir) )
            {
                hasNeighbor = true;
            }
            else
            {
                enemyTemp = (EnemyUnit)enemyTemp.GetNeighbor(_eDir);
            }
        }
        return hasNeighbor;
    }
}
