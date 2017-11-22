using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public float inputDelay = 0.1f;
	public float forwardVel = 12f;
	public float rotateVel = 200f;

	public Animator anim;
	//public RuntimeAnimatorController anim;

	private Quaternion targetRotation;
	private Rigidbody rBody;
	float forwardInput, turnInput;


	public Quaternion TargetRotation
	{
		get { return targetRotation; }
	}

	void Start()
	{
		//get animator
		anim = this.GetComponent<Animator>();
		//anim = this.GetComponent<Animator>().runtimeAnimatorController;

		targetRotation = this.transform.rotation;
		rBody = this.GetComponent<Rigidbody> ();
		if (!rBody) 
		{
			Debug.Log ("The Character needs a ridgidbody.");
		}

		forwardInput = 0f;
		turnInput = 0f;

	}

	void GetInput()
	{
		forwardInput = Input.GetAxis("Vertical");
		turnInput = Input.GetAxis("Horizontal");
	}

	void Update()
	{
		GetInput ();
		Turn ();
	}

	void FixedUpdate()
	{
		Run ();
	}

	void Run()
	{
		if (Mathf.Abs (forwardInput) > inputDelay) 
		{
			// move
			rBody.velocity = this.transform.forward * forwardInput * forwardVel;
			anim.SetFloat("vertical",forwardInput);
		} 
		else 
		{
			//stop moving
			rBody.velocity = Vector3.zero;
			anim.SetFloat("vertical", 0f);
		}
	}

	void Turn()
	{
		if (Mathf.Abs (turnInput) > inputDelay) 
		{
			targetRotation *= Quaternion.AngleAxis (rotateVel * turnInput * Time.deltaTime, Vector3.up);
		}
		this.transform.rotation = targetRotation;
	}
	
}
