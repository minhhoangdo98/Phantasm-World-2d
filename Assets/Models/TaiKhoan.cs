using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaiKhoan : MonoBehaviour
{
    private int idTK;
    private string tenNV, tenTK, pass;

    public int IdTK { get => idTK; set => idTK = value; }
    public string TenNV { get => tenNV; set => tenNV = value; }
    public string TenTK { get => tenTK; set => tenTK = value; }
    public string Pass { get => pass; set => pass = value; }

    public void LayThongTinTaiKhoan(int id)
    {
        this.IdTK = PlayerPrefs.GetInt("idTK" + id.ToString());
        this.TenNV = PlayerPrefs.GetString("ten" + id.ToString());
        this.TenTK = PlayerPrefs.GetString("tenDN" + id.ToString());
        this.Pass = PlayerPrefs.GetString("pass" + id.ToString());
    }
}
