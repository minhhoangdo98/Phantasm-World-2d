using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public Lily lily;
    public Gideon gideon;
    public Ruth ruth;
    public Main main;
    public GameObject face;

    private void Start()
    {
        lily = gameObject.GetComponent<Lily>();
        gideon = gameObject.GetComponent<Gideon>();
        ruth = gameObject.GetComponent<Ruth>();
        main = gameObject.GetComponent<Main>();
    }

    public void ThayDoiAvatar(Texture faceImage)
    {
        face.GetComponent<RawImage>().texture = faceImage;
    }
}
