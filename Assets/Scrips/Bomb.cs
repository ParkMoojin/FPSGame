using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float time;

    // Update is called once per frame
    void Update()
    {
        time-=Time.deltaTime;

        if (time <=0 )
        {
            GetComponent<Animator>().SetTrigger("Explosion");
            Invoke("DestroyThis", 0.5f);
        }

    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
