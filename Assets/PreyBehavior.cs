using UnityEngine;
using System.Collections;

public class PreyBehavior : MonoBehaviour
{
  public float speed;
  private int viewDistance = 5;
  private RaycastHit hit;
  private Ray viewRay;

  void Start ()
  {
    viewRay = new Ray (transform.position, transform.forward);
    speed = 0.1f;
    name = "prey";
    transform.position = new Vector3 (Random.Range (-10, 10), 0, Random.Range (-10, 10));
    ResetHeading ();
  }

  public void Update ()
  {
    transform.Translate (speed * transform.forward, Space.World);
    Clamp ();
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
