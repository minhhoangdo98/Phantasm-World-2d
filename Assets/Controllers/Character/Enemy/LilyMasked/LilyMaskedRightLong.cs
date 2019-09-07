using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyMaskedRightLong : MonoBehaviour
{
    [SerializeField]
    private GameObject lilyMasked;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !lilyMasked.GetComponent<EnemyController>().faceRight)
        {
            lilyMasked.GetComponent<EnemyController>().Flip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (lilyMasked.GetComponent<EnemyController>().jumpable)
            {
                lilyMasked.GetComponent<EnemyController>().r2.AddForce(Vector2.up * lilyMasked.GetComponent<EnemyController>().jumpPow);
                StartCoroutine(lilyMasked.GetComponent<EnemyController>().JumpDelay());
            }
            lilyMasked.GetComponent<EnemyController>().move = 0;
        }
    }

}
