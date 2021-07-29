
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public static int score = 0;
    public int livesLeft = 2;

    public GameObject gameover,names;
    public GameObject gamewon,highscore;
    private Vector3 direction;
    private bool alive = true;
    private bool[] cango = new bool[4];
    int count;

    Rigidbody rb2d;
    //Animator animator;
    private BoxCollider cc2d;
    Quaternion rotate;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        cc2d = GetComponent<BoxCollider>();
        rotate = gameObject.transform.rotation;
    }
   

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(count);   
        if (alive)
        {
            //animator.SetFloat("currentSpeed", rb2d.velocity.magnitude);
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (OpenDirection(Vector3.left))
                {
                    direction = Vector3.left;
                    gameObject.transform.rotation = new Quaternion(0, 0, 0, 0); ;
                }
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (OpenDirection(-Vector3.left))
                {
                    direction = -Vector3.left;
                    
                    gameObject.transform.rotation = new Quaternion(0, -1, 0, 0);
                }
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                if (OpenDirection(Vector3.back))
                {
                    direction = Vector3.back;
                    
                    gameObject.transform.rotation = new Quaternion(0, -0.7f, 0, 0.7f);
                }
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                if (OpenDirection(-Vector3.back))
                {
                    direction = -Vector3.back;
                    
                    gameObject.transform.rotation = new Quaternion(0, -0.7f, 0, -0.7f);
                    
                }
            }
            rb2d.velocity = direction * speed;
            
        }
        if (alive == false)
        {
            highscore.SetActive(true);
            gameover.SetActive(true);
            
            
        }
        if (Input.GetKey(KeyCode.Space) && gameover.activeSelf)
        {
            highscore.SetActive(true);
            RestartGame();
            score = 0;
            
        }
        if (Input.GetKey(KeyCode.Space) && gamewon.activeSelf)
        {
            RestartGame();
            
        }

        count = GameObject.FindGameObjectsWithTag("Pills").Length;
        if (count == 0)
        {
            gamewon.SetActive(true);
            rb2d.velocity = Vector3.zero;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Ghosts"))
            {
                obj.GetComponent<NavMeshAgent>().speed = 0;
            }
        }


    }

    public void addPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        //scoreText.text = "" + score;
    }

    public void setAlive(bool isAlive)
    {
        alive = isAlive;
        //animator.SetBool("alive", alive);
        rb2d.velocity = Vector3.zero;
    }

    private bool OpenDirection(Vector3 direct)
    {
        /*RaycastHit hit;
        Ray check = new Ray(this.transform.position, direct);
        if(cc2d.Raycast(check,out hit,1f) && hit.collider.tag == "Level")
        {
            return false;
        }
        return true;
        if(Physics.Raycast(check,out hit,1f) && hit.collider.tag == "Level")
        {
            
            return false;

        }
        return true;*/
        return true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Super Pill")
        {
            addPoints(200);
            foreach (GameObject ghost in GameObject.FindGameObjectsWithTag("Ghosts"))
            {
                ghost.GetComponent<GhostController>().SetEaten(true);
            }
            Destroy(other.gameObject);
            GhostController.carrot = true;

        }
        if (other.gameObject.tag == "Pills")
        {
            addPoints(100);
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Teleporter")
        {
            if(other.gameObject.transform.position.x > 10)
            {
                Debug.Log("Truth I guess");
                gameObject.transform.position = new Vector3(-5f, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            if (other.gameObject.transform.position.x < -5)
            {
                Debug.Log("Truth I guess");
                gameObject.transform.position = new Vector3(10f, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
