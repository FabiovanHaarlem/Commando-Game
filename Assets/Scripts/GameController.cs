using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_player;
    [SerializeField]
    private GameObject m_camera;

    private AudioSource m_mainTheme;
	
	private void Start ()
    {
        m_mainTheme = GetComponent<AudioSource>();

        m_mainTheme.Play();       
	}

    public void StopAudio()
    {
        m_mainTheme.Stop();
    }
	
	
	private void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (m_player.transform.position.y >= m_camera.transform.position.y - 50 && m_camera.transform.position.y < 1530)
        {
            m_camera.transform.Translate(Vector3.up * (50 * Time.deltaTime));
        }
	}
}
