using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateEnemy : Enemy
{
   	
    public void Initialize(int direction)
    {
        m_direction = direction;
        m_state = 1;
    }

    protected override void Start()
    {
        base.Start();

        m_shootTimer = 0.5f;
        m_shootTimerReset = m_shootTimer;
    }
}
