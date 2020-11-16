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
            localScale.x = this.GetComponentInParent<Enemy>().GetHealth() / 5;
            transform.localScale = localScale;
        }
        //if (gameObject.GetComponent<Enemy>().GetHealth() > 0)
        //{
        //    localScale.x = gameObject.GetComponent<Enemy>().GetHealth() / 5;
        //    transform.localScale = localScale;
        //}
        
    }
}
