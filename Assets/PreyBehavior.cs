using UnityEngine;
using System.Collections;

public class PreyBehavior : MonoBehaviour
{
  public float speed;
  private int viewDistance, viewAngle, runTime, coolDown;
  private RaycastHit hit;
  private Ray viewRay;
  private bool shouldRun, canRun;

  void Start ()
  {
    shouldRun = false;
    canRun = true;
    viewDistance = 5;
    viewAngle = 120;
    viewRay = new Ray (transform.position, transform.forward);
    speed = 0.1f;
    runTime = 60;
    coolDown = 140;
    name = "prey";
    transform.position = new Vector3 (Random.Range (-10, 10), 0, Random.Range (-10, 10));
    ResetHeading ();
  }

  public void Update ()
  {
    for (int i = -viewAngle; i <= viewAngle; i++) {
      Vector3 k = Quaternion.AngleAxis (i, Vector3.up) * transform.forward;
      Debug.DrawRay (transform.position, k * viewDistance);
    }

    SearchForPredator();
    CalculateStaminaAndRunSpeed();

    transform.Translate (speed * transform.forward, Space.World);
    Clamp ();
  }

  private void CalculateStaminaAndRunSpeed()
  {
    if (shouldRun && canRun)
    {
      speed = 0.2f;
      runTime--;
      if (runTime < 0)
      {
        canRun = false;
        coolDown = 140;
      }
    }
    else
    {
      coolDown--;
      if (coolDown < 0)
      {
        canRun = true;
        runTime = 60;
      }
      speed = 0.1f;
    }
  }

  private void SearchForPredator ()
  {
    viewRay.origin = transform.position;
    for (int i = -viewAngle; i <= viewAngle; i++) {
      viewRay.direction = Quaternion.AngleAxis (i, Vector3.up) * viewRay.direction;
      CheckForRaycastHit ();
    }
  }

  void CheckForRaycastHit ()
  {
    if (Physics.Raycast (viewRay, out hit, viewDistance)) {
      HandleHit ();
    }
  }

  private void HandleHit ()
  {
    if (hit.collider.gameObject.name == "predator") {
      transform.rotation = Quaternion.LookRotation(transform.position - hit.collider.gameObject.transform.position);
      shouldRun = true;
    }
  }
  public void OnCollisionEnter (Collision hit)
  {
    if (hit.gameObject.tag == "environment")
      ResetHeading ();
  }

  public void ResetHeading ()
  {
    transform.Rotate (Vector3.up, Random.Range (0, 360));
  }

  public void Clamp ()
  {
    transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);
    if (transform.position.x > 24 || transform.position.x < -24 || transform.position.z > 24 || transform.position.z < -24) {
      ResetHeading ();
    }
  }
}
