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

    public void ThayDoiAvatar(Texture faceImage)
    {
        face.GetComponent<RawImage>().texture = faceImage;
    }
}
