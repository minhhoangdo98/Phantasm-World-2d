using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyToBoss : MonoBehaviour
{
    //Script duoc su dung boi ReadyText
    [SerializeField]
    private Text readyText;
    [SerializeField]
    THGameController thgc;

    private void Start()
    {
        readyText = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))//Cho nguoi choi bam Enter de bat dau boss fight
        {
            StartCoroutine(ReadyToBossFight());
        }
    }

    IEnumerator ReadyToBossFight()
    {
        readyText.text = "Fight!";
        yield return new WaitForSeconds(0.2f);
        thgc.thBattle = true;
        gameObject.SetActive(false);
    }
}
