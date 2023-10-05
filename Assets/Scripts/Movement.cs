using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    private Rigidbody rig; //Físicas
	[SerializeField]
	private float speed;
	[SerializeField]
	private Vector2 input; //(lados, arriba)

    private void Start()
	{
		//Pasa en el primer frame
		this.rig = GetComponent<Rigidbody>();
		this.input = Vector2.zero;
	}

	private void Update()
	{
		this.HandleInput();
		Debug.Log($"Mag: {this.input.magnitude}, Input: {this.input}");
		//Pasa cada frame (variante)
		//Toda la demás lógica, incluyendo Input
	}

	private void FixedUpdate()
	{
		//Pasa cada 0.2s (a menos que lo cambies)
		//Lo usamos cada que necesitamos hacer operaciones de física
		//x: 2, y: 5 (2), x: 4, y: 10
		Vector3 actualForce = new Vector3(input.x, 0f, input.y);
		this.rig.AddForce(actualForce.normalized * speed); //F = kg m/s^2
	}

	private void HandleInput()
	{
		this.input.x = Input.GetAxisRaw(HORIZONTAL_AXIS);
		this.input.y = Input.GetAxisRaw(VERTICAL_AXIS);
	}
}
