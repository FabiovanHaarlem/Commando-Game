using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator m_animator;

    private int m_bulletSpeed;

    private float m_direction;
    private float m_bulletAliveTimer;
    private float m_bulletAnimationTimer;

    private string m_tag;



    public void Initialize(float direction, string tag)
    {
        m_animator = GetComponent<Animator>();

        m_direction = direction;
        m_bulletSpeed = 200;
        m_bulletAliveTimer = 0.5f;
        m_bulletAnimationTimer = 0.2f;
        m_tag = tag;
    }	
	
	void Update ()
    {
        m_bulletAliveTimer -= Time.deltaTime;

        if (m_bulletAliveTimer >= 0)
        {
            MoveBullet();
        }

        if (m_bulletAliveTimer <= 0)
        {
            DestroyBullet();
        }    
	}

    private void MoveBullet()
    {
        switch ((int)m_direction)
        {
            case 0:
                transform.Translate(new Vector3(0, m_bulletSpeed, 0) * Time.deltaTime);
                break;
            case 1:
                transform.Translate(new Vector3(m_bulletSpeed, m_bulletSpeed, 0) * Time.deltaTime);
                break;
            case 2:
                transform.Translate(new Vector3(m_bulletSpeed, 0, 0) * Time.deltaTime);
                break;
            case 3:
                transform.Translate(new Vector3(m_bulletSpeed, -m_bulletSpeed, 0) * Time.deltaTime);
                break;
            case 4:
                transform.Translate(new Vector3(0, -m_bulletSpeed, 0) * Time.deltaTime);
                break;
            case 5:
                transform.Translate(new Vector3(-m_bulletSpeed, -m_bulletSpeed, 0) * Time.deltaTime);
                break;
            case 6:
                transform.Translate(new Vector3(-m_bulletSpeed, 0, 0) * Time.deltaTime);
                break;
            case 7:
                transform.Translate(new Vector3(-m_bulletSpeed, m_bulletSpeed, 0) * Time.deltaTime);
                break;
        }
    }

    private void DestroyBullet()
    {
        m_bulletAnimationTimer -= Time.deltaTime;

        m_animator.SetInteger("State", 1);

        if (m_bulletAnimationTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_tag == "Enemy")
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player>().Death();
                m_bulletAliveTimer = 0;
            }
        }

        if (m_tag == "Player")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().Death();
                m_bulletAliveTimer = 0;
            }
        }
    }
}
