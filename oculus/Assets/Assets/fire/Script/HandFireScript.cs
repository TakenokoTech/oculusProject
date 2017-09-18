using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFireScript : MonoBehaviour {

    private GameObject hand = null;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (hand == null) return; 
        Vector3 handPos = hand.transform.position;
        transform.position = handPos;
    }

    public void SetHand(GameObject hand)
    {
        this.hand = hand;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
