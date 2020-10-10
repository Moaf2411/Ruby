using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private float speed = 5f;
    private Animator animator;
    private bool broken = true;

    private float vDelta;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        vDelta = 0f;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            
           
            return;
        }

        vDelta += speed * Time.deltaTime;
        if (vDelta > 8f || vDelta < -8f)
        {
            vDelta = 0f;
            speed *= -1;
            animator.SetFloat("MoveY",math.sign(speed)*0.5f);
        }

        Vector2 newpos=new Vector2(myRigidbody2D.position.x,myRigidbody2D.position.y+speed*Time.deltaTime);
        myRigidbody2D.MovePosition(newpos);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rubycontroller>())
        {
            other.gameObject.GetComponent<Rubycontroller>().ChangeHealth(-1);
            
        }
    }

    public void Fix()
    {
        broken = false;
        myRigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }
}
