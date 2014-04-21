using UnityEngine;
using System.Collections;

public class ZoomCamera : MonoBehaviour {

    public UISlider FocalLength;//焦距调机器.
    float FocalLengthValue;
    float FocalLengthMaxValue = 4;//最大调节范围.
    Camera cam;
	// Use this for initialization
	void Start () {

        FocalLengthValue = FocalLength.value;
       
        cam = this.gameObject.GetComponent<Camera>();
        cam.fieldOfView = 10;
	}
	
	// Update is called once per frame
	void Update () {
        FocalLengthValue = FocalLength.value;
        cam.fieldOfView = 10 - (FocalLengthMaxValue * FocalLengthValue);
	}
}
