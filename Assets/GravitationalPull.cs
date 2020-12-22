using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalPull : MonoBehaviour
{
    //Settings
    public string[] pullTags;
    public bool pullEverything;
    public bool debugging;
    //This object
    Rigidbody2D body;
    Vector2 position;
    //Temps
    private List<GameObject> targetObjects;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        position = gameObject.transform.position;
      
    }

    void Update()
    {
        foreach (var obj in GetAllObjectsProneToGravitationalPull())
        {
            PullObject(obj);
        }
    }


    GameObject[] GetAllObjectsProneToGravitationalPull()
    {
        List<GameObject> objects = new List<GameObject>();
        targetObjects = new List<GameObject>();

        if (pullEverything)
        {
            foreach (var o in FindObjectsOfType<Rigidbody2D>())
            {
                targetObjects.Add(o.gameObject);
            }
        }
        else
        {
            if (pullTags.Length == 0)
                Debug.LogWarning("Pull Tags is empty");
            foreach(var o in GameObject.FindGameObjectsWithTag(tag))
            {
                targetObjects.Add(o);
            }
           
        }
        
        
        foreach(var tag in pullTags)
        {
            foreach(var o in GameObject.FindGameObjectsWithTag(tag))
            {
                if(o != gameObject)
                    objects.Add(o);
            }
        }
        return objects.ToArray();
    }

    void PullObject(GameObject obj)
    {
        var objBody = obj.GetComponent<Rigidbody2D>();
        var objPosition = obj.transform.position;

        float f = GetForce(objBody.mass, body.mass, GetDistanceToObject(objPosition, position));
        float angle = GetAngleToObject(objPosition);
        Vector2 forceVector = new Vector2();
        forceVector.x = -(Mathf.Cos(angle) * f);
        forceVector.y = -(Mathf.Sin(angle) * f);
        if(debugging)
            Debug.Log("Pulling " + obj.name + " with vector: " + forceVector + " | Angle: " + angle );
        objBody.AddForce(forceVector);
    }

    float GetAngleToObject(Vector2 objPosition)
    {
        float deltaX = objPosition.x - position.x;
        float deltaY = objPosition.y - position.y;
        return Mathf.Atan2(deltaY, deltaX);
    }

    float GetForce(float mass1, float mass2, float distance)
    {
        //OG G: 6.67384e-11;
        float G = 6.67384e-4f;
        return G * ((mass1 * mass2) / Mathf.Sqrt(distance));
    }


    float GetDistanceToObject(Vector2 objectPosition, Vector2 pullingObjectPosition)
    {
        float katet_x = Mathf.Pow((objectPosition.x - pullingObjectPosition.x), 2);
        float katet_y = Mathf.Pow((objectPosition.y - pullingObjectPosition.y), 2);
        return Mathf.Sqrt(katet_x + katet_y);
    }
}
