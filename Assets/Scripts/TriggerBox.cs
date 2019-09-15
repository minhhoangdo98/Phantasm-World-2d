using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    [SerializeField]
    private GameObject gc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gc.GetComponent<GameController>().thgc.thBattle = false;
        gc.GetComponent<GameController>().eve.textNum = 16;
        gc.GetComponent<EventController>().PlayStory();
        Destroy(gameObject);
    }
}
