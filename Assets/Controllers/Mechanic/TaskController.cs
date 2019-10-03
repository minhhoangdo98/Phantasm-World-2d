using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour
{
    //Script duoc dung boi TaskController
    public InputField tenCV;
    public Dropdown chiSo;
    public Toggle tog;//prefab hien toggle
    public int demCV = 0;//dem cong viec
    [SerializeField]
    private GameObject taskViewContent;

    public void NutXacNhan()//khi bam vao nut xac nhan
    {
        if (tenCV.text != "")
        {
            demCV = PlayerPrefs.GetInt("demCV" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());//lay so luong cong viec
            demCV++;//tang them 1
            PlayerPrefs.SetInt("demCV" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), demCV);//Luu lai so luong cong viec
            string tcv = tenCV.text.ToString();
            TaoCheckBoxCV(demCV, tcv, chiSo.value, 0, 0);//tao toggle hien cong viec do
            tenCV.text = "";
        }
    }

    private void TaoCheckBoxCV(int id, string tenCV, int chiSo, int completed, int daCong)//Ham toggle o goc trai man hinh
    {
        Toggle togTask = Instantiate(tog, taskViewContent.transform);
        ThemCongViec(togTask, id, tenCV, chiSo, completed, daCong);//Them cong viec vao script Tasks cua toggle duoc tao
        if (togTask.GetComponent<Tasks>().Completed == 1)//Neu cong viec nay da hoan thanh thi danh dau hoan vao o toggle va khong cho bo dau do di
        {
            togTask.GetComponent<Toggle>().isOn = true;
            togTask.GetComponent<Toggle>().interactable = false;
        }
    }

    private void ThemCongViec(Toggle togTask, int demCV, string tenCV, int chiSo, int completed, int daCong)//Ham them cong viec vao script Tasks cua toggle
    {
        togTask.GetComponent<Tasks>().Id = demCV;
        togTask.GetComponent<Tasks>().T = tenCV;
        togTask.GetComponent<Tasks>().StatBonus = chiSo;
        togTask.GetComponent<Tasks>().Completed = completed;
        togTask.GetComponent<Tasks>().daCong = daCong;
        togTask.GetComponentInChildren<Text>().text = togTask.GetComponent<Tasks>().T;
        SaveCongViec(togTask, demCV);//Luu cong viec voi du lieu la toggle vua duoc tao, va id la demCV
    }

    private void SaveCongViec(Toggle togTask, int demCV)//Luu cac chi so cua cong viec vua duoc tao voi id dua vao
    {
        PlayerPrefs.SetInt("id" + demCV.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), togTask.GetComponent<Tasks>().Id);
        PlayerPrefs.SetString("tenCV" + demCV.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), togTask.GetComponent<Tasks>().T);
        PlayerPrefs.SetInt("chiSo" + demCV.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), togTask.GetComponent<Tasks>().StatBonus);
        PlayerPrefs.SetInt("completed" + demCV.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), togTask.GetComponent<Tasks>().Completed);
        PlayerPrefs.SetInt("daCong" + demCV.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), togTask.GetComponent<Tasks>().daCong);
    }

    public void LoadCongViec()//ham load cong viec duoc goi khi bat dau game
    {
        XoaTatCaTheoTag("Task");//xoa tat ca cong viec dang hien tren man hinh (neu co)
        for (int i = 1; i <= PlayerPrefs.GetInt("demCV" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()); i++)//Chay vong for de hien thi cac cong viec voi demCV la so luong cong viec da duoc tao
        {
            string tenCV = PlayerPrefs.GetString("tenCV" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
            int chiSo = PlayerPrefs.GetInt("chiSo" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
            int completed = PlayerPrefs.GetInt("completed" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
            int daCong = PlayerPrefs.GetInt("daCong" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
            TaoCheckBoxCV(i, tenCV, chiSo, completed, daCong);//Tao toggle cho cac cong viec nay          
        }
    }

    public void XoaTheoId(int idTask)
    {
        XoaTatCaTheoTag("Task");//xoa tat ca cong viec dang hien tren man hinh (neu co)

        for (int i = 1; i <= PlayerPrefs.GetInt("demCV" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()) - 1; i++)//Chay vong for de hien thi cac cong viec voi demCV la so luong cong viec da duoc tao
        {
            if (i >= idTask)
            {
                string tenCV = PlayerPrefs.GetString("tenCV" + (i + 1).ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
                int chiSo = PlayerPrefs.GetInt("chiSo" + (i + 1).ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
                int completed = PlayerPrefs.GetInt("completed" + (i + 1).ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
                int daCong = PlayerPrefs.GetInt("daCong" + (i + 1).ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
                TaoCheckBoxCV(i, tenCV, chiSo, completed, daCong);
            }
            else
            {
                string tenCV = PlayerPrefs.GetString("tenCV" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
                int chiSo = PlayerPrefs.GetInt("chiSo" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
                int completed = PlayerPrefs.GetInt("completed" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
                int daCong = PlayerPrefs.GetInt("daCong" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
                TaoCheckBoxCV(i, tenCV, chiSo, completed, daCong);
            }
        }
        demCV--;
        PlayerPrefs.SetInt("demCV" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), demCV);
        LoadCongViec();
    }

    public void XoaToanBoCV()
    {
            demCV = 0;
            PlayerPrefs.SetInt("demCV" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), demCV);
            XoaTatCaTheoTag("Task");
    }

    private void XoaTatCaTheoTag(string tag)
    {
        GameObject[] gowTag = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < gowTag.Length; i++)
        {
            Destroy(gowTag[i]);
        }
    }
}
