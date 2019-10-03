using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyToBattle : MonoBehaviour
{
    //Script duoc su dung boi ReadyText
    [SerializeField]
    private Text readyText;
    [SerializeField]
    THGameController thgc;
    [SerializeField]
    private float timeDelay = 2.5f;
    [SerializeField]
    private GameObject readyPanel;

    private void Start()
    {
        readyText = gameObject.GetComponent<Text>();
        thgc.thBattle = false;
        StartCoroutine(ReadyToBossFight());
    }

    IEnumerator ReadyToBossFight()
    {
        yield return new WaitForSeconds(timeDelay);
        readyText.text = "Fight!";
        yield return new WaitForSeconds(0.3f);
        thgc.thBattle = true;
        thgc.gc.manHinh.whiteScreen.SetActive(true);
        thgc.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
        readyPanel.SetActive(false);
        thgc.gc.manHinh.whiteScreen.SetActive(false);
    }
}
