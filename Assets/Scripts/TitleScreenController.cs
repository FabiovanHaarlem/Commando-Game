using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_title;
    [SerializeField]
    private GameObject m_camera;
    [SerializeField]
    private GameObject m_cameraEndPoint;
    [SerializeField]
    private GameObject m_level;

    [SerializeField]
    private string m_sceneName;

    private Vector3[] m_positions;
    private Vector3 m_cameraStartPosition;
    private Vector3 m_levelStartPosition;

    private AudioSource m_mainTheme;

    private float m_moveTimer;
    private float m_moveTimerReset;

    private int m_titlePositionState;
    private int m_upOrDown;


    void Start ()
    {
        m_mainTheme = GetComponent<AudioSource>();

        m_positions = new Vector3[4];

        for (int i = 0; i < 4; i++)
        {
            m_positions[i] = new Vector3(m_title.transform.position.x, m_title.transform.position.y - (0.1f * (i + 1)));
        }

        m_upOrDown = 0;

        m_cameraStartPosition = transform.position;
        m_levelStartPosition = m_level.transform.position;
        m_moveTimer = 0.2f;
        m_moveTimerReset = m_moveTimer;

        m_mainTheme.Play();
	}
	
	
	void Update ()
    {
        m_level.transform.position = new Vector3(m_level.transform.position.x, m_level.transform.position.y - 0.02f, m_level.transform.position.z);

        if (m_camera.transform.position.y >= m_cameraEndPoint.transform.position.y)
        {
            m_level.transform.position = m_levelStartPosition;
            
        }

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(m_sceneName);
        }
        MoveTitleUpAndDown();
    }

    private void MoveTitleUpAndDown()
    {
        m_moveTimer -= Time.deltaTime;

        if (m_upOrDown == 0)
        {
            if (m_titlePositionState == 3)
            {
                m_upOrDown = 1;
            }
            else if (m_moveTimer < 0)
            {
                m_titlePositionState += 1;
                m_moveTimer = m_moveTimerReset;
            }
        }
        else if (m_upOrDown == 1)
        {
            if (m_titlePositionState == 0)
            {
                m_upOrDown = 0;
            }
            else if (m_moveTimer < 0)
            {
                m_titlePositionState -= 1;
                m_moveTimer = m_moveTimerReset;
            }
        }


        m_title.transform.position = m_positions[m_titlePositionState];
    }
}
