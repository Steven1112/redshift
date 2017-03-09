﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChemicalDetector : MonoBehaviour {
    public GameObject infoObject;
    GameObject focusedObject = null;

	// the thickness of the raycast
	//float thickness = 10.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        RaycastHit hit;
		// Physics.SphereCast(transform.position,thickness,transform.forward, out hit)
		if (Physics.Raycast(transform.position,transform.forward, out hit) && hit.transform.parent != null)
        {
			if (hit.transform.parent.tag == "asteroids") {
				// is looking at an asteroid
				if (hit.transform != null)
				{
					focusedObject = hit.transform.gameObject;
					ShowInfo(hit.transform);
				}
				Debug.Log(hit.transform.name);
			}
        }
        else
        {
            // reset focused object to null when player is not looking at an asteroid
            if (focusedObject != null)
            {
                infoObject.transform.parent = null;
                infoObject.SetActive(false);
                focusedObject = null;
            }
        }
    }

    // show info of the asteroid that is being looked at
    void ShowInfo(Transform hit)
    {
        infoObject.SetActive(true);
        infoObject.transform.parent = hit;
		infoObject.transform.rotation = Quaternion.identity;
        Vector3 temp = Vector3.zero;
        float radius = hit.gameObject.GetComponent<SphereCollider>().radius;
        temp.y = radius + 0.005f;
        infoObject.transform.localPosition = temp;
		infoObject.transform.GetChild (0).GetComponent<Text> ().text = "contains " + hit.transform.tag; 
		Debug.Log ("showed info");
    }
}