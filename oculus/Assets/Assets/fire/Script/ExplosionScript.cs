using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

    private float time = 0f;

    // Use this for initialization
    void Start()
    {
       time = 0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // time
        time += Time.deltaTime;

        if (time > 5.0) {
            Destroy(gameObject);
        }
    }
}
