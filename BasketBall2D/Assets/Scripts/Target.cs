using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float lifeTime = 1f;
    void Start()
    {
        StartCoroutine(DestroyTarget());
    }

    IEnumerator DestroyTarget() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ball")) {
            GameManager.manager.ScorePoint();
            //StartCoroutine(GameManager.manager.InsertTargets());
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
