using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    // Start is called before the first frame update
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

   

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        myRigidbody2D.AddForce(direction*force);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.gameObject.GetComponent<EnemyController>();
        if (e!=null)
        {
            e.Fix();
        }
            
        
        
        
        Destroy(gameObject);
    }
}
