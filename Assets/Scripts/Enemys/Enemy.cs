using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator m_animator;

    private SpriteRenderer m_spriteRenderer;

    protected int m_moveSpeed;
    [SerializeField]
    protected int m_state;
    [SerializeField]
    protected float m_direction;
    [SerializeField]
    protected float m_shootTimer;
    protected float m_shootTimerReset;


    protected virtual void Start()
    {
        m_animator = GetComponent<Animator>();

        m_spriteRenderer = GetComponent<SpriteRenderer>();

        m_moveSpeed = 30;
        m_shootTimer = 3;
        m_shootTimerReset = m_shootTimer;
    }
	
	protected virtual void Update ()
    {
        m_shootTimer -= Time.deltaTime;
        
        if (m_shootTimer <= 0)
        {
            ShootBullet();
        }

        Movement();

        m_animator.SetFloat("Direction", (float)m_direction);
        m_animator.SetInteger("State", (int)m_state);
    }

    private void Movement()
    {
        Vector3 newEnemyPosition = new Vector3();

        switch ((int)m_direction)
        {
            case 0:
                newEnemyPosition = new Vector3(transform.position.x, transform.position.y + (m_moveSpeed * Time.deltaTime));
                break;
            case 1:
                newEnemyPosition = new Vector3(transform.position.x + (m_moveSpeed * Time.deltaTime), transform.position.y + (m_moveSpeed * Time.deltaTime));
                break;
            case 2:
                newEnemyPosition = new Vector3(transform.position.x + (m_moveSpeed * Time.deltaTime), transform.position.y);
                break;
            case 3:
                newEnemyPosition = new Vector3(transform.position.x + (m_moveSpeed * Time.deltaTime), transform.position.y - (m_moveSpeed * Time.deltaTime));
                break;
            case 4:
                newEnemyPosition = new Vector3(transform.position.x, transform.position.y - (m_moveSpeed * Time.deltaTime));
                break;
            case 5:
                newEnemyPosition = new Vector3(transform.position.x - (m_moveSpeed * Time.deltaTime), transform.position.y - (m_moveSpeed * Time.deltaTime));
                break;
            case 6:
                newEnemyPosition = new Vector3(transform.position.x - (m_moveSpeed * Time.deltaTime), transform.position.y);
                break;
            case 7:
                newEnemyPosition = new Vector3(transform.position.x - (m_moveSpeed * Time.deltaTime), transform.position.y + (m_moveSpeed * Time.deltaTime));
                break;
        }

        transform.position = newEnemyPosition;
    }

    private void ShootBullet()
    {

        float spriteHeight = m_spriteRenderer.bounds.size.y;
        float spriteWidth = m_spriteRenderer.bounds.size.x;

        Vector3 centerOffSet = GetCenterOffSprite(spriteHeight);
        Vector3 bulletOffSet = GetBulletOffSet(centerOffSet, spriteHeight, spriteWidth);

        GameObject bullet = Instantiate(Resources.Load("Prefabs\\Bullet"), bulletOffSet, transform.rotation) as GameObject;
        bullet.GetComponent<Bullet>().Initialize(m_direction, "Enemy");

        m_shootTimer = m_shootTimerReset;
    }

    private Vector3 GetCenterOffSprite(float spriteHeight)
    {
        Vector3 centerOffSet = new Vector3(transform.position.x, (transform.position.y) + (spriteHeight - spriteHeight + (spriteHeight / 2)), 0);
        return centerOffSet;
    }

    private Vector3 GetBulletOffSet(Vector3 centerOffSet, float spriteHeight, float spriteWidth)
    {
        Vector3 bulletOffSet = new Vector3(centerOffSet.x, centerOffSet.y);

        switch ((int)m_direction)
        {
            case (int)EnemyDirection.Up:
                bulletOffSet.x = centerOffSet.x;
                bulletOffSet.y = centerOffSet.y + (spriteHeight / 2);
                break;
            case (int)EnemyDirection.UpRight:
                bulletOffSet.x = centerOffSet.x + (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y + (spriteHeight / 2);
                break;
            case (int)EnemyDirection.Right:
                bulletOffSet.x = centerOffSet.x + (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y;
                break;
            case (int)EnemyDirection.RightDown:
                bulletOffSet.x = centerOffSet.x + (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y - (spriteHeight / 2);
                break;
            case (int)EnemyDirection.Down:
                bulletOffSet.x = centerOffSet.x;
                bulletOffSet.y = centerOffSet.y - (spriteHeight / 2);
                break;
            case (int)EnemyDirection.DownLeft:
                bulletOffSet.x = centerOffSet.x - (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y - (spriteHeight / 2);
                break;
            case (int)EnemyDirection.Left:
                bulletOffSet.x = centerOffSet.x - (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y;
                break;
            case (int)EnemyDirection.LeftUp:
                bulletOffSet.x = centerOffSet.x - (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y + (spriteHeight / 2);
                break;
        }

        return bulletOffSet;
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}

public enum EnemyState
{
    Idle = 0,
    Walking,
    Granade,
    Death,
    InCover
}

public enum EnemyDirection
{
    Up = 0,
    UpRight,
    Right,
    RightDown,
    Down,
    DownLeft,
    Left,
    LeftUp
}
