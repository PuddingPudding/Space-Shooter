using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingTest : MonoBehaviour , ICollidableBox
{
    //[SerializeField] Collider2D m_collider;
    //[SerializeField] Collider2D m_otherCollider;
    [SerializeField] private SimpleCollider m_otherColliderBox;
    [SerializeField] Vector2 m_v2Offset; //自訂碰撞器對於自己的相對位子
    [SerializeField] Vector2 m_v2Size;//自訂碰撞器的大小

    public float ColLeft
    {
        get
        {
            return this.transform.position.x + m_v2Offset.x - ((m_v2Size.x * this.transform.lossyScale.x) /2);
        }
    }

    public float ColRight
    {
        get
        {
            return this.transform.position.x + m_v2Offset.x + ((m_v2Size.x * this.transform.lossyScale.x) / 2);
        }
    }

    public float ColTop
    {
        get
        {
            return this.transform.position.y + m_v2Offset.y + ((m_v2Size.y * this.transform.lossyScale.y) / 2);
        }
    }

    public float ColBotton
    {
        get
        {
            return this.transform.position.y + m_v2Offset.y - ((m_v2Size.y * this.transform.lossyScale.y) / 2);
        }
    }
    // Use this for initialization
    void Start()
    {
        //m_collider = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(m_collider.IsTouching(m_otherCollider) )
        //{
        //    Debug.Log("碰到了");
        //}

        //if (Physics.Raycast(transform.position, Vector3.right, 10))
        //{
        //    Debug.Log("There is something in front of the object!");
        //}            

        if(CustomCollision.CollisionDetection(this , m_otherColliderBox))
        {
            Debug.Log("AABB碰觸");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(this.transform.right * 3 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(this.transform.right * -3 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(this.transform.up * 3 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(this.transform.up * -3 * Time.deltaTime);
        }
    }
}
