using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mathes : MonoBehaviour
{
    public Button[] num = new Button[16];
    public int[] a;
    [SerializeField]
    private GameObject starLight;

    public void KhoiTaoMathGame()
    {
        a = new int[num.Length];//a chua cac so dung vi tri, num.length - 1 loai bo phan tu la button close
        //Gan gia tri ngau nhien cho cac bien ket qua
        a[12] = Random.Range(150, 250);
        a[13] = Random.Range(150, 250);
        a[14] = Random.Range(150, 250);
        a[15] = Random.Range(150, 250);
        int[] dd = new int[500];//ham danh dau phan tu da dung

        //Gan gia tri cac bien ket qua cho mang num (dao vi tri)
        for(int i = 12; i < a.Length; i++)
        {
            int r = Random.Range(12, 16);
            while (dd[a[r]] == -1)//trong khi so a[r] da duoc dung thi random so khac
            {
                r = Random.Range(12, 16);
            }
            num[i].GetComponentInChildren<Text>().text = a[r].ToString();//gan gia cho b
            dd[a[r]] = -1;//danh dau la da dung
        }

        //Gan gia tri cho ham a
        int j = 0;//Chi so index cho cac số hạng
        int k = 0;//chỉ số index cho kết quả

        //Khoi tao cac gia tri mang a
        while (j<12)
        {
            int t = a[k + 12];//chua ket qua
            a[j] = Random.Range(1, 80);//Random so dau tien tren hang
            t -= a[j];//tru so do
            if (k == 3)//kiem tra xem da la ket qua cuoi chua
            {
                //Chua lai 1 o trong (mot bien bang 0)
                a[j + 1] = t;
                a[j + 2] = 0;
            }
            else
            {               
                do
                {
                    a[j + 1] = Random.Range(1, 80);
                } while (a[j + 1] >= t);
                t -= a[j + 1];
                a[j + 2] = t;
            }
            j += 3;
            k++;
        }

        //Gan gia tri cac phan tu a cho cac button num hien thi len man hinh (ngoai tru ket qua da gan)
        for (int i = 0; i < 12; i++)
        {
            int r = Random.Range(0, 12);
            while (dd[r] == -1)//trong khi vi tri r da duoc dung thi random vi tri khac
            {
                r = Random.Range(0, 12);
            }
            num[i].GetComponentInChildren<Text>().text = a[r].ToString();//gan gia tri cho num
            dd[r] = -1;//danh dau vi tri la da dung
            if (a[r] == 0)
            {
                num[i].GetComponentInChildren<Text>().text = "";
            }
        }
    }

    public void DaoViTri(int buttonIndex)//Khi bam chuot thi dao vi tri cua phan tu trong o vua bam vao voi o trong
    {
        //Kiem tra cac o gan ke cua o bam vao
        if (buttonIndex - 1 >= 0)
        {
            //Neu co o trong thi thuc hien doi vi tri
            if (num[buttonIndex - 1].GetComponentInChildren<Text>().text == "")
            {
                num[buttonIndex - 1].GetComponentInChildren<Text>().text = num[buttonIndex].GetComponentInChildren<Text>().text;
                num[buttonIndex].GetComponentInChildren<Text>().text = "";
            }
        }
        if (buttonIndex + 1 <= 11)
        {
            if (num[buttonIndex + 1].GetComponentInChildren<Text>().text == "")
            {
                num[buttonIndex + 1].GetComponentInChildren<Text>().text = num[buttonIndex].GetComponentInChildren<Text>().text;
                num[buttonIndex].GetComponentInChildren<Text>().text = "";
            }
        }
        if (buttonIndex - 3 >= 0)
        {
            if (num[buttonIndex - 3].GetComponentInChildren<Text>().text == "")
            {
                num[buttonIndex - 3].GetComponentInChildren<Text>().text = num[buttonIndex].GetComponentInChildren<Text>().text;
                num[buttonIndex].GetComponentInChildren<Text>().text = "";
            }
        }
        if (buttonIndex + 3 <= 11)
        {
            if (num[buttonIndex + 3].GetComponentInChildren<Text>().text == "")
            {
                num[buttonIndex + 3].GetComponentInChildren<Text>().text = num[buttonIndex].GetComponentInChildren<Text>().text;
                num[buttonIndex].GetComponentInChildren<Text>().text = "";
            }
        }       
    }

    public bool KiemTra()
    {
        int[] t = new int[17]; 
        for (int i = 0; i < num.Length; i++)
        {
            if (num[i].GetComponentInChildren<Text>().text == "")
                t[i] = 0;
            else
                t[i] = IntParseFast(num[i].GetComponentInChildren<Text>().text);
        }
        //Kiem tra tong cac phan tu tren mot hang co bang ket qua hay khong, neu khong bang thi return false
        if ((t[0] + t[1] + t[2]) != t[12])
            return false;
        if ((t[3] + t[4] + t[5]) != t[13])
            return false;
        if ((t[6] + t[7] + t[8]) != t[14])
            return false;
        if ((t[9] + t[10] + t[11]) != t[15])
            return false;
        //sau khi kiem tra het ma khong vao cac truong hop tren thi return true 
        return true;
    }

    public static int IntParseFast(string value)//chuyen doi chuoi string sang kieu so int
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
    }

    private void Update()
    {
        if (KiemTra())//Kiem tra xem neu hoan thanh bang
        {
            Vector3 pos = new Vector3(0, 0, 0);
            GameObject star = Instantiate(starLight, pos, Quaternion.identity) as GameObject;//Tao hieu ung chuc mung
            star.GetComponent<ParticleSystem>().Play();
            Destroy(star, 3);
            gameObject.transform.parent.gameObject.SetActive(false);
        }

    }
}
