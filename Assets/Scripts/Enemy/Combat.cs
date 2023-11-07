using System.Collections;
using System.Collections.Generic;
using Auxiliars;
using UnityEngine;

//TODO: Optimize detection
public class Combat : MonoBehaviour {

	//1.- Detectar al jugador
	//2.- Disparar a su dirección
	//Screen space checking
	//Lerp by time

	[SerializeField]
	private float detectionRadius;

	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private float bulletSpeed;
	[SerializeField]
	private bool canShoot;

	private Transform player;
	private SpartanTimer cooldownTimer;
	private const float COOLDOWN_TIME_S = 1f;

	public bool Detected { get; private set; }

	private void Start()
	{
		//Cache the playerRef
		this.Detected = false;
		this.canShoot = true;
		this.player = EntityFetcher.PlayerRef.transform;
		this.cooldownTimer = new SpartanTimer(TimeMode.Framed);
	}

	private void Update()
	{
		//Destino - Origen
		Vector3 distanceVector = this.player.position - this.transform.position;
		//Distance is the magnitude of the resulting vector
		//Raíz cuadrada es costosa > Multiplicación
		this.Detected = distanceVector.sqrMagnitude < this.detectionRadius * this.detectionRadius;
		this.HandleCooldown();
		this.Shoot();
	}

	private void Shoot()
	{
		//Initial condition
		if (!Detected || !this.canShoot) return;
		//Instance prefab
		Bullet instance = Instantiate(this.bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
		//Control the movement of this instance
		Vector3 targetDir = player.position - transform.position;
		instance.Shoot(targetDir.normalized, bulletSpeed);
		this.cooldownTimer.Start();
		//Raycast

		//Implement cooldown
	}

	private void HandleCooldown()
	{
		float timePassed = this.cooldownTimer.GetCurrentTime(TimeScaleMode.Seconds);
		Debug.Log($"Time passed: {timePassed}");
		if (timePassed >= COOLDOWN_TIME_S)
		{
			this.canShoot = true;
			this.cooldownTimer.Stop();
		}
		else if (cooldownTimer.Started)
		{
			this.canShoot = false;
		}
	}

	private void OnDrawGizmos()
	{
		//TODO: Lerp this color from green to red
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, this.detectionRadius);
	}

}
