using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBorder : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D enemy)
    { 
        if (enemy.gameObject.tag == "Enemy")
        {
            Destroy(enemy.gameObject);
        }
    }

}
