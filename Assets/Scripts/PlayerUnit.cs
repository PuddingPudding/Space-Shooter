using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    private ExplodeScript m_explodeScript;

    protected PlayerUnit()
    {
    }
    
    private void Awake()
    {
        m_fCurrentHp = m_fMaxHp;
    }

    public void SetExplode(ExplodeScript _explodeScript)
    {
        _explodeScript.gameObject.SetActive(false);
        m_explodeScript = _explodeScript;
    }

    public override void Hit(float _fDamage)
    {
        m_fCurrentHp -= _fDamage;
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
