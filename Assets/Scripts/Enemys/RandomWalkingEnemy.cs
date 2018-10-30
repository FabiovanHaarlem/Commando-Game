using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkingEnemy : Enemy
{

    private float m_timer;
    private float m_timerReset;


	
	protected override void Start ()
    {
        base.Start();
        m_timer = 1;
        m_timerReset = m_timer;
        m_direction = 0;
        m_state = (int)EnemyState.Walking;
	}


    protected override void Update ()
    {
        base.Update();

        m_timer -= Time.deltaTime;

        if (m_timer <= 0)
        {
            float oldDirection = m_direction;
            m_direction = GetRandomNumber();

            if (m_direction == oldDirection)
            {
                m_direction = GetRandomNumber();
            }

            m_timer = m_timerReset;
        }
	}

    private float GetRandomNumber()
    {
        float newDirection = Random.Range(0, 8);    

        return newDirection;
    }
}


