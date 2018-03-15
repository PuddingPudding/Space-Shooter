using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUnit : Unit
{
    [SerializeField] private float m_fMaxTime = 3;
    private float m_fExistTime;
    private Vector2 movDir = Vector2.up;

    public override void Hit(float _fDamage)
    {
        throw new System.NotImplementedException();
    }
    private void Update()
    {
        this.Move(this.transform.up);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.InitAndShoot(this.transform.up + new Vector3(1, 0, 0), this.transform.position, ETeam.None);
            Debug.Log(this.transform.up);
        }
        if (this.isActiveAndEnabled)
        {
            m_fExistTime += Time.deltaTime;
            if (m_fExistTime >= m_fMaxTime)
            {
                m_fExistTime = 0;
                this.KillSelf();
            }
        }
        //this.move(movdir);
        //if (input.getkeydown(keycode.q))
        //{
        //    movdir += vector2.right;
        //}
    }

    public void InitAndShoot(Vector2 _v2Dir, Vector2 _v2Pos, Unit.ETeam _eTeam)
    {
        m_eTeam = _eTeam;
        m_fExistTime = 0;
        this.transform.position = _v2Pos;
        this.transform.up = _v2Dir; //將自己的頭(編輯畫面下的向上箭頭)轉向到跟輸入的向量一樣的角度
    }
}
