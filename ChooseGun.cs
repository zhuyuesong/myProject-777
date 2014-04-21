using UnityEngine;
using System.Collections;

public class ChooseGun : MonoBehaviour {

    public GameObject hand;
    public Transform pos;
    public Vector3 offsetPosion ;
	// Use this for initialization
	void Start () {

        transform.parent = hand.transform;
        transform.position = new Vector3(pos.position.x + offsetPosion.x,
            pos.position.y + offsetPosion.y,
            pos.position.z + offsetPosion.z); ;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(pos.position.x + offsetPosion.x,
            pos.position.y + offsetPosion.y,
            pos.position.z + offsetPosion.z);
	}
}
