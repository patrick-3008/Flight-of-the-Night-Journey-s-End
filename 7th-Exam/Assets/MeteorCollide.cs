using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollide : MonoBehaviour
{
    // Start is called before the first frame update
    bool hit;
    void Start()
    {
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < 1.0f)
        {
            Stats.destroyedMeteors++;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "redbull" && !hit)
        {
         //   Destroy(collision.gameObject);
          //  Stats.redbullsDestroyed++;
        //    hit = true;
        }
        else if (collision.gameObject.name == "Bat")
        {

            PlayerMove pm = collision.gameObject.GetComponent<PlayerMove>();
            if (!pm.invincible)
            {
                pm.lives--;
                pm.Respawn();
                pm.Lose();
            }
            hit = true;

        }
    }


}
