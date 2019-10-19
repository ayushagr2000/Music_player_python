using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class CannonController : MonoBehaviour {

	public GameObject cannonBall, cannonShoot;
	public TextMesh noOfBallText, extraBallsText, ballTextShadow, extraTextShadow;
	public int noOfBalls = 5;
	public int noOfExtraballs = 0;
	public Transform spawnPoint, extraSpawnPoint;
	public float force;
	public float distance = 10;
	// [HideInInspector]
	public bool start;
	Rigidbody rb, rb1;
	AudioSource aS;
	public AudioClip outOfAmmo, shoot;

	void Start()
	{
		aS = this.GetComponent<AudioSource>();
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0) && noOfBalls<=0 && start)
		{
			aS.clip = outOfAmmo;
			aS.Play();
		}
		if(Input.GetMouseButtonDown(0) && noOfBalls>0 && start)
		{
			// this.GetComponentInChildren<Animator>();
			this.GetComponentInChildren<Animator>().Play("Recoil");
			Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);
			Instantiate(cannonShoot);
			
			if(noOfExtraballs>0)
			{
				rb = Instantiate(cannonBall, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
				rb1 = Instantiate(cannonBall, extraSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();		
				rb.transform.LookAt(mousePos);    
				rb1.transform.LookAt(mousePos);    
				rb.AddForce(rb.transform.forward * force*1000);
				rb1.AddForce(rb.transform.forward * force*1000);
			}
			else
			{
				rb = Instantiate(cannonBall, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();			
				rb.transform.LookAt(mousePos);    
				rb.AddForce(rb.transform.forward * force*1000);
			}
			BallsReduce();
			aS.clip = shoot;
			aS.Play();
			// transform.LookAt(mousePos); 
			CameraShaker.Instance.ShakeOnce(1.5f,1.5f, 0f, 0.5f);
		}
		
		TextUpdate();
		
	}
	void BallsReduce()
	{
		noOfBalls --;
		if(noOfExtraballs>0)
		{
			noOfExtraballs --;
		}
	}
	public void AddBalls(int add)
	{
		noOfBalls += add;
	}
	public void AddExtraBalls(int add)
	{
		noOfExtraballs += add;
	}

	void TextUpdate()
	{
		if(!start)
		{
			noOfBallText.gameObject.SetActive(false);
			ballTextShadow.gameObject.SetActive(false);
		}
		else
		{
			noOfBallText.gameObject.SetActive(true);
			ballTextShadow.gameObject.SetActive(true);
		}
		if(noOfBalls >0)
		{
			noOfBallText.text = "x " + noOfBalls;
		}
		else
		{
			noOfBallText.text = "NO BALLS";
		}
		ballTextShadow.text = noOfBallText.text;
		if(noOfExtraballs >0)
		{
			extraBallsText.gameObject.SetActive(true);
			extraTextShadow.gameObject.SetActive(true);
			extraBallsText.text = "+ " + noOfExtraballs + " extra";
			extraTextShadow.text = extraBallsText.text;
		}
		else
		{
			extraBallsText.gameObject.SetActive(false);
			extraTextShadow.gameObject.SetActive(false);
		}		
	}
}
