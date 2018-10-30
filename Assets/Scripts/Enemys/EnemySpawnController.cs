using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_enemys;

    private Random m_randomGenarator;

    void Start ()
    {
        m_randomGenarator = new Random();
	}
	
	
	void Update ()
    {
		
	}


}
