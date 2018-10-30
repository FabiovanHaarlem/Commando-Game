using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyActive : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_enemys;


    private void Start()
    {
        //m_enemys = new List<GameObject>();

        for (int i = 0; i < m_enemys.Count; i++)
        {
            m_enemys[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
       
        if (player.gameObject.tag == "Player")
        {

            for (int i = 0; i < m_enemys.Count; i++)
            {
                print("H");

                m_enemys[i].SetActive(true);
            }
        }
    }
}
