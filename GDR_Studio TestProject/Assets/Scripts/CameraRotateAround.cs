using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//не мой код. Долго пытался реализовать сам, но в итоге позаимстовал из интернета.
// правда под себя доработать пришлось все-таки.
public class CameraRotateAround : MonoBehaviour {

	Transform player = null;
	//Transform target;
	Vector3 offset;
	public float sensitivity = 3; // чувствительность мышки
	public float limitDown = -20; // ограничение вращения по Y
	public float limitUp = 80; // ограничение вращения по Y
	public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
	public float zoomMax = 10; // макс. увеличение
	public float zoomMin = 3; // мин. увеличение
	private float X, Y;

	public Vector3 offsetGlobal = Vector3.zero;
	public bool findPlayerOnAwake = true;

	private void Awake()
	{
		if (findPlayerOnAwake)
		{
			StartCoroutine(FindPlayer());
		}
	}

	void Start () 
	{		
		offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax)/2);
		//transform.position = player.position + offset + offsetGlobal;
	}

	void Update ()
	{
		if (!player) return;
		//transform.position = player.position + offset + offsetGlobal;
		
		if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
		else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
		offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

		X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
		Y += Input.GetAxis("Mouse Y") * sensitivity;
		Y = Mathf.Clamp(Y, -limitUp, limitDown);
		transform.localEulerAngles = new Vector3(-Y, X, 0);
		transform.position = transform.localRotation * offset + player.position + offsetGlobal;
	}

	IEnumerator FindPlayer()
	{
		while (!player)
		{
			player = GameObject.FindGameObjectWithTag("Player")?.transform;

			yield return new WaitForSeconds(1f);
		}
	}
}