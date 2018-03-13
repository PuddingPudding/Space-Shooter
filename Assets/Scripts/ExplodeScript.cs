using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    public delegate void Event();
    public delegate void Reuse(GameObject _gameObject);
    private Event m_event;
    private Reuse m_reuse;

    private void AnimationEnd() //自訂的函式，在動畫結束時呼叫
    {
        if (m_reuse != null)
        {
            m_reuse.Invoke(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        if(m_event != null)
        {
            m_event.Invoke();
        }
    }

    public void SetEvent(Event _event)
    {
        m_event = _event;
    }
    public void SetReuse(Reuse _reuse)
    {
        m_reuse = _reuse;
    }

}
