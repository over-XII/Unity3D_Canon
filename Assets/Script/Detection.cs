﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detection : MonoBehaviour {


    public float RayonShoot;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Detect();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        float theta = 0;
        float x = RayonShoot * Mathf.Cos(theta);
        float y = RayonShoot * Mathf.Sin(theta);
        Vector3 pos = transform.position + new Vector3(x, 0, y);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = RayonShoot * Mathf.Cos(theta);
            y = RayonShoot * Mathf.Sin(theta);
            newPos = transform.position + new Vector3(x, 0, y);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
        Gizmos.DrawLine(pos, lastPos);
    }

    //function to check if a target is in range
    public void Detect()
    {   
        List<GameObject> cible = GameObject.Find("Terrain").GetComponent<GamePlay>().TargetActive;
        float Rtarget = GameObject.Find("Terrain").GetComponent<GamePlay>().target.GetComponent<SphereCollider>().radius; //to take save the raduis of the Target

        if (cible.Count != 0)
            for (int i = 0; i < cible.Count; i++)
            {
                if (distanceVector(cible[0].transform.position, transform.position) < RayonShoot + Rtarget)
                {
                        if (this.GetComponent<rotationGaucheDroite>().automatique==true)
                        {
                            //print(-cible[0].transform.position);
                            //transform.Rotate(new Vector3(0, 1, 0), Vector3.Angle(GameObject.Find("PivotCanon").transform.forward, -cible[0].transform.position + GameObject.Find("PivotCanon").transform.position));
                            this.transform.LookAt(-cible[0].transform.position);
                        }
                }
            }
    }

    float distanceVector(Vector3 a,Vector3 b)
    {
        return Mathf.Sqrt((b.x - a.x)*(b.x - a.x) +(b.y - a.y)*(b.y - a.y) +(b.z - a.z)*(b.z - a.z));
    }
}
