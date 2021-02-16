using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	Vector3 rotation = Vector3.zero;
	Vector3 moveDir = Vector3.zero;

	Animator anim;
	
	float rotSpeed = 50f;	
	float speed = 1f;
	float runSpeed = 3.5f;
	float currentSpeed;

	bool isBall = false;
	bool isClose = true;
	bool inProcess = false;
	bool uncovered = false;

	public bool SecondLvlBool = false;

	public bool _isClose
	{
		get
		{
			return isClose;
		}
	}
	public bool _uncovered
	{
		get
		{
			return uncovered;
		}
	}


	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		gameObject.transform.eulerAngles = rotation;
	}

	
	void Update()
	{
		CheckKey();
		gameObject.transform.eulerAngles = rotation;

		if (isBall)
		{
			InBall();
		}
	}

	void CheckKey()
	{
		if (!inProcess)
		{
			// ѕоворот влево
			if (Input.GetButton("MoveLeft"))
			{
				rotation.y -= rotSpeed * Time.deltaTime;
			}

			// ѕоворот вправо
			if (Input.GetButton("MoveRight"))
			{
				rotation.y += rotSpeed * Time.deltaTime;
			}

			// ’отьба
			if (!isBall && !isClose) // если не в состо€нии шара
			{
				if (Input.GetButton("MoveForward"))
				{
					moveDir.z = Input.GetAxis("MoveForward");

					transform.Translate(moveDir * speed * Time.deltaTime);

					anim.SetBool("Walk_Anim", true);
				}
				else
				{
					anim.SetBool("Walk_Anim", false);
				}
			}

			// ”скорение с превращением в шар
			if (Input.GetButtonDown("Transformation"))
			{
				if (!isClose)
				{
					RollAnimHelper();
				}
				else
				{
					if (!anim.GetBool("RollThenClose"))
					{
						anim.SetBool("RollThenClose", true);
						isBall = true;
						rotSpeed *= 2f;

						uncovered = true;
					}
					else
					{
						anim.SetBool("RollThenClose", false);
						isBall = false;
						rotSpeed /= 2;

						uncovered = false;
					}
				}
			}

			// ѕревращение в шар
			if (Input.GetButtonDown("Open/Close"))
			{
				StartCoroutine(InProcess());

				if (!anim.GetBool("Open_Anim"))
				{
					anim.SetBool("Open_Anim", true);
					isClose = false;
				}
				else
				{
					anim.SetBool("Open_Anim", false);
					isClose = true;
				}
			}
		}		
	}

	public void InBall()
	{		
		moveDir.z = 1f; // выставл€ем навправление вектора движени€

		if(currentSpeed < runSpeed) // данный блок отвечает за постепенный разгон после превращени€ в шар
		{
			currentSpeed += 0.01f;
		}

		transform.Translate(moveDir * currentSpeed * Time.deltaTime); //движение
	} 	

	// задержка дл€ проигрыша анимации в 3,3сек
	IEnumerator InProcess()
	{
		inProcess = true;

		yield return new WaitForSeconds(3.30f);

		inProcess = false;
	}

	public void RollAnimHelper()
	{
		if (anim.GetBool("Roll_Anim"))
		{
			anim.SetBool("Roll_Anim", false);
			isBall = false;
			currentSpeed = 0; // текуща€ скорость сбрасываетс€ до 0 (при движении в этой форме)
			rotSpeed /= 2f;

			uncovered = false;
		}
		else
		{
			anim.SetBool("Roll_Anim", true);
			isBall = true;
			rotSpeed *= 2f;

			uncovered = true;
		}
	}

	// столкновение со стенй приводит к превращению в бота из шара
	private void OnCollisionEnter(Collision collision)
	{

		if (collision.gameObject.CompareTag("Wall"))
		{
			if (isBall)
			{
				SecondLvlBool = true; // bool дл€ задани€ во втором левеле.

				anim.SetBool("Collision", true);
				anim.SetBool("Roll_Anim", false);
				anim.SetBool("Open_Anim", true);

				isBall = false;
				isClose = false;

				currentSpeed = 0; // текуща€ скорость сбрасываетс€ до 0 (при движении в этой форме)
				rotSpeed /= 2f;

				Invoke("AnimHelper", 3f);
			}
		}
	}

	public void AnimHelper()
	{
		anim.SetBool("RollThenClose", false);
		anim.SetBool("Collision", false);
	}
}
