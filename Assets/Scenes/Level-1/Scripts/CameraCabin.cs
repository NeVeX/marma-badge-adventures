using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class CameraCabin : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;

	public float sensiticity = 1;
	public float smoothing = 3;

	public float maxY=15;
	public float maxX=60;
	public float minY=-5;
	public float minX=-60;

	Transform oldPos;

	void Start () {


	}
	

	void Update () 
	{

		if(Camera.main!=null)
		{
			Vector2 md = new Vector2(Input.GetAxisRaw("Right Analog Stick (Horizontal)"), Input.GetAxisRaw("Right Analog Stick (Vertical)"));
			md = Vector2.Scale(md, new Vector2(sensiticity*smoothing,sensiticity*smoothing));
			smoothV.x = Mathf.Lerp(smoothV.x,md.x,1/smoothing);
			smoothV.y = Mathf.Lerp(smoothV.y,md.y,1/smoothing);
			mouseLook+=smoothV;

			mouseLook.y=Mathf.Clamp(mouseLook.y,minY,maxY);
			mouseLook.x=Mathf.Clamp(mouseLook.x,minX,maxX);

			transform.localRotation=Quaternion.AngleAxis(-mouseLook.y,Vector3.right);

			this.transform.parent.transform.localRotation= Quaternion.AngleAxis(mouseLook.x,Vector3.up);

			Camera.main.transform.position=transform.position;
			Camera.main.transform.rotation=transform.rotation;

			}
			else if(Camera.main==null)
				Debug.Log("please add a camera to the scene with tag 'MainCamera' ");

	}
		
}
