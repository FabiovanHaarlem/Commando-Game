using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator m_animator;

    private SpriteRenderer m_spriteRenderer;

    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private GameObject m_gate;

    private Vector3 m_position;
    [SerializeField]

    private bool m_up;
    private bool m_right;
    private bool m_down;
    private bool m_left;
    private bool m_shoot;
    private bool m_x;
    private bool m_z;

    [SerializeField]
    private int m_granades;
    private int m_moveSpeed;
    private int m_state;

    private float m_direction;

    private void Start ()
    {
        m_position = new Vector3();

        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        m_moveSpeed = 50;
        m_granades = 5;
    }


    private void Update ()
    {
        GetPlayerInput();
        
        SetPlayerState();
        SetPlayerDirection();
        ShootBullet();
        ThrowGranade();

        m_animator.SetFloat("Direction",(float)m_direction);
        m_animator.SetInteger("State",(int)m_state);
        
        m_spriteRenderer.sortingOrder = (int)(transform.position.y - transform.position.y * 2);  
    }

    void FixedUpdate()
    {
        MovePlayer();
        
        m_rigidbody.MovePosition((Vector3)m_rigidbody.position + m_position * Time.fixedDeltaTime);
        Vector3 playerPos = m_rigidbody.transform.position;
    }

    private void GetPlayerInput()
    {
        m_up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        m_right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        m_down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        m_left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        m_shoot = Input.GetKeyDown(KeyCode.Space);
        m_z = Input.GetKeyDown(KeyCode.Z);
        m_x = Input.GetKeyDown(KeyCode.X);
    }

    private void MovePlayer()
    {
        m_position = new Vector3();

        if (m_up) { m_position.y = m_moveSpeed; }
        if (m_right) { m_position.x = m_moveSpeed; }
        if (m_down) { m_position.y = -m_moveSpeed; }
        if (m_left) { m_position.x = -m_moveSpeed; }
    }

    private void SetPlayerState()
    {
        if (!m_z && !m_x && !m_up && !m_right && !m_down && !m_left)
        {
            m_state = (int)PlayerState.Idle;
        }

        if (m_up || m_right || m_down || m_left)
        {
            m_state = (int)PlayerState.Walking;
        }

        if (m_granades > 0)
        {
            if (m_z)
            {
                m_state = (int)PlayerState.Granade;
            }
        }

        if (m_x)
        {
            m_state = (int)PlayerState.Death;
        }
    }

    private void SetPlayerDirection()
    {
        if (m_up)
        {
            if (m_right) { m_direction = (float)PlayerDirection.UpRight; }
            else if (m_left) { m_direction = (float)PlayerDirection.LeftUp; }
            else { m_direction = (float)PlayerDirection.Up; }
        }
        else if (m_down)
        {
            if (m_right) { m_direction = (float)PlayerDirection.RightDown; }
            else if (m_left) { m_direction = (float)PlayerDirection.DownLeft; }
            else { m_direction = (float)PlayerDirection.Down; }
        }
        else if (m_right) { m_direction = (float)PlayerDirection.Right; }
        else if (m_left) { m_direction = (float)PlayerDirection.Left; }
    }

    private void ShootBullet()
    {
        if (m_shoot)
        {
            
            float spriteHeight = m_spriteRenderer.bounds.size.y;
            float spriteWidth = m_spriteRenderer.bounds.size.x;

            Vector3 centerOffSet = GetCenterOffSprite(spriteHeight);
            Vector3 bulletOffSet = GetBulletOffSet(centerOffSet, spriteHeight, spriteWidth);
            
            GameObject bullet = Instantiate(Resources.Load("Prefabs\\Bullet"), bulletOffSet, transform.rotation) as GameObject;
            bullet.GetComponent<Bullet>().Initialize(m_direction, "Player");
        }
    }

    private void ThrowGranade()
    {
        if (m_granades > 0)
        {
            if (m_direction == 0 || m_direction == 1 || m_direction == 7)
            {
                if (m_z)
                {
                    GameObject granade = Instantiate(Resources.Load("Prefabs\\Granade"), transform.position, transform.rotation) as GameObject;
                    granade.GetComponent<Granade>().Initialize(m_direction);
                    m_granades -= 1;
                }
            }
        }
    }

    private Vector3 GetCenterOffSprite(float spriteHeight)
    {
        Vector3 centerOffSet = new Vector3(transform.position.x, (transform.position.y) + (spriteHeight - spriteHeight + (spriteHeight / 2)), 0);
        return centerOffSet;
    }

    private Vector3 GetBulletOffSet(Vector3 centerOffSet, float spriteHeight, float spriteWidth)

    {
        Vector3 bulletOffSet = new Vector3();
        print(centerOffSet);
        switch ((int)m_direction)
        {
            case (int)PlayerDirection.Up:
                bulletOffSet.x = centerOffSet.x;
                bulletOffSet.y = centerOffSet.y + (spriteHeight / 2);
                break;
            case (int)PlayerDirection.UpRight:
                bulletOffSet.x = centerOffSet.x + (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y + (spriteHeight / 2);
                break;
            case (int)PlayerDirection.Right:
                bulletOffSet.x = centerOffSet.x + (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y;
                break;
            case (int)PlayerDirection.RightDown:
                bulletOffSet.x = centerOffSet.x + (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y - (spriteHeight / 2);
                break;
            case (int)PlayerDirection.Down:
                bulletOffSet.x = centerOffSet.x;
                bulletOffSet.y = centerOffSet.y - (spriteHeight / 2);
                break;
            case (int)PlayerDirection.DownLeft:
                bulletOffSet.x = centerOffSet.x - (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y - (spriteHeight / 2);
                break;
            case (int)PlayerDirection.Left:
                bulletOffSet.x = centerOffSet.x - (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y;
                break;
            case (int)PlayerDirection.LeftUp:
                bulletOffSet.x = centerOffSet.x - (spriteWidth / 2);
                bulletOffSet.y = centerOffSet.y + (spriteHeight / 2);
                break;
        }

        return bulletOffSet;
    }

    public void Death()
    {
        m_moveSpeed = 0;
        m_state = (int)PlayerState.Death;
        SceneManager.LoadScene("CommandoGame");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GranadeBox")
        {
            
            m_granades += 5;
            m_granades = Mathf.Clamp(m_granades, 0, 5);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Gate")
        {
            Gate m_gateScript = m_gate.GetComponent<Gate>();

            m_gateScript.OpenGate();
        }
    }
}



public enum PlayerState
{
    Idle = 0,
    Walking, 
    Granade,
    Death
}

public enum PlayerDirection
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
