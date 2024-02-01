using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public Object meteorObject;
    public Object redBullObject;
    public Object starObject;
    public Object bat;
    float timer;
    System.Random rand;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.25f;
        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 0.25f;
            Vector3 dropForward = bat.GameObject().transform.forward;
            dropForward.x += rand.Next(-50, 50);
            dropForward.z *= 100.0f;
            dropForward.y += 100.0f;

            if (rand.Next(100) <= 5)
            {
                Object d = Instantiate(starObject, bat.GameObject().transform.position + dropForward , Quaternion.identity);
                d.GetComponent<Rigidbody>().velocity = Vector3.down * 30.0f;
                d.name = "star";
                d.hideFlags = HideFlags.HideInHierarchy;
                Stats.stars++;

            }
            else if(rand.Next(100) <= 10)
            {
                Object d = Instantiate(redBullObject, bat.GameObject().transform.position + dropForward, Quaternion.identity);
                d.GetComponent<Rigidbody>().velocity = Vector3.down * 30.0f;
                d.name = "redbull";
                d.hideFlags = HideFlags.HideInHierarchy;
                Stats.totalRedbulls++;

            }
            else
            {
                Object d = Instantiate(meteorObject, bat.GameObject().transform.position + dropForward , Quaternion.identity);
                d.GetComponent<Rigidbody>().velocity = Vector3.down * 30.0f;
                d.name = "meteor";
                d.hideFlags = HideFlags.HideInHierarchy;
                Stats.totalMeteors++;
            }
        }
    }
}
