using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPieces : MonoBehaviour {

	public int noOfPieces = 10;
	// public Vector2 force;
	public GameObject piece;
	// public float radius = 2f;
	bool hit;
	
	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Ball" && !hit)
		{
			this.gameObject.tag = "Untagged";
			hit = true;
			// this.GetComponent<Collider>().enabled = false;
			for (int i = 0; i < noOfPieces; i++)
			{
				GameObject glasspiece = Instantiate(piece, other.transform.position, Quaternion.identity);
				glasspiece.transform.localScale = new Vector3(transform.localScale.x/2, transform.localScale.y/2,
												transform.localScale.z/2);
				// Destroy(pieceRb.GetComponent<GlassPieces>());
				// pieceRb.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				// float randForce = Random.Range(force.x,force.y);
				// pieceRb.AddExplosionForce(randForce, Vector3.up, radius);
			}
			Destroy(this.gameObject);
		}

	}
}
