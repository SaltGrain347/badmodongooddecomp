using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class HolidayScript : KOTLIN.Interactions.Interactable
{
    private void Start()
    {
        this.agent = base.GetComponent<NavMeshAgent>(); //Get the agent
        this.audioDevice = base.GetComponent<AudioSource>();
        this.origin = base.transform.position;
        active = true;
        Wander();
    }

    private void Update()
    {
        if (this.coolDown > 0f)
        {
            this.coolDown -= 1f * Time.deltaTime;
        }
        else if (this.coolDown < 0.1f)
            {
            active = true;
        }
        // Ok. If Agent not Moving. Wander again.
        if (agent.velocity.magnitude <= 0.1f)
        {
            this.Wander();
        }
    }

    private void Wander()
    {
        this.wanderer.GetNewTarget(); //Get a new target on the map
        this.agent.SetDestination(this.wanderTarget.position); //Set its destination to position of the wanderTarget
    }

    public override void Interact()
    {
       if (active == true)
        {
            this.audioDevice.PlayOneShot(this.cheer);
            coolDown = maxcooldown;
            int selecteditem = Mathf.RoundToInt(UnityEngine.Random.Range(0f, Items.Length));
            GameControllerScript.Instance.CollectItem(Items[selecteditem]); // yo thanks EchoTyler my goat
            active = false;
        }

    }

    public Transform wanderTarget;

    public AILocationSelectorScript wanderer;

    public float coolDown;

    public float maxcooldown;

    public int[] Items;

    public int wanders;

    public AudioClip cheer;

    public bool active;

    private NavMeshAgent agent;

    private Vector3 origin;

    private AudioSource audioDevice;
}