using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Unit m_player;
    [SerializeField] private Sprite m_spritePlayerFace;
    [SerializeField] private int m_iMaxBulletNum;
    private Queue<BulletScript> m_playerBullets;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v2FinalSpeed = Vector2.zero;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            v2FinalSpeed += Vector2.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            v2FinalSpeed += Vector2.left;
        }
        m_player.Move(v2FinalSpeed);
    }
}
