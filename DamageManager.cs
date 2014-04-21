using UnityEngine;
using System.Collections;

public class DamageManager : MonoBehaviour
{

    public AudioClip[] hitsound;
    public GameObject body;
    public int DamageMult = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //计算伤害.
    public void ApplyDamage(float damage, Vector3 velosity)
    {
        if (hitsound.Length > 0)
        {
            AudioSource.PlayClipAtPoint(hitsound[Random.Range(0, hitsound.Length)], transform.position);//随机播放一个声音.
        }
        if (body)
        {
            if (body.GetComponent<Status>())
            {
                body.GetComponent<Status>().ApplyDamage(damage * DamageMult, velosity * DamageMult);
            }
        }
    }

}
