using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject objec;
    [SerializeField]
    private float velocityy, velocityz,rotationx;
    Vector3 actualp;
    Vector3 actualr;
    bool goingdown, goingup, rotupx, rotdownx;
    // Start is called before the first frame update
    void Start()
    {
        actualp = gameObject.transform.position;
        actualr = gameObject.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.transform.position.y == 12.5 && gameObject.transform.position.z == 14)
        {
            goingdown = true;
            goingup = false;
        }
        if(gameObject.transform.position.y == 8 && gameObject.transform.position.z == 16 )
        {
            goingdown = false;

            goingup = true;
        }
        if(gameObject.transform.position.y > 8 && gameObject.transform.position.z < 16 && goingdown && gameObject.transform.position.y < 12.5 && gameObject.transform.position.z > 14 || ( gameObject.transform.position.y == 12.5 && gameObject.transform.position.z == 14))
        {
            Vector3 pos = new Vector3(0, -velocityy * Time.deltaTime, velocityz * Time.deltaTime);
            actualp = gameObject.transform.position;
            Vector3 change = actualp + pos;
            gameObject.transform.position = change;
        }
        if (gameObject.transform.position.y < 12.5 && gameObject.transform.position.z > 14 && goingup && gameObject.transform.position.y > 8 && gameObject.transform.position.z < 16 || (gameObject.transform.position.y == 8 && gameObject.transform.position.z == 16))
        {
            actualp = gameObject.transform.position;
            Vector3 pos = new Vector3(0, -velocityy * Time.deltaTime, velocityz * Time.deltaTime);
            Vector3 change = actualp - pos;
            gameObject.transform.position = change;
        }
        if(gameObject.transform.rotation.eulerAngles.x == 68)
        {
            rotupx = true;
            rotdownx = false;
        }
        if (gameObject.transform.rotation.eulerAngles.x == 53)
        {
            rotupx = false;
            rotdownx = true;
        }
        if (gameObject.transform.rotation.eulerAngles.x < 68 && rotupx || gameObject.transform.rotation.eulerAngles.x == 68)
        {
            actualr = gameObject.transform.rotation.eulerAngles;
            Vector3 rot = new Vector3(-rotationx, 0, 0);
            Vector3 change;
            change = actualr + rot;
            gameObject.transform.rotation = Quaternion.Euler( change);
            
        }
        if(gameObject.transform.rotation.eulerAngles.x > 53 && rotdownx || gameObject.transform.rotation.eulerAngles.x == 53)
        {
            actualr = gameObject.transform.rotation.eulerAngles;
            Vector3 rot = new Vector3(-rotationx, 0, 0);
            Vector3 change;
            change = actualr - rot;
            gameObject.transform.rotation = Quaternion.Euler(change);
        }
        
    }
}
