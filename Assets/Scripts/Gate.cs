using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    private List<GameObject> m_doors;

    [SerializeField]
    private GameObject  m_gateClosedLeft, m_gateClosedRight, m_gateOpenLeft, m_gateOpenRight, m_gateEnemySpawnPoint, m_arrow;
    [SerializeField]
    private GameObject m_player, m_gameHandler;

    private bool m_spawnEnemys;
    private float m_nextWaveTimer;
    private int m_waves;
    private AudioSource m_victoryMusic;

    void Start ()
    {
        m_doors = new List<GameObject>();
        m_doors.Add(m_gateClosedLeft);
        m_doors.Add(m_gateClosedRight);
        m_doors.Add(m_gateOpenLeft);
        m_doors.Add(m_gateOpenRight);

        m_doors[2].SetActive(false);
        m_doors[3].SetActive(false);
        m_spawnEnemys = false;
        m_nextWaveTimer = 0;
        m_waves = 4;
        m_arrow.SetActive(false);
        m_victoryMusic = GetComponent<AudioSource>();
        m_victoryMusic.Stop();
    }
	
	
	void Update ()
    {
		if (m_spawnEnemys)
        {           
            SpawnEnemys();
        }
	}

    private  void SpawnEnemys()
    {
        m_nextWaveTimer -= Time.deltaTime;
        if (m_nextWaveTimer <= 0 && m_waves != 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 spawnPosition = new Vector3(m_gateEnemySpawnPoint.transform.position.x + Random.Range(-10, 10), m_gateEnemySpawnPoint.transform.position.y);
                GameObject enemy = Instantiate(Resources.Load("Prefabs\\Enemys\\GateEnemy"), spawnPosition, m_gateEnemySpawnPoint.transform.rotation) as GameObject;
                enemy.GetComponent<GateEnemy>().Initialize(3 + i);
            }

            m_waves -= 1;
            m_nextWaveTimer = 3;
        }
        
        if (m_waves == 0 && m_nextWaveTimer <= -3)
        {
            m_arrow.SetActive(true);
            m_victoryMusic.Play();
            m_gameHandler.GetComponent<GameController>().StopAudio();

            if (m_player.transform.position.y > 1590)
            {
                SceneManager.LoadScene("TitleScreen");

            }
        }
    }

    public void OpenGate()
    {
        m_doors[0].SetActive(false);
        m_doors[1].SetActive(false);
        m_doors[2].SetActive(true);
        m_doors[3].SetActive(true);
        m_spawnEnemys = true;
    }
}
