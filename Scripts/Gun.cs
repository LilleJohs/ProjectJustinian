using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public bool automatic = false;
	public float fireRate = 0.5f;
	public float shotSpread = 0.5f;
	public float aimTime = 0.5f;
	public Vector3 aimPos;
	public Vector3 startPos;

	private float currentShotSpread = 0.5f;
	private float nextFire = 0.0f;
	private Transform mainCam;
	private Camera cam;
	private float velocityFOV;
	private Vector3 velocity;

	// Start is called before the first frame update
	void Start()
    {
		currentShotSpread = shotSpread;
		mainCam = transform.parent;
		cam = mainCam.GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update() {
		if (automatic)
		{
			if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
			{
				Shoot();
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire)
			{
				Shoot();
			}
		}

		if (Input.GetKey(KeyCode.Mouse1))
		{
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				currentShotSpread = 0;
				//mainCam.GetComponent(MouseLook).sensitivityY = mainCam.GetComponent(MouseLook).standardSen / 3;
				//player.GetComponent(MouseLook).sensitivityX = player.GetComponent(MouseLook).standardSen / 3;
			}
			cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, 40, ref velocityFOV, aimTime * 2);
			transform.localPosition = Vector3.SmoothDamp(transform.localPosition, aimPos, ref velocity, aimTime);
		}
		else
		{
			cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, 90, ref velocityFOV, aimTime);
			transform.localPosition = Vector3.SmoothDamp(transform.localPosition, startPos, ref velocity, aimTime * 2);
		}

		if (Input.GetKeyUp(KeyCode.Mouse1))
		{
			currentShotSpread = shotSpread;
			//mainCam.GetComponent(MouseLook).sensitivityY = mainCam.GetComponent(MouseLook).standardSen;
			//player.GetComponent(MouseLook).sensitivityX = player.GetComponent(MouseLook).standardSen;
		}
	}

	void Shoot() {
		GetComponent<AudioSource>().Play();
		nextFire = Time.time + fireRate;

		Vector3 direction = SprayDirection();

		RaycastHit hit;
		if (Physics.Raycast(mainCam.position, direction, out hit, 2000, 1))
		{
			GameObject makeSpark = Instantiate(Resources.Load("GunEffect/Ground"), hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;

			if (hit.rigidbody)
			{
				hit.rigidbody.AddForceAtPosition(100 * mainCam.forward, hit.point);
			}
		}
	}


	public Vector3 SprayDirection() {
		float vx = (1 - 2 * Random.value) * currentShotSpread;
		float vy = (1 - 2 * Random.value) * currentShotSpread;
		float vz = 1.0f;
		return mainCam.TransformDirection(new Vector3(vx, vy, vz));
	}
}
