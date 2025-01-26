using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Rigidbody2D rigidbody2D;

    [SerializeField] private float shootLimit = 3f;

    [SerializeField] private float shootForce = 10f;

    private Camera camera;

    private bool isShooting = false;

    private int shoots = 0;

    private bool ballTouchedRim = false;

    Vector3 MousePosition => camera.ScreenToWorldPoint(Input.mousePosition);
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
        lineRenderer.enabled = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.manager.gameActive) {
        if(Input.GetMouseButtonDown(0) && !isShooting) {
            StartShoot();
        }

        if(isShooting) {
            ShootTheBall();
        }

        if(Input.GetMouseButtonUp(0) && isShooting) {
            StopShoot();
        }
    }
    }

    void StartShoot() {
        isShooting = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, MousePosition);
    }

    void ShootTheBall() {
        Vector3 startPosition = lineRenderer.GetPosition(0);
        Vector3 currentPosition = MousePosition;

        Vector3 distance = currentPosition - startPosition;

        if(distance.magnitude <=shootLimit) {
            lineRenderer.SetPosition(1, new Vector3(currentPosition.x, currentPosition.y, -1f)); 
        } else {
            Vector3 limitShoot = startPosition + (distance.normalized * shootLimit);
            lineRenderer.SetPosition(1, new Vector3(limitShoot.x, limitShoot.y, -1f));
        }
    }

    void StopShoot() {
        isShooting = false;
        lineRenderer.enabled = false;

        Vector3 startPosition = lineRenderer.GetPosition(0);
        Vector3 endPosition = lineRenderer.GetPosition(1);

        Vector3 distance = endPosition - startPosition;
        Vector3 ballForce = distance * shootForce;

        rigidbody2D.AddForce(-ballForce, ForceMode2D.Impulse);

    }
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     //Debug.Log("Ball hit the ring");
    //     if (other.gameObject.CompareTag("Net"))
    //     {
    //         // Ball hit the ring
    //         //ScoreManager.Instance.AddScore(1);

    //         Debug.Log("Ball hit the ring");
    //     }
    // }

    //  void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Ball hit the ring");
    //     if (collision.gameObject.CompareTag("Circle"))
    //     {
    //         // Ball hit the ring
    //         //ScoreManager.Instance.AddScore(1);

    //         Debug.Log("Ball hit the ring");
    //     }
    // }

//    void OnTriggerEnter2D(Collider2D collider)
// {
//     if (collider.gameObject.CompareTag("Net"))
//     {
//         shoots++;
//         Debug.Log("Ball went through the net: " + shoots);
//     }

    //  private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Rim"))
    //     {
    //         //ballTouchedRim = true;
    //          shoots++;
    //         Debug.Log("Ball went through the net: " + shoots);
    //     }
    // }

    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if (collider.gameObject.CompareTag("NetCollider") && ballTouchedRim)
    //     {
    //         shoots++;
    //         Debug.Log("Ball went through the net: " + shoots);
    //         ballTouchedRim = false;
    //     }
    // }


}
