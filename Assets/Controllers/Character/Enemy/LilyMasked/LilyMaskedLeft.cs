using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyMaskedLeft : MonoBehaviour
{
    [SerializeField]
    private GameObject lilyMasked;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && lilyMasked.GetComponent<EnemyController>().diChuyen && !lilyMasked.GetComponent<EnemyController>().backMove)
        {
            StartCoroutine(lilyMasked.GetComponent<EnemyController>().BackStep(1));
        }
        if (collision.tag == "PlayerBullet" && lilyMasked.GetComponent<EnemyController>().diChuyen && lilyMasked.GetComponent<EnemyController>().jumpable)
        {
            lilyMasked.GetComponent<EnemyController>().r2.AddForce(Vector2.up * lilyMasked.GetComponent<EnemyController>().jumpPow);
            StartCoroutine(lilyMasked.GetComponent<EnemyController>().JumpDelay());
        }
        if (collision.tag == "Tuong" && lilyMasked.GetComponent<EnemyController>().jumpable)
        {
            lilyMasked.GetComponent<EnemyController>().r2.AddForce(Vector2.up * lilyMasked.GetComponent<EnemyController>().jumpPow * 1.5f);
            lilyMasked.GetComponent<EnemyController>().move = 1;
            StartCoroutine(lilyMasked.GetComponent<EnemyController>().JumpDelay());
        }
    }
   
}
