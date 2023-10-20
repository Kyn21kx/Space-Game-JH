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

	private Transform player;
	private SpartanTimer cooldownTimer;
	private const float COOLDOWN_TIME_S = 1f;

	public bool Detected { get; private set; }

	private void Start()
	{
		//Cache the playerRef
		this.Detected = false;
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
		this.Shoot();
	}

	private void Shoot()
	{
		//Initial condition
		if (!Detected && (this.cooldownTimer.GetCurrentTime(TimeScaleMode.Seconds) < COOLDOWN_TIME_S)) return;
		this.cooldownTimer.Reset();
		//Instance prefab
		Bullet instance = Instantiate(this.bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
		//Control the movement of this instance
		Vector3 targetDir = player.position - transform.position;
		instance.Shoot(targetDir.normalized, bulletSpeed);
		//Raycast

		//Implement cooldown
	}

	private void OnDrawGizmos()
	{
		//TODO: Lerp this color from green to red
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, this.detectionRadius);
	}

}
