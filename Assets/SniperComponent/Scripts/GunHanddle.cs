using UnityEngine;
using System.Collections;

public class GunHanddle : MonoBehaviour {

    public GameObject Guns;
    public GameObject quit;
	// Use this for initialization
	void Start () {

        quit.SetActive (false );
	}
	
	// Update is called once per frame
	void Update () {
        //切换相机.
        //if (Input.GetMouseButtonDown(1))
        //{
        //   Guns.gameObject.GetComponent<Gun>().Zoom();
        //}

        if (MyButtonMessage.isCam)
        {
            Guns.gameObject.GetComponent<Gun>().Zoom();
            MyButtonMessage.isCam = false;
        }

        //发射.
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Guns.gameObject.GetComponent<Gun>().Shoot();
        //}

        if (MyButtonMessage.isray)
        {

            Guns.gameObject.GetComponent<Gun>().Shoot();

            MyButtonMessage.isray = false;
        }

       
        if (Input.GetKeyDown(KeyCode.Escape ))
        {
            quit.active = !quit.active;
            MouseLooking.isMoveCam = !MouseLooking.isMoveCam;
        }

        if (MyButtonMessage.QuitYesButton)
        {
            Application.Quit();
        }
        if (MyButtonMessage.QuitNoButton)
        {
            quit.active = false ;
            MyButtonMessage.QuitNoButton = false;
            MouseLooking.isMoveCam = true ;
        }
	
	}
    //隐藏相机.
    public void SeeBulletRun()
    {
        Guns.gameObject.GetComponent<Gun>().DisableCamera();

    }
}
