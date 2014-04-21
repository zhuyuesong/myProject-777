using UnityEngine;
using System.Collections;

public class CriticalStrike : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        print(transform.localScale);
        transform.localScale = new Vector3(1, 1, 1);
        Destroy(gameObject, 2);
	}
}
