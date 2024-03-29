using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Auxiliars;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
	private const string HORIZONTAL_AXIS = "Horizontal";
	private const string VERTICAL_AXIS = "Vertical";

	private Rigidbody rig; //Físicas
	[SerializeField] Transform transform;
	[SerializeField]
	private float speed;
	[SerializeField]
	private Vector2 input; //(lados, arriba)
	private const float MAX_SPEED = 10f;

    private void Start()
	{
		//Pasa en el primer frame
		this.rig = GetComponent<Rigidbody>();
		this.input = Vector2.zero;
	}

	private void Update()
	{
		this.HandleInput();
	}

	private void FixedUpdate()
	{
		//Pasa cada 0.2s (a menos que lo cambies)
		//Lo usamos cada que necesitamos hacer operaciones de física
		//x: 2, y: 5 (2), x: 4, y: 10
		if(this.input == Vector2.zero)
		{
			rig.velocity = SpartanMath.Lerp(rig.velocity, Vector3.zero, Time.fixedDeltaTime * 3f);
		}
		Vector3 actualForce = new Vector3(input.x, input.y, 0f);
		Vector3 nextVelocity = Vector3.ClampMagnitude(this.rig.velocity + actualForce.normalized * speed, MAX_SPEED); //F = kg m/s^2
		this.rig.velocity = nextVelocity;
	}

	private void HandleInput()
	{
		this.input.x = Input.GetAxisRaw(HORIZONTAL_AXIS);
		this.input.y = Input.GetAxisRaw(VERTICAL_AXIS);
    }
}
