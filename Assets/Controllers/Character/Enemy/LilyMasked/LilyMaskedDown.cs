using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyMaskedDown : MonoBehaviour
{
    [SerializeField]
    private GameObject lilyMasked, fireEffect;
    [SerializeField]
    private bool actionDown1 = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && actionDown1 && lilyMasked.GetComponent<EnemyController>().damagable && lilyMasked.GetComponent<EnemyController>().hoatDong)
        {
            StartCoroutine(kickDown());
        }
    }

    IEnumerator kickDown()
    {
        actionDown1 = false;
        Vector3 pos = new Vector3(lilyMasked.transform.position.x, lilyMasked.transform.position.y - 0.3f);
        GameObject fire = Instantiate(fireEffect, pos, Quaternion.identity, lilyMasked.transform) as GameObject;
        Destroy(fire, 0.5f);
        lilyMasked.GetComponent<EnemyController>().r2.AddForce(-Vector2.up * lilyMasked.GetComponent<EnemyController>().jumpPow * 2);
        yield return new WaitForSeconds(0.3f);
        lilyMasked.GetComponent<EnemyController>().r2.AddForce(Vector2.up * lilyMasked.GetComponent<EnemyController>().jumpPow);
        yield return new WaitForSeconds(1);
        actionDown1 = true;
    }
}
