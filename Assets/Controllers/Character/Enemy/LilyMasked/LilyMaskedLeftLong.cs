using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyMaskedLeftLong : MonoBehaviour
{
    [SerializeField]
    private GameObject lilyMasked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (lilyMasked.GetComponent<EnemyController>().jumpable && lilyMasked.GetComponent<EnemyController>().diChuyen && lilyMasked.GetComponent<EnemyController>().hoatDong)
            {
                lilyMasked.GetComponent<EnemyController>().r2.AddForce(Vector2.up * lilyMasked.GetComponent<EnemyController>().jumpPow);
                StartCoroutine(lilyMasked.GetComponent<EnemyController>().JumpDelay());
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && lilyMasked.GetComponent<EnemyController>().faceRight)
        {
            lilyMasked.GetComponent<EnemyController>().Flip();
        }
        if (collision.tag == "PlayerBullet" && lilyMasked.GetComponent<EnemyController>().diChuyen && !lilyMasked.GetComponent<EnemyController>().backMove)
        {
            lilyMasked.GetComponent<EnemyController>().BackStep(1);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (lilyMasked.GetComponent<EnemyController>().jumpable && lilyMasked.GetComponent<EnemyController>().diChuyen && lilyMasked.GetComponent<EnemyController>().hoatDong)
            {
                lilyMasked.GetComponent<EnemyController>().r2.AddForce(Vector2.up * lilyMasked.GetComponent<EnemyController>().jumpPow);
                StartCoroutine(lilyMasked.GetComponent<EnemyController>().JumpDelay());
            }
        }
    }

    

}
