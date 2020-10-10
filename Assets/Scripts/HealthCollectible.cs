using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip collectedSound;
    
    [SerializeField] ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Rubycontroller>())
        {
            other.gameObject.GetComponent<Rubycontroller>().ChangeHealth(1f);
            Instantiate(particleSystem, transform.position, quaternion.identity);
            other.gameObject.GetComponent<Rubycontroller>().PlaySound(collectedSound);
            Destroy(gameObject);
        }
    }
}
