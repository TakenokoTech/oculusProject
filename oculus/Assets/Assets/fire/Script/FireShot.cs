using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour {

    public Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0,0,1000));
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(new Vector3(0,0,0), transform.position) > 100)
        {
            Destroy(gameObject);
        }
	}

    public void Force(float x, float y, float z) {

        Vector3 vec3 = new Vector3(x*100, y*100, z*100);
        rb.AddForce(vec3);
        // Debug.Log("x=" + vec3.x + ", y=" + vec3.y + ", z=" + vec3.z);

    }
}
