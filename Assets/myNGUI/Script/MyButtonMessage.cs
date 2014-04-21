using UnityEngine;
using System.Collections;

public class MyButtonMessage : MonoBehaviour {
    static public bool isCam;//切换相机.
    static public bool isray;//射击.
    public string RePlayLevelName = "ZombieScene";
    public GameObject setMe;//设置按钮.
    static public bool QuitNoButton = false;//是否退出No.
    static public bool QuitYesButton = false;
	// Use this for initialization
	void Start () {
        if (setMe != null)
            setMe.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadBullet()
    {
        Gun.bulletCount = 5;
    }

    void LoadLevel()
    {
        Application.LoadLevel("ZombieScene");
    }

    void SwitchCamera()
    {
        isCam = true;
        //Debug.Log("Button is Click!!!");
    }

    void RayShoot()
    {
        isray = true;
    }

    void RePlay()
    {
        Application.LoadLevel(RePlayLevelName);
        Gun.bulletCount = 5;
    }

    void SetPres()
    {
        MouseLooking.isMoveCam = !MouseLooking.isMoveCam;
        setMe.active = !setMe.active;
    }

    void QuitNo()
    {
        QuitNoButton = true;
    }

    void QuitYes()
    {
        QuitYesButton = true;
    }
}
