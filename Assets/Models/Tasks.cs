using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tasks : MonoBehaviour
{
    //Script duoc dung boi cac toggle Task
    private int id;
    private string t;//Ten cong viec
    private int completed;//hoan thanh hay chua 0:chua hoan thanh, 1:da hoan thanh
    private int statBonus;
    public GameObject gc;
    public GameObject parti;
    public int daCong;//cong viec nay sau khi hoan thanh da cong chi so cho nguoi choi hay chua 0:chua, 1:roi

    public int Id { get => id; set => id = value; }
    public string T { get => t; set => t = value; }
    public int StatBonus { get => statBonus; set => statBonus = value; }
    public int Completed { get => completed; set => completed = value; }

    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
        if (Completed == 1)
        {
            gameObject.GetComponent<Toggle>().isOn = true;
            gameObject.GetComponent<Toggle>().interactable = false;
        }
    }

    public void CheckedClick()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            Completed = 1;
            gameObject.GetComponent<Toggle>().interactable = false;
            PlayerPrefs.SetInt("completed" + Id.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), 1);
            Vector2 logPos = new Vector2(200, 200 - Id * 40);//vi tri log
            if (daCong == 0)
            {
                daCong = 1;
                PlayerPrefs.SetInt("daCong" + Id.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), 1);
                gc.GetComponent<GameController>().stat.AddStat(statBonus, 1);
                Vector3 pos = new Vector3(0, 0, -4.7f);
                gc.GetComponent<GameController>().TaoHieuUng(parti, 1.5f, pos);

            }
        }
    }

    public void DeleteTask()
    {
        int demCV = PlayerPrefs.GetInt("demCV" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
        if (Id == demCV)
        {
            demCV--;
            PlayerPrefs.SetInt("demCV" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), demCV);
            Destroy(gameObject);
        }
        else
            gc.GetComponent<TaskController>().XoaTheoId(Id);
    }
}
