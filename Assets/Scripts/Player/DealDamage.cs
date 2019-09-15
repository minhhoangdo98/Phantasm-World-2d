using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public GameObject attackEffect;
    public int damageAmount;
    void Start()
    {
        damageAmount = (int)PlayerPrefs.GetInt("str" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()) * 2;//Sat thuong mac dinh
    }

    #region Gay sat thuong cho ke dich
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))//neu la ke dich
        {
            Vector3 pos = new Vector3(collision.transform.position.x, collision.transform.position.y);
            GameObject ae = Instantiate(attackEffect, pos, Quaternion.identity, collision.transform) as GameObject;//tao hieu ung danh trung ke dich
            Destroy(ae, 0.5f);
            collision.SendMessage("TakeDamage", damageAmount, SendMessageOptions.DontRequireReceiver);//gay sat thuong cho ke dich
        }
    }

    #endregion
}
