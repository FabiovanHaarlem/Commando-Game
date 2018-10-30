using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovingEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        m_direction = (int)EnemyDirection.Down;
        m_state = (int)EnemyState.Walking;
	}
}
