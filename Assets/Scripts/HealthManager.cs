using Auxiliars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	[SerializeField]
	private int health;
	[SerializeField]
	private Material playerMaterial;
	private SpartanTimer damageFxTimer;

	private const string MATERIAL_COLOR_REF = "_Color";
	private const int FX_DURATION_MS = 200;

	private void Start()
	{
		damageFxTimer = new SpartanTimer(TimeMode.Framed);
	}

	private void Update()
	{
		if (health <= 0)
		{
			//Pause game
			//Show death menu
			//Change colors	on the screen
			playerMaterial.SetColor(MATERIAL_COLOR_REF, Color.white);
			Destroy(gameObject);
		}
		this.HandleDamageFX();
	}

	public int Damage()
	{
		this.health--;
		damageFxTimer.Start();
		playerMaterial.SetColor(MATERIAL_COLOR_REF, Color.red);
		//Algún efecto mamador
		return this.health;
	}

	private void HandleDamageFX()
	{
		if (!this.damageFxTimer.Started || this.damageFxTimer.CurrentTimeMS < FX_DURATION_MS) return;
		//Check if the timer has passed x seconds, and return back to the original color
		playerMaterial.SetColor(MATERIAL_COLOR_REF, Color.white);
		this.damageFxTimer.Stop();
	}
}
