using UnityEngine;
using System.Collections;

public class CameraButton : MonoBehaviour {

    static public bool isCam;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        isCam = true;
        //Debug.Log("Button is Click!!!");
    }
}
