using UnityEngine;
using System.Collections;

public class Prey
{
	
		private GameObject go;
		public Vector3 velocity;
		public Vector3 heading;
		public float speed;
		private bool r;
		private int viewDistance = 5;

		public Prey ()
		{  
				speed = 0.25f;
				go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				go.name = "prey";
				go.transform.position = new Vector3 (Random.Range (-10, 10), 0, Random.Range (-10, 10));
				go.AddComponent<SphereCollider> ();
				go.AddComponent<Rigidbody> ();
				ResetHeading ();
		}
	
		public void Update ()
		{         
				Debug.DrawRay (go.transform.position, go.transform.forward * viewDistance);

//				go.transform.Translate (speed * go.transform.forward, Space.World);
				Clamp ();
		}

		public void OnCollisionEnter (Collision collision)
		{
				Debug.Log ("hit");
		}
			
		public void ResetHeading ()
		{
				go.transform.Rotate (Vector3.up, Random.Range (0, 360));
		}

		public void Clamp ()
		{
				go.transform.rotation = Quaternion.Euler (0, go.transform.rotation.eulerAngles.y, 0);
//				go.transform.position = new Vector3 (go.transform.position.x, 0, go.transform.position.z);		
		}
}
