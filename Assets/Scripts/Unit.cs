using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, ICollidableBox
{
    public delegate void Reuse(GameObject _gameObject);
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
    [SerializeField] protected BoxCollider2D m_collider;
    protected float m_fCurrentHp;
    protected ETeam m_eTeam = ETeam.None;
    protected Reuse m_reuse;
    protected ExplodeScript m_explodeScript;

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

    public float ColLeft
    {
        get { return this.transform.position.x + m_collider.offset.x - (m_collider.bounds.size.x / 2); }
    }
    public float ColRight
    {
        get { return this.transform.position.x + m_collider.offset.x + (m_collider.bounds.size.x / 2); }
    }
    public float ColTop
    {
        get { return this.transform.position.y + m_collider.offset.y + (m_collider.bounds.size.y / 2); }
    }
    public float ColBotton
    {
        get { return this.transform.position.y + m_collider.offset.y - (m_collider.bounds.size.y / 2); }
    }

    public float Attack
    {
        get { return this.m_fAttack; }
    }
    public float CurrentHP
    {
        get { return this.m_fCurrentHp; }
    }

    public void SetFace(Sprite _spriteNewFace)
    {
        m_spriteRendererFace.sprite = _spriteNewFace;
    }

    public void Move(Vector2 _v2Dir)
    {
        //this.transform.Translate(_v2Dir.normalized * m_fSpeed * Time.deltaTime,Space.World);
        this.transform.position += (Vector3)_v2Dir.normalized * m_fSpeed * Time.deltaTime;
    }
    public void Move(Vector2 _v2Dir, float _fTimes)
    {
        //this.transform.Translate(_v2Dir.normalized * m_fSpeed * Time.deltaTime * _fTimes,Space.World);
        this.transform.position += (Vector3)_v2Dir.normalized * m_fSpeed * Time.deltaTime * _fTimes;
    }

    public void SetExplode(ExplodeScript _explodeScript)
    {
        _explodeScript.gameObject.SetActive(false);
        m_explodeScript = _explodeScript;
    }

    public void SetReuse(Reuse _reuse)
    {
        this.m_reuse = _reuse;
    }
    public void KillSelf() //自殺有時能解決問題
    {
        if (this.m_reuse != null)
        {
            m_reuse.Invoke(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Init(Sprite _spriteNewFace, Reuse _reuse, ETeam _team)
    {
        this.SetFace(_spriteNewFace);
        this.SetReuse(_reuse);
        this.m_eTeam = _team;
        m_fCurrentHp = m_fMaxHp;
    }

    public abstract void Hit(float _fDamage); //受傷
}