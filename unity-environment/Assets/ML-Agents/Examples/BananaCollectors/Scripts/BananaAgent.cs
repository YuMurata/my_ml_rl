using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using MLAgents;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

public class BananaAgent : Agent
{
    public GameObject myAcademyObj;
    BananaAcademy myAcademy;
    public GameObject area;
    BananaArea myArea;
    bool frozen;
    bool poisioned;
    bool satiated;
    bool shoot;
    float frozenTime;
    float effectTime;
<<<<<<< HEAD
    Rigidbody agentRB;
=======
    Rigidbody agentRb;
    private int bananas;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

    // Speed of agent rotation.
    public float turnSpeed = 300;

    // Speed of agent movement.
    public float moveSpeed = 2;
    public Material normalMaterial;
    public Material badMaterial;
    public Material goodMaterial;
<<<<<<< HEAD
    int bananas;
    public GameObject myLaser;
    public bool contribute;
    RayPerception rayPer;
=======
    public Material frozenMaterial;
    public GameObject myLaser;
    public bool contribute;
    private RayPerception rayPer;
    public bool useVectorObs;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

    public override void InitializeAgent()
    {
        base.InitializeAgent();
<<<<<<< HEAD
        agentRB = GetComponent<Rigidbody>();
=======
        agentRb = GetComponent<Rigidbody>();
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        Monitor.verticalOffset = 1f;
        myArea = area.GetComponent<BananaArea>();
        rayPer = GetComponent<RayPerception>();
        myAcademy = myAcademyObj.GetComponent<BananaAcademy>();
    }

    public override void CollectObservations()
    {
<<<<<<< HEAD
        float rayDistance = 50f;
        float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
        string[] detectableObjects = { "banana", "agent", "wall", "badBanana", "frozenAgent" };
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        Vector3 localVelocity = transform.InverseTransformDirection(agentRB.velocity);
        AddVectorObs(localVelocity.x);
        AddVectorObs(localVelocity.z);
        AddVectorObs(System.Convert.ToInt32(frozen));
        AddVectorObs(System.Convert.ToInt32(shoot));
    }

    public Color32 ToColor(int HexVal)
    {
        byte R = (byte)((HexVal >> 16) & 0xFF);
        byte G = (byte)((HexVal >> 8) & 0xFF);
        byte B = (byte)((HexVal) & 0xFF);
        return new Color32(R, G, B, 255);
=======
        if (useVectorObs)
        {
            float rayDistance = 50f;
            float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
            string[] detectableObjects = { "banana", "agent", "wall", "badBanana", "frozenAgent" };
            AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
            Vector3 localVelocity = transform.InverseTransformDirection(agentRb.velocity);
            AddVectorObs(localVelocity.x);
            AddVectorObs(localVelocity.z);
            AddVectorObs(System.Convert.ToInt32(frozen));
            AddVectorObs(System.Convert.ToInt32(shoot));
        }
    }

    public Color32 ToColor(int hexVal)
    {
        byte r = (byte)((hexVal >> 16) & 0xFF);
        byte g = (byte)((hexVal >> 8) & 0xFF);
        byte b = (byte)(hexVal & 0xFF);
        return new Color32(r, g, b, 255);
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }

    public void MoveAgent(float[] act)
    {
        shoot = false;

        if (Time.time > frozenTime + 4f && frozen)
        {
            Unfreeze();
        }
        if (Time.time > effectTime + 0.5f)
        {
            if (poisioned)
            {
                Unpoison();
            }
            if (satiated)
            {
                Unsatiate();
            }
        }

        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

<<<<<<< HEAD

=======
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        if (!frozen)
        {
            bool shootCommand = false;
            if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
            {
                dirToGo = transform.forward * Mathf.Clamp(act[0], -1f, 1f);
                rotateDir = transform.up * Mathf.Clamp(act[1], -1f, 1f);
<<<<<<< HEAD
                shootCommand = Mathf.Clamp(act[2], 0f, 1f) > 0.5f;
            }
            else
            {
                switch ((int)(act[0]))
=======
                shootCommand = Mathf.Clamp(act[2], -1f, 1f) > 0.5f;
            }
            else
            {
                switch ((int)act[0])
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                {
                    case 1:
                        dirToGo = transform.forward;
                        break;
                    case 2:
                        shootCommand = true;
                        break;
                    case 3:
                        rotateDir = -transform.up;
                        break;
                    case 4:
                        rotateDir = transform.up;
                        break;
                }
            }
            if (shootCommand)
            {
                shoot = true;
                dirToGo *= 0.5f;
<<<<<<< HEAD
                agentRB.velocity *= 0.75f;
            }
            agentRB.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
            transform.Rotate(rotateDir, Time.fixedDeltaTime * turnSpeed);
        }

        if (agentRB.velocity.sqrMagnitude > 25f) // slow it down
        {
            agentRB.velocity *= 0.95f;
=======
                agentRb.velocity *= 0.75f;
            }
            agentRb.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
            transform.Rotate(rotateDir, Time.fixedDeltaTime * turnSpeed);
        }

        if (agentRb.velocity.sqrMagnitude > 25f) // slow it down
        {
            agentRb.velocity *= 0.95f;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        }

        if (shoot)
        {
            myLaser.transform.localScale = new Vector3(1f, 1f, 1f);
            Vector3 position = transform.TransformDirection(RayPerception.PolarToCartesian(25f, 90f));
            Debug.DrawRay(transform.position, position, Color.red, 0f, true);
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 2f, position, out hit, 25f))
            {
<<<<<<< HEAD
                if (hit.collider.gameObject.tag == "agent")
=======
                if (hit.collider.gameObject.CompareTag("agent"))
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                {
                    hit.collider.gameObject.GetComponent<BananaAgent>().Freeze();
                }
            }
        }
        else
        {
            myLaser.transform.localScale = new Vector3(0f, 0f, 0f);

        }
    }


    void Freeze()
    {
        gameObject.tag = "frozenAgent";
        frozen = true;
        frozenTime = Time.time;
<<<<<<< HEAD
        gameObject.GetComponent<Renderer>().material.color = Color.black;
=======
        gameObject.GetComponent<Renderer>().material = frozenMaterial;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }


    void Unfreeze()
    {
        frozen = false;
        gameObject.tag = "agent";
        gameObject.GetComponent<Renderer>().material = normalMaterial;
    }

    void Poison()
    {
        poisioned = true;
        effectTime = Time.time;
        gameObject.GetComponent<Renderer>().material = badMaterial;
    }

    void Unpoison()
    {
        poisioned = false;
        gameObject.GetComponent<Renderer>().material = normalMaterial;
    }

    void Satiate()
    {
        satiated = true;
        effectTime = Time.time;
        gameObject.GetComponent<Renderer>().material = goodMaterial;
    }

    void Unsatiate()
    {
        satiated = false;
        gameObject.GetComponent<Renderer>().material = normalMaterial;
    }



    public override void AgentAction(float[] vectorAction, string textAction)
    {
        MoveAgent(vectorAction);
    }

    public override void AgentReset()
    {
        Unfreeze();
        Unpoison();
        Unsatiate();
        shoot = false;
<<<<<<< HEAD
        agentRB.velocity = Vector3.zero;
=======
        agentRb.velocity = Vector3.zero;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        bananas = 0;
        myLaser.transform.localScale = new Vector3(0f, 0f, 0f);
        transform.position = new Vector3(Random.Range(-myArea.range, myArea.range),
                                         2f, Random.Range(-myArea.range, myArea.range))
            + area.transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("banana"))
        {
            Satiate();
            collision.gameObject.GetComponent<BananaLogic>().OnEaten();
            AddReward(1f);
            bananas += 1;
            if (contribute)
            {
                myAcademy.totalScore += 1;
            }
        }
        if (collision.gameObject.CompareTag("badBanana"))
        {
            Poison();
            collision.gameObject.GetComponent<BananaLogic>().OnEaten();

            AddReward(-1f);
            if (contribute)
            {
                myAcademy.totalScore -= 1;
            }
        }
<<<<<<< HEAD
        if (collision.gameObject.CompareTag("wall"))
        {
            Done();
        }
=======
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }

    public override void AgentOnDone()
    {

    }
}
