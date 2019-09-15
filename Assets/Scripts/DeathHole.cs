using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.SendMessage("TakeDamage", 99999, SendMessageOptions.DontRequireReceiver);
    }
}
