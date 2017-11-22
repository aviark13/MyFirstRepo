using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float lookSmooth = 0.05f;
	public Vector3 offsetFromTargert = new Vector3 (0, 2.5f, -8);
	public float xTilt = 10;

	private Vector3 destination = Vector3.zero;
	private CharacterController charController;
	float rotateVel = 0;


	void Start () 
	{
		SetCameraTarget (target);
	}

	public void SetCameraTarget(Transform trans)
	{
		if (trans != null) 
		{
			if (trans.GetComponent<CharacterController>())
			{
				charController = trans.GetComponent<CharacterController> ();
			}
			else 
			{
				Debug.LogError ("The cameras target needs a camera controller.");
			}
		} 
		else 
		{
			Debug.LogError ("Your camera needs a target.");
		}
	}

	void LateUpdate () 
	{
		//moving
		MoveToTarget();
		//rotating
		LookAtTarget();
	}

	void MoveToTarget()
	{
		destination = charController.TargetRotation * offsetFromTargert;
		destination += target.position;
		this.transform.position = destination;
	}

	void LookAtTarget()
	{
		float eulerYAngle = Mathf.SmoothDampAngle (this.transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
		this.transform.rotation = Quaternion.Euler (xTilt, eulerYAngle, 0);
	}
}
