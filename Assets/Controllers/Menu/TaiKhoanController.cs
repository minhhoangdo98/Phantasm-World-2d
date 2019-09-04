using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaiKhoanController : MonoBehaviour
{
    private int demTK = 0;
    private TaiKhoan tk;
    public InputField tenNV, tenDNT, passT, tenDNL, passL;
    public GameObject MBPanel;
    void Start()
    {
        tk = gameObject.GetComponent<TaiKhoan>();
        demTK = PlayerPrefs.GetInt("demTK");
    }

    public void DangKy()
    {
        demTK++;
        PlayerPrefs.SetInt("demTK", demTK);
        ThemTaiKhoan(demTK, tenNV.text, tenDNT.text, passT.text);
        MBPanel.GetComponent<MessBox>().textThongBao.text = "Successful registration please login again!";
        MBPanel.SetActive(true);
    }

    private void ThemTaiKhoan(int demTk, string ten, string tenDN, string pass)
    {
        PlayerPrefs.SetInt("idTK" + demTK.ToString(), demTK);
        PlayerPrefs.SetString("ten" + demTK.ToString(), ten);
        PlayerPrefs.SetString("tenDN" + demTK.ToString(), tenDN);
        PlayerPrefs.SetString("pass" + demTK.ToString(), pass);
    }

    public void DangNhap()
    {
        for (int i = 1; i <= demTK; i++)
        {
            tk.LayThongTinTaiKhoan(i);
            if (tk.TenTK == tenDNL.text && tk.Pass == passL.text)
            {
                PlayerPrefs.SetInt("idTKCurrent", i);
                PlayerPrefs.SetString("tenCurrent", tk.TenNV);
                SceneManager.LoadScene(2);
                return;
            }
        }
        MBPanel.GetComponent<MessBox>().textThongBao.text = "Wrong username or password!";
        MBPanel.SetActive(true);
    }

    public void XoaToanBoTaiKhoan()
    {
        for (int i = 1; i <= demTK; i++)
        {
            PlayerPrefs.SetInt("saved" + "tk" + i.ToString(), 0);
            PlayerPrefs.SetInt("story" + "tk" + i.ToString(), 0);
        }
        demTK = 0;
        PlayerPrefs.SetInt("demTK", demTK);
        PlayerPrefs.SetInt("demCV", 0);
        PlayerPrefs.SetInt("demCH", 0);
        MBPanel.GetComponent<MessBox>().textThongBao.text = "Deleted! ";
        MBPanel.SetActive(true);
    }
}
