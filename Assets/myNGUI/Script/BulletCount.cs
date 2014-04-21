using UnityEngine;
using System.Collections;

public class BulletCount : MonoBehaviour {

    UILabel text;
    int count;
	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<UILabel>();
        count = Gun.bulletCount;
	}
	
	// Update is called once per frame
	void Update () {

        text.text = Gun.bulletCount.ToString() + "/" + count.ToString();
	}
}
