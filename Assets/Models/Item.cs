using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int id, cost;
    public string itemName, itemDescription;
    public Texture icon;
    public AudioSource useSound;
    public Text nameText, costText;
    public PlayerStat stat;

    private void Start()
    {
        InitItem();
    }

    private void InitItem()
    {
        useSound = gameObject.GetComponent<AudioSource>();
        switch (this.id)
        {
            case 0://neu item id = 0 la khong co item nao
                this.itemName = "";
                this.itemDescription = "";
                this.cost = 0;
                break;
            case 1:
                this.itemName = "Hp Up";
                this.itemDescription = "Increase health";
                this.cost = 500;
                break;
            case 2:
                this.itemName = "Knife Up";
                this.itemDescription = "Increase knife damage";
                this.cost = 350;
                break;
            case 3:
                this.itemName = "Pistol Up";
                this.itemDescription = "Increase pistol damage";
                this.cost = 1000;
                break;
            case 4:
                this.itemName = "Shotgun Up";
                this.itemDescription = "Increase number of shotgun bullet";
                this.cost = 2500;
                break;
            case 5:
                this.itemName = "Strength Up";
                this.itemDescription = "increase strength";
                this.cost = 3000;
                break;
            case 6:
                this.itemName = "Intelligent Up";
                this.itemDescription = "increase intelligent";
                this.cost = 3000;
                break;
            case 7:
                this.itemName = "Dexterity Up";
                this.itemDescription = "increase dexterity";
                this.cost = 3000;
                break;
            case 8:
                this.itemName = "Stamina Up";
                this.itemDescription = "increase stamina";
                this.cost = 3000;
                break;
        }
        nameText.text = this.itemName;
        costText.text = this.cost.ToString();
        this.GetComponentInChildren<RawImage>().texture = icon;
    }

    public void OnPointEnter()
    {
        Text tipText = GameObject.FindGameObjectWithTag("tip").GetComponent<Text>();
        tipText.text = this.itemDescription;
    }

    public void OnPointExit()
    {
        Text tipText = GameObject.FindGameObjectWithTag("tip").GetComponent<Text>();
        tipText.text = "";
    }

    public void OnUseItem()
    {
        stat = GameObject.FindGameObjectWithTag("Model").GetComponent<PlayerStat>();
        if (stat.Cash >= this.cost)
        {
            useSound.Play();
            //Dung item
            switch (this.id)
            {
                case 1:
                    stat.AddStat(6, 10);
                    break;
                case 2:
                    stat.AddStat(7, 1);
                    break;
                case 3:
                    stat.AddStat(8, 1);
                    break;
                case 4:
                    stat.AddStat(9, 1);
                    break;
                case 5:
                    stat.AddStat(0, 1);
                    break;
                case 6:
                    stat.AddStat(1, 1);
                    break;
                case 7:
                    stat.AddStat(2, 1);
                    break;
                case 8:
                    stat.AddStat(3, 10);
                    break;
            }
            stat.AddStat(4, -this.cost);
        }
    }
}
