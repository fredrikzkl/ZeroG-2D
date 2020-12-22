using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 startPos;

    public float maxPower = 110f;
    public float clickCloseFactor = 15f;
    private Rigidbody2D body;
    

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
        body = gameObject.GetComponent<Rigidbody2D>();
        Reset();
    }

    void Reset()
    {
        gameObject.transform.position = startPos;
        body.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            Debug.Log("Horitonzal");
        }

        //Reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
            return;
        }

        //Stop floating
        if (Input.GetKeyDown(KeyCode.S))
        {
            body.velocity = Vector2.zero;
            return;
        }



        //Click
        if (Input.GetButtonDown("Fire1"))
        {
            body.AddForce(GetForce());
            return;
        }
    }

    Vector2 GetForce()
    {
        Vector2 mousePos = GetRealMouseWorldPosition();
        Vector2 playerPos = gameObject.transform.position;

        float deltaX = GetDistance(mousePos.x, playerPos.x);
        float deltaY = GetDistance(mousePos.y, playerPos.y);

        float powerX = GetPower(deltaX);
        float powerY = GetPower(deltaY);

        Debug.Log("MousePosition: " + mousePos.x + " | playerPos: " + playerPos.x + " | Delta: "  + deltaX);

        return new Vector2(powerX, powerY);
    }

    float GetPower(float delta)
    {
        float tempPower = maxPower;
        if (delta < 0) tempPower *= -1;
        return tempPower - (delta * clickCloseFactor);
    }


    float GetDistance(float x1, float x2)
    {
        if(x1 < x2)
        {
            return -(x1 - x2);
        }
        return (x2 - x1);
    }


    Vector2 GetRealMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

}
