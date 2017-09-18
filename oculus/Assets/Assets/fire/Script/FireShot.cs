using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour {

    private float time = 0f;
    public Rigidbody rb;
    public GameObject explosion;

    // Use this for initialization
    void Start ()
    {
        time = 0f;
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(0,0,100));
    }
	
	// Update is called once per frame
	void Update ()
    {

        // time
        time += Time.deltaTime;

        if( time > 2.0 || transform.position.y < 0.1)
        // if (Vector3.Distance(new Vector3(0,0,0), transform.position) > 10)
        {
            Vector3 pos = transform.position;
            GameObject rightFire = (GameObject)Instantiate(explosion, pos, Quaternion.identity);
            explosion.name = "explosion";

            Destroy(gameObject);
        }
	}

    public void Force(float x, float y, float z)
    {

        Vector3 vec3 = new Vector3(x*1000, y*100, z*1000);
        rb.AddForce(vec3);
        Debug.Log("x=" + vec3.x + ", y=" + vec3.y + ", z=" + vec3.z);

    }
}
