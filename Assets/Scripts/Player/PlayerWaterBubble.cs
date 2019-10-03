using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterBubble : MonoBehaviour
{
    [SerializeField]
    private GameObject playerWater;

    private void Start()
    {
        StartCoroutine(BubbleExplor());
    }

    IEnumerator BubbleExplor()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<AudioSource>().Play();
        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
            GameObject k = Instantiate(playerWater, pos, Quaternion.identity) as GameObject;
            k.transform.Rotate(0, 0, transform.rotation.z + i * -30, Space.Self);
            Destroy(k, 2);
        }       
        Destroy(gameObject);
    }
}
