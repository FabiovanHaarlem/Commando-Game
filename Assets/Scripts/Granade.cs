using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    private Animator m_animator;

    private int m_granadeSpeed;

    private float m_direction;
    private float m_granadeAliveTimer;
    private float m_granadeAnimationTimer;



    public void Initialize(float direction)
    {
        m_animator = GetComponent<Animator>();

        m_direction = direction;
        m_granadeSpeed = 100;
        m_granadeAliveTimer = 1.0f;
        m_granadeAnimationTimer = 0.5f;
    }

    void Update()
    {
        m_granadeAliveTimer -= Time.deltaTime;

        if (m_granadeAliveTimer >= 0)
        {
            MoveBullet();
        }

        if (m_granadeAliveTimer <= 0)
        {
            DestroyGranade();
        }
    }

    private void MoveBullet()
    {         
        transform.Translate(new Vector3(0, m_granadeSpeed, 0) * Time.deltaTime);
    }

    private void DestroyGranade()
    {
        m_granadeAnimationTimer -= Time.deltaTime;

        m_animator.SetInteger("State", 1);

        if (m_granadeAnimationTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
