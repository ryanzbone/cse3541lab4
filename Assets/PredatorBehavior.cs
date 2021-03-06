﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PredatorBehavior : MonoBehaviour
{
  public float speed;
  public GameObject target, emptyTarget;
  public int viewDistance, viewAngle;
  private RaycastHit hit;
  private Ray viewRay;

  void Start ()
  {
    viewRay = new Ray (transform.position, transform.forward);
    emptyTarget = new GameObject ();
    emptyTarget.transform.position = new Vector3 (0, 100, 0);
    target = emptyTarget;
    Debug.Log (target.transform.position);
    speed = 0.2f;
    name = "predator";
    viewDistance = 10;
    viewAngle = 30;
    transform.position = new Vector3 (Random.Range (-20, 20), 0, Random.Range (-20, 20));
  }

  public void Update ()
  {
    for (int i = -viewAngle; i <= viewAngle; i++) {
      Vector3 k = Quaternion.AngleAxis (i, Vector3.up) * transform.forward;
      Debug.DrawRay (transform.position, k * viewDistance);
    }

    SearchForTarget ();

    if (target != emptyTarget)
      PursueTarget ();
    else
      transform.Translate (transform.forward * speed, Space.World);

    Clamp ();
  }

  public void PursueTarget ()
  {
    transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed);
    transform.LookAt (target.transform.position, Vector3.up);
  }

  private void SearchForTarget ()
  {
    viewRay.origin = transform.position;
    for (int i = -viewAngle; i <= viewAngle; i++) {
      viewRay.direction = Quaternion.AngleAxis (i, Vector3.up) * viewRay.direction;
      if (Physics.Raycast (viewRay, out hit, viewDistance)) {
        if (hit.collider.gameObject.name == "prey" && HitCloserThanTarget (hit)) {
          target = hit.collider.gameObject;
        }
      }
    }
  }

  private bool HitCloserThanTarget (RaycastHit hit)
  {
    return Vector3.Distance (transform.position, hit.transform.position) < Vector3.Distance (transform.position, target.transform.position);
  }

  void OnCollisionEnter (Collision hit)
  {
    if (hit.gameObject.name == "prey") {
      Destroy (hit.gameObject);
      target = emptyTarget;
    }
    if (hit.gameObject.tag == "environment") {
      transform.rotation = Quaternion.LookRotation(transform.position - hit.collider.gameObject.transform.position);
    }
  }

  public void ResetHeading ()
  {
    transform.Rotate (0, Random.Range (0, 360), 0, Space.World);
  }

  public void Clamp ()
  {
    transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);
    if (transform.position.x > 24 || transform.position.x < -24 || transform.position.z > 24 || transform.position.z < -24) {
      ResetHeading ();
    }
  }
}
