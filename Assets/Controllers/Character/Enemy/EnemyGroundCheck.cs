using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    public EnemyController enemy;


    // Use this for initialization
    void Start()
    {
        enemy = gameObject.GetComponent<EnemyController>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            enemy.grounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("Dat"))
            enemy.grounded = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("Dat"))
            enemy.grounded = false;
    }
}
