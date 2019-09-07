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
            StartCoroutine(BackStep());
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

    IEnumerator BackStep()
    {
        lilyMasked.GetComponent<EnemyController>().flipable = false;
        lilyMasked.GetComponent<EnemyController>().backMove = true;
        lilyMasked.GetComponent<EnemyController>().speed = 300f;
        lilyMasked.GetComponent<EnemyController>().move = 1;
        yield return new WaitForSeconds(0.5f);
        lilyMasked.GetComponent<EnemyController>().flipable = true;
        lilyMasked.GetComponent<EnemyController>().backMove = false;
        lilyMasked.GetComponent<EnemyController>().speed = 150f;
        lilyMasked.GetComponent<EnemyController>().move = 0;
    }
}
