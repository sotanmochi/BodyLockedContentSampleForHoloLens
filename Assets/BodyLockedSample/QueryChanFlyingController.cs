using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueryChanFlyingController : MonoBehaviour
{
	public enum CharacterState
	{
		Idle,
		Move,
	}

	public float distance = 1.80f;
	public float offsetFromCenter = 0.20f;
	public float moveSpeed = 2.0f;

	private Transform mainCamera;

	private CharacterController characterController;
	private CharacterState state;
	private bool forwardMode;
	private Vector3 destination;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		mainCamera = Camera.main.transform;
		
		forwardMode = false;
		state = CharacterState.Idle;
		destination = distance * mainCamera.forward;

		// LookAt
		Vector3 direction = mainCamera.position - transform.position;
		transform.forward = direction;
	}
	
	void Update()
	{
		switch(state)
		{
			case CharacterState.Idle:
				UpdateIdle();
				break;
			case CharacterState.Move:
				UpdateMove();
				break;
			default:
				break;
		}
	}

	void UpdateIdle()
	{
		Vector3 direction = transform.position - mainCamera.position;
		transform.forward = forwardMode ? direction : -direction;
		if(IsOutOfView())
		{
			destination = distance * mainCamera.forward;
			state = CharacterState.Move;
		}
	}

	void UpdateMove()
	{
		if(IsOutOfView())
		{
			destination = distance * mainCamera.forward;
		}		
		float remainingDistance = Vector3.Distance(destination, transform.position);
		if(remainingDistance > offsetFromCenter)
		{
			Move();
		}
		else
		{
			state = CharacterState.Idle;
		}
	}

	bool IsOutOfView()
	{
		Vector3 direction = (transform.position - mainCamera.position).normalized;
		float dot = Vector3.Dot(mainCamera.forward, direction);
		if(dot < 0.93) // theta is grater than equal 22 degree.
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void Move()
	{
		Vector3 direction = (destination - transform.position).normalized;
		transform.forward = direction;
		characterController.Move(direction * moveSpeed * Time.deltaTime);
	}

	public void ChangeForwardMode()
	{
		forwardMode = !forwardMode;
	}
}
