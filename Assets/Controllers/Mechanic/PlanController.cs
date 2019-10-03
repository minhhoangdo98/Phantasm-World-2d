using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanController : MonoBehaviour
{
    [SerializeField]
    private Dropdown chiSo, dayDropdown, monthDropdown, yearDropdown;
    public InputField tenPlan;
    public int demPlan = 0;
    [SerializeField]
    private GameController gc;

    private void Start()
    {
        InitDayDropDown();
        InitYearDropdown();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void InitDayDropDown()//Khoi tao dropdown ngay theo thang
    {
        dayDropdown.ClearOptions();
        List<string> dayString = new List<string>();
        switch (monthDropdown.value)//xac dinh thang co 30 ngay va thang co 31 ngay
        {
            case 0:
            case 2:
            case 4:
            case 6:
            case 7:
            case 9:
            case 11:
                for (int i = 1; i < 32; i++)
                    dayString.Add(i.ToString());
                dayDropdown.AddOptions(dayString);
                break;
            case 1:
            case 3:
            case 5:
            case 8:
            case 10:
                for (int i = 1; i < 31; i++)
                    dayString.Add(i.ToString());
                dayDropdown.AddOptions(dayString);
                break;
        }
    }

    private void InitYearDropdown()//Khoi tao namg
    {
        yearDropdown.ClearOptions();
        int i = 0;
        List<string> yearString = new List<string>();
        while (i < 20)//lay 20 nam tinh tu nam hien tai
        {
            int t = DateTime.Now.Year + i;
            yearString.Add(t.ToString());
            i++;
        }
        yearDropdown.AddOptions(yearString);
    }

    public void NutAddPlan()//khi bam vao nut xac nhan
    {
        if (tenPlan.text != "")
        {
            demPlan = PlayerPrefs.GetInt("demPlan" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());//lay so luong cong viec
            demPlan++;//tang them 1
            PlayerPrefs.SetInt("demPlan" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), demPlan);//Luu lai so luong cong viec
            SavePlan(demPlan, tenPlan.text.ToString(), chiSo.value, dayDropdown.value + 1, monthDropdown.value + 1, yearDropdown.value + DateTime.Now.Year, 0, 0);
            tenPlan.text = "";      
        }
    }

    private void SavePlan(int id, string tenPlan, int chiSo, int deadlineDay, int deadlineMonth, int deadlineYear, int planCompleted, int planDaCong)//Luu plan vao vi tri id duoc dua vao
    {
        PlayerPrefs.SetInt("id" + id.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), id);
        PlayerPrefs.SetString("tenCV" + id.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), tenPlan);
        PlayerPrefs.SetInt("chiSo" + id.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), chiSo);
        PlayerPrefs.SetInt("deadlineDay" + id.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), deadlineDay);
        PlayerPrefs.SetInt("deadlineMonth" + id.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), deadlineMonth);
        PlayerPrefs.SetInt("deadlineYear" + id.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), deadlineYear);
        PlayerPrefs.SetInt("planCompleted" + id.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), planCompleted);
        PlayerPrefs.SetInt("planDaCong" + id.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), planDaCong);
        gc.GetSelectedDayInCal();
    }

    public void XoaToanBoPlan()
    {
        demPlan = 0;
        PlayerPrefs.SetInt("demPlan" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), demPlan);
        gc.GetSelectedDayInCal();
    }
}
