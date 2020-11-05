using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHidden : MonoBehaviour
{

    public bool revealed;

    // Start is called before the first frame update
    void Start()
    {
        revealed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Revealed")){
            revealed = true;
        }
    }
}
