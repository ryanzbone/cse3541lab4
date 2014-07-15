using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldController : MonoBehaviour
{	
		public GameObject PreyPrefab, PredatorPrefab;
		public List<GameObject> prey; 

		void Start ()
		{				
				prey = new List<GameObject> ();
				for (int i = 0; i < 20; i++) {
						prey.Add ((GameObject)Instantiate (PreyPrefab, 
			                                   new Vector3 (Random.Range (-10, 10), 0, Random.Range (-10, 10)), 
			                                   Quaternion.identity));
				} 
				Instantiate (PredatorPrefab);
		}
}
