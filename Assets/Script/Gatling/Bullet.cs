﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {

    public GameObject ExitBullet;
    public GameObject Plane;
    public AudioClip explosion;

    void Start()
    {
    }
	// Update is called once per frame
	void Update () {
        if(transform.position.x<16 && transform.position.z<16 && transform.position.x>-16 && transform.position.z > -16) //To check if the bullet is in of the Map
        {
            HitTarget();
            
        }
        else
        {
            ExitBullet.GetComponent<Shoot>().DisableBullet(this.gameObject);
        }
	}


    //To know if it hit a Target or not 
    void HitTarget()
    {
        List<GameObject> cible = Plane.GetComponent<GamePlay>().TargetActive;
        float Rtarget = Plane.GetComponent<GamePlay>().target.GetComponent<SphereCollider>().radius; //to take save the raduis of the Target
        if (cible.Count != 0)
            for (int i = 0; i < cible.Count; i++)
            {
                if (distanceVector(cible[i].transform.position, transform.position) <  Rtarget && cible[i].GetComponent<BehaviourTarget>().shield !=true)
                {
                    ExitBullet.GetComponent<Shoot>().DisableBullet(this.gameObject);
                    cible[i].GetComponent<BehaviourTarget>().Life--;
                    //print("La vie de la cible "+i+ " aprés est de "+ cible[i].GetComponent<BehaviourTarget>().Life);
                    if (cible[i].GetComponent<BehaviourTarget>().Life == 0)
                    {
                        //Control Sound
                        Camera.main.GetComponent<AudioSource>().PlayOneShot(explosion);
                        //Control Particule
                        ParticleSystem p = (Instantiate(cible[i].GetComponent<BehaviourTarget>().explosion.gameObject, cible[i].transform.position, Quaternion.identity) as GameObject).GetComponent<ParticleSystem>();
                        p.Play();
                        Plane.GetComponent<GamePlay>().DisableTarget(cible[i],true);
                        Destroy(p,0.5f);
                    }
                }
                else if(distanceVector(cible[i].transform.position, transform.position) < Rtarget)
                {
                    cible[i].GetComponent<BehaviourTarget>().shield = false;
                    ExitBullet.GetComponent<Shoot>().DisableBullet(this.gameObject);
                }
                
            }
        
    }


    float distanceVector(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt((b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y) + (b.z - a.z) * (b.z - a.z));
    }
}
