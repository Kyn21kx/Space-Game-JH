using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Auxiliars;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;

    [SerializeField]
    private Transform target;
    private Vector3 TargetPos => target.position;

    private float followingBlend;

    [SerializeField]
    private float followingSpeed;

    private void Start()
    {
        followingBlend = 0;
        offset =  TargetPos - this.transform.position;
    }

    private void FixedUpdate()
    {
       FollowTarget( Time.fixedDeltaTime);
    }

    //Quiero hacer una fn que dado el target y una velocidad, la camara lo siga.

    void FollowTarget( float timeStep )
    {
        followingBlend += timeStep * followingSpeed;

        if(followingBlend > 1)
        {
            followingBlend = 0;
        }

        Debug.Log($"{TargetPos}, {offset}");
        
        this.transform.position = SpartanMath.SmoothStop(this.transform.position, TargetPos - offset, followingBlend, 2f);
    }
}
