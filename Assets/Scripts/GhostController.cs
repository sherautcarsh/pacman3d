using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    [SerializeField]
    private Transform[] destinations;
    [SerializeField]
    private Transform spawningpos;
    [SerializeField]
    private NavMeshAgent agent;
    private Timer catchtimer;
    private Timer spreadtimer;
    private Timer initialbreak;
    private Timer eatentimer, starttimer;
    [SerializeField]
    private GameObject player, blue1, blue2,eyes;
    private Material material1,material2;
    private bool alldone = false, catcht;
    private int i = 1;
    [SerializeField]
    private float timecatch, timespread, initialtime, eatentime, starttime;
    private bool eaten = false;
    private bool alreadyeaten;
    bool x = true;
    Timer timer;
    bool onceinawhile = true;
    bool onetime = true,twotime = false;
    public static bool carrot;


    // Start is called before the first frame update
    void Start()
    {
        catchtimer = gameObject.AddComponent<Timer>();
        catchtimer.Duration = timecatch;
        catcht = false;
        spreadtimer = gameObject.AddComponent<Timer>();
        spreadtimer.Duration = timespread;
        spreadtimer.Run();
        initialbreak = gameObject.AddComponent<Timer>();
        initialbreak.Duration = initialtime;
        initialbreak.Run();
        eatentimer = gameObject.AddComponent<Timer>();
        eatentimer.Duration = 6;
        starttimer = gameObject.AddComponent<Timer>();
        starttimer.Duration = 5;
        starttimer.Run();
        agent.updateRotation = false;
        timer = gameObject.AddComponent<Timer>();
        spawningpos.position = gameObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            if (!spreadtimer.Finished && !alreadyeaten)
            {

                switch (i)
                {
                    case 1:

                        agent.SetDestination(destinations[0].position);
                        break;
                    case 2:
                        agent.SetDestination(destinations[1].position);
                        break;
                    case 3:
                        agent.SetDestination(destinations[2].position);
                        break;
                    case 4:
                        agent.SetDestination(destinations[3].position);
                        break;
                    case 5:
                        agent.SetDestination(destinations[4].position);
                        break;
                    case 6:
                        agent.SetDestination(destinations[5].position);
                        break;
                    case 7:
                        agent.SetDestination(destinations[6].position);
                        break;
                    case 8:
                        agent.SetDestination(destinations[7].position);
                        break;
                    default:
                        alldone = true;
                        break;
                }
            }

            if (spreadtimer.Finished)
            {
                if (onetime)
                    catchtimer.Run();
                catcht = true;
                onetime = false;
                twotime = true;
            }
            if (catcht & !alreadyeaten)
            {
                agent.SetDestination(player.transform.position);
            }
            if (catchtimer.Finished)
            {
                if (twotime)
                {
                    spreadtimer.Run();
                    catcht = false;
                    onetime = true;
                    twotime = false;
                }
            }
            
            
            if (eaten && !alreadyeaten)
            {
                if (eatentimer.Running)
                {
                    if (carrot)
                    {
                        blue1.SetActive(false);
                        onceinawhile = true;
                        carrot = false;
                    }
                }
                MeshRenderer[] mesh = gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mes in mesh)
                {
                    mes.enabled = false;
                }
                blue1.SetActive(true);
                if (onceinawhile)
                {
                    eatentimer.Run();
                    onceinawhile = false;
                }
            }
            if (eatentimer.Finished && !alreadyeaten)
            {
                MeshRenderer[] mesh = gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mes in mesh)
                {
                    mes.enabled = true;
                }
                onceinawhile = true;
                blue1.SetActive(false);
                eaten = false;
            }
            if (timer.Finished)
            {
                x = true;
            }
            
        }
        if (alldone)
        {
            i = 1;
            alldone = false;
        }
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Playe")
        {            
            if (!eaten && !alreadyeaten)
            {
                Debug.Log("Kills");
                player.GetComponent<PlayerController>().setAlive(false);
                foreach(MeshCollider collid in player.GetComponentsInChildren<MeshCollider>())
                {
                    collid.enabled = false;
                }
                foreach (MeshRenderer collid in player.GetComponentsInChildren<MeshRenderer>())
                {
                    collid.enabled = false;
                }
                foreach ( GameObject ghost in GameObject.FindGameObjectsWithTag("Ghosts"))
                    ghost.GetComponent<NavMeshAgent>().speed = 0;
            }
            if (eaten)
            {
                Debug.Log("Will I ever");
                blue1.SetActive(false);
                agent.SetDestination(spawningpos.position);
                alreadyeaten = true;
                eaten = false;
                onceinawhile = true;
                player.GetComponent<PlayerController>().addPoints(250);
                eyes.GetComponent<MeshRenderer>().enabled = true;
                gameObject.layer = 10;
                foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true))
                {
                    child.gameObject.layer = 10;  // add any layer you want. 
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destinations")
        {
            if (x)
            {
                i++;
                x = false;
                timer.Duration = 3;
                timer.Run();
            }
        }
        if(other.gameObject.tag == "Spawn")
        {
            if (starttimer.Finished)
            {
                alreadyeaten = false;
                MeshRenderer[] mesh = gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mes in mesh)
                {
                    mes.enabled = true;
                }
                blue1.SetActive(false);
                gameObject.layer = 9;
                foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true))
                {
                    child.gameObject.layer = 9;  // add any layer you want. 
                }
            }
        }
    }

    public void SetEaten(bool eat)
    {
        eaten = eat;
    }
}
