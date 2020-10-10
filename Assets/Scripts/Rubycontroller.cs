using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Rubycontroller : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    private Rigidbody2D myRigidbody2D;
    private float horizontalDelta;
    private float verticalDelta;
    private Vector2 move;
    private float health;
    private float max_Health = 3f;
    public float invincibleTime = 3f;
    public bool isInvicinble = false;
    private Animator animator;
    private float horizontalFace, verticalFace;
    [SerializeField] public GameObject bullet;
    private AudioSource audioSource;
    [SerializeField] private AudioClip HitSound;
    [SerializeField] private AudioClip throwSound;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        horizontalDelta = 0f;
        verticalDelta = 0f;
        health = max_Health;
        animator = GetComponent<Animator>();
        horizontalFace = 0f;
        verticalFace = -0.5f;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalDelta = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        verticalDelta = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        if (Input.GetAxis("Horizontal") > Mathf.Epsilon)
        {
            horizontalFace = 0.5f;
            verticalFace = 0f;
            
        }
        else if(horizontalDelta<0f)
        {
            horizontalFace = -0.5f;
            verticalFace = 0f;
        }
        
        if (Input.GetAxis("Vertical") > Mathf.Epsilon)
        {
            horizontalFace = 0f;
            verticalFace = 0.5f;
        }
        else if(verticalDelta<0f)
        {
            horizontalFace = 0f;
            verticalFace = -0.5f;
        }

        if (Input.GetAxis("Horizontal") > Mathf.Epsilon && Input.GetAxis("Vertical") > Mathf.Epsilon)
        {
            verticalFace = 0.5f;
            horizontalFace = 0.5f;
        }

       
        

        Vector2 movementDir=new Vector2(horizontalDelta,verticalDelta);
        animator.SetFloat("LookX", horizontalFace);
        animator.SetFloat("LookY", verticalFace);
        if(!Mathf.Approximately(horizontalDelta+verticalDelta,0.0f) )
        animator.SetFloat("Running",movementDir.magnitude);
        else
        {
            animator.SetFloat("Running",-1f);
        }
       move = new Vector2(myRigidbody2D.position.x+horizontalDelta, myRigidbody2D.position.y+verticalDelta);
       myRigidbody2D.MovePosition(move);
       if (health <= 0)
       {
           Destroy(gameObject);
       }
         
       if (isInvicinble)
       {
           invincibleTime -= Time.deltaTime;
           if (invincibleTime <= 0)
           {
               invincibleTime = 3f;
               isInvicinble = false;
           }

       }
       if(Input.GetKeyDown(KeyCode.Space))
       {
           Launch();
       }
       if (Input.GetKeyDown(KeyCode.X))
       {
           RaycastHit2D hit = Physics2D.Raycast(myRigidbody2D.position , movementDir.normalized, 1.5f, LayerMask.GetMask("NPC"));
           if (hit.collider != null)
           {
               hit.collider.GetComponent<NonPlayer>().DisplayTalk();
           }
       }
    }

  

    public void ChangeHealth(float amount)
    {
        if(amount>0)
        health += amount;
        
        Mathf.Clamp(health, 0, max_Health);
        if (amount < 0 && !isInvicinble)
        {
            isInvicinble = true;
            health += amount;
            PlaySound(HitSound);
        }
        UIHealthBar.instance.SetValue(health/(float)max_Health);

    }


    void Launch()
    {
        float hor = horizontalDelta, ver = verticalDelta;
        if (Mathf.Approximately(horizontalDelta, 0.0f) && Mathf.Approximately(verticalDelta, 0.0f))
        {
            hor = 0f;
            ver = 0.5f;
        }
        GameObject projectile=Instantiate(bullet, myRigidbody2D.position, quaternion.identity);
        projectile.GetComponent<Bullet>().Launch(new Vector2(hor,ver).normalized,1000f );
        
        animator.SetTrigger("Launch");
        PlaySound(throwSound);
        




    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip,50f);
        
    }
    
}
