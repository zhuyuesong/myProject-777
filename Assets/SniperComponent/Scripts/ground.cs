using UnityEngine;
using System.Collections;

public class ground : MonoBehaviour {
    float mouseXstart = 0;
    float mouseXend = 0;
    float mouseX = 0;
    public GameObject []Gun;
   
    float starrGroundX = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
        if (Input.GetMouseButtonDown(0))
        {
            mouseXstart = Input.mousePosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseXend = Input.mousePosition.x;
            mouseX = mouseXend - mouseXstart;
            starrGroundX = Gun[0].transform.position.x;

            //if (mouseX > 0)
            //{
            //    if(Gun[0].transform.position.x)
            //}
            //else if (mouseX < 0)
            //{
            //}
        }

        print(mouseXstart +"  "+ mouseXend);
        if (mouseX > 0)
        {
            Gun[0].transform.Translate(Vector3.forward  * Time.deltaTime * 3);
            Gun[1].transform.Translate(-Vector3.up * Time.deltaTime * 3);

            if ((Gun[0].transform.position.x - starrGroundX) >= 3f)
            {

                if (Gun[0].transform.position.x >= 3f)
                    Gun[0].transform.position = new Vector3(-3f, 0, Gun[0].transform.position.z);
               

                if (Gun[1].transform.position.x >= 3f)
                    Gun[1].transform.position = new Vector3(-3f, -0.27f, Gun[1].transform.position.z);
                

                mouseX = 0;
            }
            else if (mouseX < 0)
            {
            }
        }

        //Gun.transform.position = new Vector3((transform.position.x+3.1f )/ 2 - 1 / 2,
        //    Gun.transform.position.y,
        //   Gun.transform.position.z);
	}
}
