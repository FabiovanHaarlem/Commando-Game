using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCoverEnemy : Enemy
{
    [SerializeField]
    private GameObject m_player;

    protected override void Start ()
    {
        base.Start();
        

        m_direction = (int)EnemyDirection.Down;
        m_state = (int)EnemyState.InCover;

        m_moveSpeed = 0;
	}
	
	
	protected override void Update ()
    {
        base.Update();

        if (m_player.transform.position.x < transform.position.x - 50)
        {
            m_direction = (int)EnemyDirection.DownLeft;
        }
        else if (m_player.transform.position.x > transform.position.x && m_player.transform.position.x < transform.position.x + 50)
        {
            m_direction = (int)EnemyDirection.Down;
        }
        else if (m_player.transform.position.x > transform.position.x + 50)
        {
            m_direction = (int)EnemyDirection.RightDown;
        }
        else
        {
            m_direction = (int)EnemyDirection.Down;
        }
    }
}
