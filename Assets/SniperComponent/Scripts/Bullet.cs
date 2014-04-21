using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{

    public GameObject ParticleHit;// 子弹打中的粒子系统.
    public bool LookTargetHit; // 如果不想操作相机设置为false.
    public GameObject Owner;//所有player.

    public UILabel  CriticalStrike;
    public GameObject  parent;

    private GameObject objcameraLookat;
    private bool hited;//是否命中.
    private int raylength = 50;
    private bool cameraLook;
    public int Damage = 100;

	// Use this for initialization
	void Start () {

        RayShoot();
	}
	
	// Update is called once per frame
	void Update () {
        if (!hited)
        {
            RayShoot();
            CameraLookAtTarget();
        }

        //移动子弹.
        if (this.rigidbody)
            transform.forward = Vector3.Normalize(this.rigidbody.velocity);
	}

    public void SetUp(GameObject owner)
    {
        Owner = owner;
    }

    void HitTarget(Collider collision)
    {
        // 如果是生命体计算伤害.
        if (collision.gameObject.GetComponent<DamageManager>())
        {
            collision.gameObject.GetComponent<DamageManager>().ApplyDamage(Damage, this.transform.rigidbody.velocity);
        }
    }

    //发射.
    void RayShoot()
    {

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 20);//发射一条射线检测碰撞体,返回一个数组.

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider)//碰到物体为非空.
            {

                if (hit.collider.tag != "Player" && hit.collider.tag != "Bullet")
                {
                    GameObject hitparticle = Instantiate(ParticleHit, hit.point, hit.transform.rotation) as GameObject;
                    hitparticle.transform.rotation = Quaternion.FromToRotation(hitparticle.transform.up, transform.forward) * hitparticle.transform.rotation;//计算粒子方向.
                    GameObject.Destroy(hitparticle, 5);//加载5秒后销毁粒子.

                    HitTarget(hit.collider);// 计算伤害.
                    if (hit.rigidbody)
                    {
                        //添加一个力（目标，本身）.
                        hit.rigidbody.AddForceAtPosition((100 + this.rigidbody.velocity.magnitude) * hit.normal, hit.point);
                    }
                    hited = true;
                }
            }
        }

        if (hited)
        {
            GameObject.Destroy(this.gameObject);//如果击中销毁这个子弹.
        }
    }

    UILabel label;
    bool bUi = true;
    void CameraLookAtTarget()//摄像机指向被击中生命体。.
    {

        RaycastHit camerahits;
        if (Physics.Raycast(transform.position, transform.forward, out camerahits, 1000))
        {
            RaycastHit hitcam = camerahits;
            if (hitcam.collider)
            {
                if (hitcam.collider.gameObject.GetComponent<DamageManager>())
                {
                    if (hitcam.collider.gameObject.name == "Head"&&bUi )
                    {
                        label = Instantiate(CriticalStrike, Vector3.zero, Quaternion.identity) as UILabel ;
                        label.transform.parent = GameObject.Find("UI Root").transform;
                        CriticalStrike.text  = "CriticalStrike";
                        //CriticalStrike.transform.localScale = new Vector3(1, 1, 1);
                      //  print(CriticalStrike.transform .localScale  );
                        //特写镜头
                        //if (!cameraLook)
                        //{
                        //    cameraLook = true;
                        //    if (GameObject.Find("CamaraBullet") && LookTargetHit)
                        //    {
                        //        if (Owner)
                        //        {
                        //            Owner.GetComponent<GunHanddle>().SeeBulletRun();
                        //        }
                        //        objcameraLookat = hitcam.collider.gameObject;
                        //        //计算摄像机的位置，指向被击中的物体.
                        //        GameObject.Find("CamaraBullet").GetComponent<CameraBullet>().SetUp(objcameraLookat, this.transform, hitcam.point);
                        //    }
                        //}

                        bUi = false;
                        
                    }
                   // print(CriticalStrike.transform.localScale);
                   
                }
            }
        }
    }
}


