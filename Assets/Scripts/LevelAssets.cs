using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAssets : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;

	
	void Start ()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	
	void Update ()
    {
		m_spriteRenderer.sortingOrder = (int)(transform.position.y - transform.position.y * 2);
    }
}
