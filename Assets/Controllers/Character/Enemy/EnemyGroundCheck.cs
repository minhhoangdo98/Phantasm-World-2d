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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Dat")
            enemy.grounded = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Dat")
            enemy.grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Dat")
            enemy.grounded = false;
    }
}
