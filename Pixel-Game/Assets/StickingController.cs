using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickingController : MonoBehaviour
{

    public Movement movementController;
    public AnimalController parentAnimal;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("StickyPlatform"))
        {
            transform.parent = collision.collider.transform;

            movementController.body.simulated = false;

            parentAnimal = collision.gameObject.GetComponent<AnimalController>();

            parentAnimal.playerSticking = true;
            
        }
    }



}
