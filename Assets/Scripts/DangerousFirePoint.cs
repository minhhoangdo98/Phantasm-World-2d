using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousFirePoint : MonoBehaviour
{
    private bool isFire = false;
    [SerializeField]
    private GameObject fireEffect;
    [SerializeField]
    private float delayTime = 2;
    [SerializeField]
    private bool isRotate = false;

    void Update()
    {
        if (!isFire)
            StartCoroutine(fireDangerous());
    }

    IEnumerator fireDangerous()
    {
        isFire = true;
        yield return new WaitForSeconds(delayTime);
        Vector2 pos;
        if (isRotate)
            pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.8f);
        else
            pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.8f);
        GameObject fe = Instantiate(fireEffect, pos, Quaternion.identity, gameObject.transform);
        if (isRotate)
            fe.transform.Rotate(0, 0, transform.rotation.z + 180, Space.Self);
        Destroy(fe, 2);
        yield return new WaitForSeconds(2);
        isFire = false;
    }
}
