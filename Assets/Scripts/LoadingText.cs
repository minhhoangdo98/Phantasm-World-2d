using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    private bool loading = true;
    void Update()
    {
        if (loading)
        {
            StartCoroutine(LoadingTextAnim());
        }
    }

    IEnumerator LoadingTextAnim()
    {
        loading = false;
        gameObject.GetComponent<Text>().text = "Loading";
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Text>().text = "Loading.";
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Text>().text = "Loading..";
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Text>().text = "Loading...";
        yield return new WaitForSeconds(0.5f);
        loading = true;
    }
}
