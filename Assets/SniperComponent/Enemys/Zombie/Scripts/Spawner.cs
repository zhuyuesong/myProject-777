using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public Transform Objectman;//敌人.
    private int timetemp = 0;
    public float timeSpawn = 3;
    public int enemyCount = 0;
    public int radiun;
    int alreadyEnemyCount = 0;//已经生成的敌人数.

    // Use this for initialization
    void Start()
    {
        if (renderer)
            renderer.enabled = false;
        timetemp = (int)Time.time ;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        if (alreadyEnemyCount < enemyCount)
        {
            if (gos.Length < enemyCount)
            {

                if (Time.time > timetemp + timeSpawn)
                {
                    timetemp = (int)Time.time;
                    var enemyCreated = Instantiate(Objectman, transform.position/* + new Vector3(Random.Range(-radiun, radiun), this.transform.position.y, Random.Range(-radiun, radiun))*/, Quaternion.identity);
                    alreadyEnemyCount++;
                }
            }
        }
    }
}
