using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public Transform Myself;
    public int Speed = 3;
    public AudioClip[] footstepSound;
    public Vector3 targetPosition;//目的位置.
    public int timethink = 0;

    NavMeshAgent man;
    GameObject[] target1;
    GameObject[] target2;
    int i1,i2;
    bool initPosion = true;
    // Use this for initialization
    IEnumerator Start()
    {
        man = this.gameObject.GetComponent<NavMeshAgent>();
        target1 = GameObject.FindGameObjectsWithTag("Target1");
        target2 = GameObject.FindGameObjectsWithTag("Target2");
        i1 = Random.Range(0, target1.Length-1);
        man.SetDestination(target1[i1].transform.position);

        yield return new WaitForSeconds(Random.Range(0, 3.0f));
        Myself.animation.CrossFade("Run", 0.3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (man.remainingDistance < 3)
        {
            print("111111111111111111111");
            man.SetDestination(target2[Random.Range(target2.Length-1, 0)].transform.position);
        }

        //类似play，混合淡入淡出.
        Myself.animation.CrossFade("Run", 0.3f);

        //if (timethink <= 0)
        //{
        //    targetPosition = new Vector3(Random.Range(-200, 200), 0, Random.Range(-200, 200));
        //    timethink = Random.Range(100, 500);
        //}
        //else
        //{
        //    timethink -= 1;
        //}

        //targetPosition.y = transform.position.y;
        //transform.LookAt(targetPosition);//转向这个方向.
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * Speed);//移动到这个位置.
        //if (initPosion)
        //{
        //    target1 = GameObject.FindGameObjectsWithTag("Target1");
        //    i1 = Random.Range(0, target1.Length-1);

        //    if (target1 != null)
        //    {
        //        man.SetDestination(target1[i1].transform.position);
        //        initPosion = false;

        //    }
        //}


        

        

        
    }

    void LateUpdate()
    {
        
    }
}
