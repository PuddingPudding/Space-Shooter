using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public delegate void Explode(GameObject _gameObject);
    [SerializeField] private List<Sprite> m_playerFaces;
    private Explode m_explode;

    public PlayerUnit(int _iFaceNum, Explode _explode)
    {
        if (_iFaceNum < m_playerFaces.Count)
        {
            this.SetFace(m_playerFaces[_iFaceNum]);
        }
        this.SetExplode(_explode);
    }
    protected PlayerUnit()
    {
    }

    public void SetPlayer(int _iFaceNum, Explode _explode)
    {
        if (_iFaceNum < m_playerFaces.Count)
        {
            this.SetFace(m_playerFaces[_iFaceNum]);
        }
        this.SetExplode(_explode);
    }

    public override void Hit(float _fDamage)
    {
        m_fCurrentHp -= _fDamage;
        if(m_fCurrentHp <= 0)
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
