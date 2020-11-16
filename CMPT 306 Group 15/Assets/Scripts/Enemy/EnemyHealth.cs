using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 localScale;

    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponentInParent<Enemy>().GetHealth() > 0)
        {
            //Debug.Log("HEALTH = " + this.GetComponentInParent<Enemy>().GetHealth());
            //Debug.Log("MAX HEALTH = " + this.GetComponentInParent<Enemy>().GetMaxHealth());
            localScale.x = this.GetComponentInParent<Enemy>().GetHealth() / this.GetComponentInParent<Enemy>().GetMaxHealth();
            localScale.x = localScale.x / 4; // make fit better
            transform.localScale = localScale;
        }
    }
}
