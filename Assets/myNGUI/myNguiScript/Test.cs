using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    public UIInput UserName;
    public UIInput Password;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnSubmit()
    {
        Debug.Log("Login with UserName: " + UserName.value + "Password:" + Password.value);
    }
}
