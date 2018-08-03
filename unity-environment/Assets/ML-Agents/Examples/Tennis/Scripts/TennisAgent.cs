using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
=======
using MLAgents;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

public class TennisAgent : Agent
{
    [Header("Specific to Tennis")]
    public GameObject ball;
    public bool invertX;
<<<<<<< HEAD
    public float invertMult;
=======
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    public int score;
    public GameObject scoreText;
    public GameObject myArea;
    public GameObject opponent;

<<<<<<< HEAD
    public override void CollectObservations()
    {
        AddVectorObs(invertMult * (gameObject.transform.position.x - myArea.transform.position.x));
        AddVectorObs(gameObject.transform.position.y - myArea.transform.position.y);
        AddVectorObs(invertMult * gameObject.GetComponent<Rigidbody>().velocity.x);
        AddVectorObs(gameObject.GetComponent<Rigidbody>().velocity.y);

        AddVectorObs(invertMult * (ball.transform.position.x - myArea.transform.position.x));
        AddVectorObs(ball.transform.position.y - myArea.transform.position.y);
        AddVectorObs(invertMult * ball.GetComponent<Rigidbody>().velocity.x);
        AddVectorObs(ball.GetComponent<Rigidbody>().velocity.y);
    }

    // to be implemented by the developer
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        float moveX = 0.0f;
        float moveY = 0.0f;
        moveX = 0.25f * Mathf.Clamp(vectorAction[0], -1f, 1f) * invertMult;
        if (Mathf.Clamp(vectorAction[1], -1f, 1f) > 0f && gameObject.transform.position.y - transform.parent.transform.position.y < -1.5f)
        {
            moveY = 0.5f;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, moveY * 12f, 0f);
        }

        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(moveX * 50f, GetComponent<Rigidbody>().velocity.y, 0f);

        if (invertX)
        {
            if (gameObject.transform.position.x - transform.parent.transform.position.x < -invertMult)
            {
                gameObject.transform.position = new Vector3(-invertMult + transform.parent.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
        else
        {
            if (gameObject.transform.position.x - transform.parent.transform.position.x > -invertMult)
            {
                gameObject.transform.position = new Vector3(-invertMult + transform.parent.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }

        scoreText.GetComponent<Text>().text = score.ToString();
    }

    // to be implemented by the developer
    public override void AgentReset()
    {
        if (invertX)
        {
            invertMult = -1f;
        }
        else
        {
            invertMult = 1f;
        }

        gameObject.transform.position = new Vector3(-(invertMult) * Random.Range(6f, 8f), -1.5f, 0f) + transform.parent.transform.position;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
=======
    private Text textComponent;
    private Rigidbody agentRb;
    private Rigidbody ballRb;
    private float invertMult;

    public override void InitializeAgent()
    {
        agentRb = GetComponent<Rigidbody>();
        ballRb = GetComponent<Rigidbody>();
        textComponent = scoreText.GetComponent<Text>();
    }

    public override void CollectObservations()
    {
        AddVectorObs(invertMult * (transform.position.x - myArea.transform.position.x));
        AddVectorObs(transform.position.y - myArea.transform.position.y);
        AddVectorObs(invertMult * agentRb.velocity.x);
        AddVectorObs(agentRb.velocity.y);

        AddVectorObs(invertMult * (ball.transform.position.x - myArea.transform.position.x));
        AddVectorObs(ball.transform.position.y - myArea.transform.position.y);
        AddVectorObs(invertMult * ballRb.velocity.x);
        AddVectorObs(ballRb.velocity.y);
    }


    public override void AgentAction(float[] vectorAction, string textAction)
    {
        var moveX = Mathf.Clamp(vectorAction[0], -1f, 1f) * invertMult;
        var moveY = Mathf.Clamp(vectorAction[1], -1f, 1f);
        
        if (moveY > 0.5 && transform.position.y - transform.parent.transform.position.y < -1.5f)
        {
            agentRb.velocity = new Vector3(agentRb.velocity.x, 7f, 0f);
        }

        agentRb.velocity = new Vector3(moveX * 30f, agentRb.velocity.y, 0f);

        if (invertX && transform.position.x - transform.parent.transform.position.x < -invertMult || 
            !invertX && transform.position.x - transform.parent.transform.position.x > -invertMult)
        {
                transform.position = new Vector3(-invertMult + transform.parent.transform.position.x, 
                                                            transform.position.y, 
                                                            transform.position.z);
        }

        textComponent.text = score.ToString();
    }

    public override void AgentReset()
    {
        invertMult = invertX ? -1f : 1f;

        transform.position = new Vector3(-invertMult * Random.Range(6f, 8f), -1.5f, 0f) + transform.parent.transform.position;
        agentRb.velocity = new Vector3(0f, 0f, 0f);
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }
}
