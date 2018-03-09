using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCollider : MonoBehaviour, ICollidableBox
{
    [SerializeField] Collider2D m_collider;

    public float ColLeft
    {
        get
        {
            return this.transform.position.x + m_collider.offset.x - (m_collider.bounds.size.x/2);
        }
    }

    public float ColRight
    {
        get
        {
            return this.transform.position.x + m_collider.offset.x + (m_collider.bounds.size.x / 2);
        }
    }

    public float ColTop
    {
        get
        {
            return this.transform.position.y + m_collider.offset.y + (m_collider.bounds.size.y / 2);
        }
    }

    public float ColBotton
    {
        get
        {
            return this.transform.position.y + m_collider.offset.y - (m_collider.bounds.size.y / 2);
        }
    }
}
