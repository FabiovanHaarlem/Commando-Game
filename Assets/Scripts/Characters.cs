using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    protected Animator m_animator;

    protected SpriteRenderer m_spriteRenderer;

    protected Rigidbody2D m_rigidbody;

    protected float m_direction;

    protected int m_moveSpeed;
    protected int m_state;

    protected virtual void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        m_moveSpeed = 50;
    }
}
