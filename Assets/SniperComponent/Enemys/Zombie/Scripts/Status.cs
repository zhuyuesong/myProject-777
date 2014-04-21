using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

   
    public GameObject deadbody;
    public AudioClip[] hitsound;
    public int hp = 100;
    private Vector3 velositydamage;

    public void ApplyDamage(float damage, Vector3 velosity)
    {
        if (hp <= 0)
        {
            return;
        }
        hp -= (int)damage;

        velositydamage = velosity;
        if (hp <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        if (hitsound.Length > 0)
        {
            AudioSource.PlayClipAtPoint(hitsound[Random.Range(0, hitsound.Length)], transform.position);
        }
        if (deadbody)
        {
            GameObject obj = Instantiate(deadbody, this.transform.position, this.transform.rotation) as GameObject;//创建一个尸体.
            CopyTransformsRecurse(this.transform, obj);//把npc的信息给其尸体.
            Destroy(obj, 5);//5秒之后销毁.
        }
        Destroy(this.gameObject);
    }

    void CopyTransformsRecurse(Transform src, GameObject dst)
    {
        dst.transform.position = src.position;
        dst.transform.rotation = src.rotation;
        dst.transform.localScale = src.localScale;
        if (dst.rigidbody)
            dst.rigidbody.velocity = velositydamage / 20f;
        foreach (Transform child in dst.transform)
        {
            var curSrc = src.Find(child.name);
            if (curSrc)
            {
                CopyTransformsRecurse(curSrc, child.gameObject);
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
