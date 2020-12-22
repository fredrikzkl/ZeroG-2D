using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float initialForceY = 250f;

    private Rigidbody2D body;

    private bool takenOff;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        takenOff = false;
    }

    private void Update()
    {
        if (!takenOff)
        {
            Vector2 intialForce = new Vector2(0, initialForceY);
            body.AddForce(intialForce);
            takenOff = true;
        }
    }

}
