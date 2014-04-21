using UnityEngine;
using System.Collections;

[AddComponentMenu ("Camera-Control/Mouse Look")]

public class MouseLooking : MonoBehaviour {
    //灵敏度.
    public float sensitivityX = 15;
    public float sensitivityY = 15;
    //xy方向大最大最小旋转值.
    public float minimumX = -360;
    public float maximumX = 360;
    public float minimumY = -60;
    public float maximumY = 60;

    public float delayMouse = 3;//鼠标延时.

    //降噪.
    public float noiseX = 0.1f;
    public float noiseY = 0.1f;
    public bool Noise;
    //呼吸影响。
    public float breathHolderVal = 1;
    

    private float rotationX = 0;
    private float rotationY = 0;
    private float rotationXtemp = 0;
    private float rotationYtemp = 0;
    private Quaternion originalRotation;
    private float noisedeltaX;
    private float noisedeltaY;
    private float stunY;

    public UISlider mySlider;
    float sliderNum;
    static public bool isMoveCam = true;
	// Use this for initialization
	void Start () {
        if (rigidbody)
            rigidbody.freezeRotation = true;//锁定旋转不被其他地方修改.
        originalRotation = transform.localRotation;//把player的角度给相机.

        sliderNum = mySlider.value;
	}
	
	// Update is called once per frame
	void Update () {

        sensitivityY = sensitivityX;
        Screen.lockCursor = true;//隐藏光标.

        stunY += (0 - stunY) / 20f;

        sliderNum = mySlider.value;

        if (Noise)
        {
            //计算浮动值.
            noisedeltaX += ((((Mathf.Cos(Time.time) * Random.Range(-10, 10) / 5f) * noiseX) - noisedeltaX) / 100) * breathHolderVal;
            noisedeltaY += ((((Mathf.Sin(Time.time) * Random.Range(-10, 10) / 5f) * noiseY) - noisedeltaY) / 100) * breathHolderVal;
        }
        else
        {

            noisedeltaX = 0;
            noisedeltaY = 0;
        }

        Vector2 v = Vector2.zero;
        if (Input.touchCount == 1 &&isMoveCam )
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                print(Input.GetTouch(0).deltaPosition);
                v = Input.GetTouch(0).deltaPosition;
                //rotationXtemp += (Input.GetAxis("Mouse X") * sensitivityX) + noisedeltaX;
                //rotationYtemp += (Input.GetAxis("Mouse Y") * sensitivityY) + noisedeltaY;

              
            }
            else
            {
                v = Vector2.zero;
            }

        }


        rotationXtemp += (v.x / (12 - 7 * sliderNum) * sensitivityX) + noisedeltaX;
        rotationYtemp += (v.y / (12 - 7 * sliderNum) * sensitivityY) + noisedeltaY;
        rotationX += (rotationXtemp - rotationX) / delayMouse;//计算x延时.
        //x方向的角度控制在360到-360之间.
        if (rotationX >= 360)
        {
            rotationX = 0;
            rotationXtemp = 0;
        }
        if (rotationX <= -360)
        {
            rotationX = 0;
            rotationXtemp = 0;
        }

        rotationY += (rotationYtemp - rotationY) / delayMouse;//计算y延时.
        
        //print(Input.mousePosition .x);
        ////计算xy方向的浮动值.
        //rotationXtemp += (Input.GetAxis("Mouse X") * sensitivityX) + noisedeltaX;
        //rotationYtemp += (Input.GetAxis("Mouse Y") * sensitivityY) + noisedeltaY;
       

       
        //得到旋转角度.
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);


        //计算旋转.
        var xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        var yQuaternion = Quaternion.AngleAxis(rotationY + stunY, Vector3.left);

        //得到旋转值.
        transform.localRotation = originalRotation * xQuaternion * yQuaternion;
	
	}

    //射击震动.
    public void Stun(float val)
    {
        stunY = val;
    }

    //控制角度.
    static float ClampAngle(float angle, float min, float max)
    {

        if (angle < -360.0f)
            angle += 360.0f;

        if (angle > 360.0f)
            angle -= 360.0f;

        return Mathf.Clamp(angle, min, max);

    }
}
