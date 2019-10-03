using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject mBPanel, menuCanvas, menuList, hoiThoaiPanel;
    public GameObject[] menuButton = new GameObject[20];
    public GameObject[] menuPanel = new GameObject[10];

    public void TatToanBoMidMenu(GameObject exeptMenu)
    {
        for (int i = 0; i < menuPanel.Length; i++)
            if (menuPanel[i] != exeptMenu)
                menuPanel[i].SetActive(false);
    }
}
