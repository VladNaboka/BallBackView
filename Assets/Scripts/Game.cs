using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Game : MonoBehaviour
{
    public PixelPerfectCamera Cam;
    public GameObject Loading;
    public GameObject GameO;
    private static Game _instance;
    public static Game Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
        Cam.assetsPPU = (int)((float)Cam.assetsPPU / 1080 * Screen.width);
    }

    public void StartGame()
    {
        Loading.SetActive(false);
        GameO.SetActive(true);
    }
}
