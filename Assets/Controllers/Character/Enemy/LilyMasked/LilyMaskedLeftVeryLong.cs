using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyMaskedLeftVeryLong : MonoBehaviour
{
    [SerializeField]
    private GameObject lilyMasked;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            lilyMasked.GetComponent<EnemyController>().move = -1;
        }
           
    } 

}
