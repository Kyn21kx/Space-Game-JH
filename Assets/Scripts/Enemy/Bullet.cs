using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    private bool following;
    private float speed;
    private Vector3 direction;

    private void Awake()
    {
        following = false;
    }

    private void Update()
    {
        if (!following) return;
        this.transform.position = transform.position + (direction * speed * Time.deltaTime);
    }

    public void Shoot(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
        this.following = true;
    }

}

