using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public GameObject Bullets; // 子弹.
    public GameObject Shell; // 子弹壳.
    public Transform BulletSpawn; // 子弹发射的位置.
    public Transform ShellSpawn; // 子弹壳发出的位置.
    public GameObject[] cameras;// 需要切换的摄像机，准星跟普通相机.
    public float fireRate = 0.2f; // 发射速度.

    //发射的声音.
    public AudioClip SoundGunFire;
    public AudioClip SoundBoltEnd;
    public AudioClip SoundBoltStart;
    public int Force = 50000;

    private bool boltout;
    private int indexcamera;
    private float timefire = 0;
    private int gunState = 0;
    private int ammoin = 1;//弹药.

    int indexcameraCount;//记录相机索引.

    static public int bulletCount = 5;

    public GameObject ZoomTex;
	// Use this for initialization
	void Start () {
        SelectCamera(0);
        ZoomTex.SetActive(false);
	}
	
	// Update is called once per frame
    void Update()
    {
       
        //if (ammoin <= 0)
        //{
        //    if (indexcamera == 1)
        //    {
        //        GameObject.Find("Howto2").guiText.text = "Right Click to Reload";//提示加载弹药.
        //    }
        //}
        //else
        //{
        //    GameObject.Find("Howto2").guiText.text = "";
        //}

        if (bulletCount <=0)
        {
            ZoomTex.SetActive(false);
        }

        switch (gunState)
        {
            case 0://上弹.
                if (ammoin <= 0)
                {
                    StartCoroutine(StartSleepTime(1f));

                    if (indexcamera != 1 && bulletCount > 0)//普通相机.
                    {
                        animation.CrossFade("Bolt");//拉动上弹动作.
                        gunState = 2;//播放上弹动作.
                        if (SoundBoltStart)
                        {
                            AudioSource.PlayClipAtPoint(SoundBoltStart, this.transform.position);
                        }
                    }
                }
               

                break;
            case 1:
                if (animation["Shoot"].time >= animation["Shoot"].length * 0.8f)//射击动作.
                {
                    gunState = 0;
                }
                gunState = 0;
                break;
            case 2:

                if (animation["Bolt"].time >= animation["Bolt"].length * 0.3f)//超出这个动画时间执行上弹动作.
                {
                    if (Shell && ShellSpawn)
                    {
                        if (!boltout)//子弹壳是否发出.
                        {
                            //实例化一个子弹壳.
                            GameObject shell = Instantiate(Shell, ShellSpawn.position, ShellSpawn.rotation) as GameObject;
                            shell.rigidbody.AddForce(transform.right * 400);//添加一个力.
                            shell.rigidbody.AddRelativeTorque(Vector3.up * 1000);//添加一个力矩.
                            GameObject.Destroy(shell, 5);
                            boltout = true;
                        }
                    }
                }

                if (animation["Bolt"].time >= animation["Bolt"].length * 0.8f)//播放完这一动画回到初始动作.
                {
                    gunState = 0;
                    ammoin = 1;

                    animation.CrossFade("Idle");
                    if (SoundBoltEnd)
                    {
                        AudioSource.PlayClipAtPoint(SoundBoltEnd, this.transform.position);
                    }

                    StartCoroutine(EndSleepTime(0.5f));
                    
                }

                
                break;
        }

       
        //移动相机.
        if (transform.parent.GetComponent<MouseLooking>())
        {
            if (indexcamera == 1)
            {
                transform.parent.GetComponent<MouseLooking>().sensitivityX = 0.5f;
                transform.parent.GetComponent<MouseLooking>().Noise = true;
            }
            else
            {
                transform.parent.GetComponent<MouseLooking>().sensitivityX = 3;
                transform.parent.GetComponent<MouseLooking>().Noise = false;
            }
        }


    }

    IEnumerator StartSleepTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (indexcameraCount == 1)
        {
            ZoomTex.SetActive(false);
            indexcamera = 0;
            SelectCamera(0);
        }

    }

    IEnumerator EndSleepTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (indexcameraCount == 1)
        {
            ZoomTex.SetActive(true);
            indexcamera = 1;
            SelectCamera(1);

        }

       
    }

    public void DisableCamera()//禁用相机.
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].camera)
                cameras[i].camera.gameObject.SetActive(false);
        }
    }

    void SelectCamera(int index)//切换相机.
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == index)
            {
                cameras[i].camera.gameObject.SetActive(true);
               
            }
            else
            {
                cameras[i].camera.gameObject.SetActive(false);
            }
        }
    }


    public void Zoom()//切换相机.
    {
        if (indexcamera != 1)
        {
            indexcamera = 1;
            indexcameraCount = 1;
            gunState = 0;
            ZoomTex.SetActive(true);
        }
        else
        {
            indexcamera = 0;
            indexcameraCount = 0;
            gunState = 0;
            ZoomTex.SetActive(false);
        }
        SelectCamera(indexcamera);
    }

    public void Shoot()
    {


        if (timefire + fireRate < Time.time)//判断是否在可发射的时间内.
        {
            if (bulletCount > 0)
            {

                if (gunState == 0)
                {
                    if (ammoin > 0)
                    {
                        if (transform.parent.GetComponent<MouseLooking>())
                            transform.parent.GetComponent<MouseLooking>().Stun(10);//相机震动.
                        if (SoundGunFire)
                        {
                            AudioSource.PlayClipAtPoint(SoundGunFire, this.transform.position);
                        }

                        if (Bullets && BulletSpawn)//实例化并发射子弹 .
                        {
                            GameObject bullet = Instantiate(Bullets, BulletSpawn.position, BulletSpawn.rotation) as GameObject;
                            bullet.GetComponent<Bullet>().SetUp(this.transform.parent.gameObject);
                            bullet.rigidbody.AddForce(transform.forward * Force);

                        }

                        boltout = false;
                        animation.Play("Shoot");
                        timefire = Time.time;
                        gunState = 1;
                        ammoin -= 1;
                        bulletCount--;

                    }
                }
            }
        }

    }

}
