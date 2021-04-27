using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{

    ArrayList objectBodyList;

    [SerializeField]
    private float explodeFactor = 1;



    // Start is called before the first frame update
    void Start()
    {
        objectBodyList = new ArrayList();

        objectBodyList.AddRange(gameObject.GetComponentsInChildren<Rigidbody2D>(true));
        print(objectBodyList.Count);
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(Vector2 parentVelocity)
    {
        foreach (Rigidbody2D rb in objectBodyList)
        {
            rb.gameObject.SetActive(true);
            rb.velocity = parentVelocity * -explodeFactor * Random.Range(-0.8f, 0.8f);
        }
    }




}
