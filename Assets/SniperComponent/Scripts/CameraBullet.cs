using UnityEngine;
using System.Collections;

public class CameraBullet : MonoBehaviour {

    public Transform Bullet;
    public GameObject Target;

    public void SetUp(GameObject victim, Transform bullet, Vector3 hitpoint)
    {
        if (victim)
        {
            Bullet = bullet;
            Target = victim;
            //计算摄像机的位置.
            transform.position = victim.transform.position + new Vector3(Random.Range(-25, 25), Random.Range(2, 5), Random.Range(-25, 25));
            transform.LookAt(Target.transform.position);//指向被击中的物体.
        }
    }

    void View(GameObject victim, Transform bullet, Vector3 hitpoint)
    {
        if (victim)
        {
            Target = victim;
            transform.LookAt(Target.transform.position);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Target)
            transform.LookAt(Target.transform.position);

    }
}
