using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    private const float MAX_LIFETIME_S = 8f;

    private bool following;
    private float speed;
    private Vector3 direction;
    private float timeAlive = 0f;

    private GameObject PlayerRef => EntityFetcher.PlayerRef;

    private void Awake()
    {
        following = false;
        this.timeAlive = 0f;
    }

    private void Update()
    {
        this.HandleLifetime();
        //Follow
        if (!following) return;
        this.transform.position = transform.position + (direction * speed * Time.deltaTime);
        //Condition to check if we collided with the player
        if (CollidedWithPlayer())
		{
            //Damage the player
            HealthManager playerHealth = PlayerRef.GetComponent<HealthManager>();
            playerHealth.Damage();
            //Efectos de sonido mamadores
            Destroy(this.gameObject);
		}
    }

    public void Shoot(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
        this.following = true;
    }

    private bool CollidedWithPlayer()
	{
        //Create the raycast
        RaycastHit hit; //Store the information of the object with which we collided
        bool collided = Physics.Raycast(transform.position, direction, out hit, 1f);
        string playerTag = PlayerRef.tag;
        return collided && hit.transform.CompareTag(playerTag);
	}

    private void HandleLifetime()
	{
        this.timeAlive += Time.deltaTime;
        if (timeAlive > MAX_LIFETIME_S)
        {
            Destroy(this.gameObject);
        }
    }

	private void OnDrawGizmos()
	{
        if (direction == Vector3.zero) return;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction);
	}


}

