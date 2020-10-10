using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayer : MonoBehaviour
{
    private float timer = 4f;

    [SerializeField]private GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Talk()
    {
      text.SetActive(true);
      yield return new WaitForSeconds(4);
      text.SetActive(false);
    }

    public void DisplayTalk()
    {
        StartCoroutine(Talk());
    }
}
