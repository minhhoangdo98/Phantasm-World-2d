using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    private int str = 1, dex = 1, intl = 1, sta = 1, currentHp = 100, maxHp = 100, cash = 0, pistolDam = 1, shotgunDam = 1, knifeDam = 1;//strength, dex, intelligent, stamina,...
    private string ten = "";//ten nhan vat do nguoi choi nhap vao
    public int saved = 0;//bien dung de nhan biet da co file luu hay chua
    public Text tenText, strText, intlText, dexText, staText, HpText, cashText;

    public int Str { get => str; set => str = value; }
    public int Dex { get => dex; set => dex = value; }
    public int Intl { get => intl; set => intl = value; }
    public int Sta { get => sta; set => sta = value; }
    public int CurrentHp { get => currentHp; set => currentHp = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Cash { get => cash; set => cash = value; }
    public string Ten { get => ten; set => ten = value; }
    public int PistolDam { get => pistolDam; set => pistolDam = value; }
    public int ShotgunDam { get => shotgunDam; set => shotgunDam = value; }
    public int KnifeDam { get => knifeDam; set => knifeDam = value; }

    private void Start()
    {
        Ten = PlayerPrefs.GetString("tenCurrent");
    }

    public void SaveStat(int idtk)
    {
        saved = 1;
        PlayerPrefs.SetInt("str" + "tk" + idtk.ToString(), Str);
        PlayerPrefs.SetInt("intl" + "tk" + idtk.ToString(), Intl);
        PlayerPrefs.SetInt("dex" + "tk" + idtk.ToString(), Dex);
        PlayerPrefs.SetInt("sta" + "tk" + idtk.ToString(), Sta);
        PlayerPrefs.SetInt("cash" + "tk" + idtk.ToString(), Cash);
        PlayerPrefs.SetInt("saved" + "tk" + idtk.ToString(), saved);
        PlayerPrefs.SetInt("currentHp" + "tk" + idtk.ToString(), CurrentHp);
        PlayerPrefs.SetInt("maxHp" + "tk" + idtk.ToString(), MaxHp);
        PlayerPrefs.SetInt("knifeDam" + "tk" + idtk.ToString(), KnifeDam);
        PlayerPrefs.SetInt("pistolDam" + "tk" + idtk.ToString(), PistolDam);
        PlayerPrefs.SetInt("shotgunDam" + "tk" + idtk.ToString(), ShotgunDam);
    }

    public void LoadStat(int idtk)
    {
        Str = PlayerPrefs.GetInt("str" + "tk" + idtk.ToString());
        Intl = PlayerPrefs.GetInt("intl" + "tk" + idtk.ToString());
        Dex = PlayerPrefs.GetInt("dex" + "tk" + idtk.ToString());
        Sta = PlayerPrefs.GetInt("sta" + "tk" + idtk.ToString());
        cash = PlayerPrefs.GetInt("cash" + "tk" + idtk.ToString());
        CurrentHp = PlayerPrefs.GetInt("currentHp" + "tk" + idtk.ToString());
        MaxHp = PlayerPrefs.GetInt("maxHp" + "tk" + idtk.ToString());
        saved = PlayerPrefs.GetInt("saved" + "tk" + idtk.ToString());
        KnifeDam = PlayerPrefs.GetInt("knifeDam" + "tk" + idtk.ToString());
        PistolDam = PlayerPrefs.GetInt("pistolDam" + "tk" + idtk.ToString());
        ShotgunDam = PlayerPrefs.GetInt("shotgunDam" + "tk" + idtk.ToString());
        if (saved == 0)//Neu chua co file luu (Lan dau choi)
        {
            Str = 1;
            Intl = 1;
            Dex = 1;
            Sta = 1;
            cash = 1;
            CurrentHp = 100;
            MaxHp = 100;
            KnifeDam = 1;
            PistolDam = 1;
            ShotgunDam = 1;
            SaveStat(PlayerPrefs.GetInt("idTKCurrent"));
        }
    }

    public void AddStat(int statBonus, int num)//them stat dua theo lua chon stat nhap vao
    {
        switch (statBonus)
        {
            case 0://neu statBonus=0 tuc la them cho Suc khoe
                Str += num;
                break;
            case 1://neu statBonus=1 tuc la them cho Tri tue
                Intl += num;
                break;
            case 2://neu statBonus=2 tuc la them cho dex
                Dex += num;
                break;
            case 3:
                Sta += num;
                break;
            case 4:
                Cash += num;
                break;
            case 5:
                CurrentHp += num;
                if (CurrentHp > MaxHp)
                    CurrentHp = MaxHp;
                break;
            case 6:
                CurrentHp += num;
                MaxHp += num;
                break;
            case 7:
                KnifeDam += num;
                break;
            case 8:
                PistolDam += num;
                break;
            case 9:
                ShotgunDam += num;
                break;
        }
        SaveStat(PlayerPrefs.GetInt("idTKCurrent"));//Sau khi them thi luu lai
    }

    public void ShowCharInfo()
    {
        tenText.text = ten;
        strText.text = Str.ToString();
        intlText.text = Intl.ToString();
        dexText.text = Dex.ToString();
        staText.text = Sta.ToString();
        HpText.text = currentHp.ToString() + "/" + maxHp.ToString();
        cashText.text = Cash.ToString();
    }
}
