using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using MLAgents;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

public class BasicAgent : Agent
{
    [Header("Specific to Basic")]
    private BasicAcademy academy;
    public float timeBetweenDecisionsAtInference;
    private float timeSinceDecision;
    public int position;
    public int smallGoalPosition;
    public int largeGoalPosition;
    public GameObject largeGoal;
    public GameObject smallGoal;
    public int minPosition;
    public int maxPosition;

    public override void InitializeAgent()
    {
        academy = FindObjectOfType(typeof(BasicAcademy)) as BasicAcademy;
    }

    public override void CollectObservations()
    {
        AddVectorObs(position);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
	{
<<<<<<< HEAD
        float movement = vectorAction[0];
		int direction = 0;
		if (movement == 0) { direction = -1; }
		if (movement == 1) { direction = 1; }

        position += direction;
=======
        var movement = (int)vectorAction[0];
	    
		int direction = 0;
	    
		switch (movement)
		{
		    case 0:
		        direction = -1;
		        break;
		    case 1:
		        direction = 1;
		        break;
		}

	    position += direction;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        if (position < minPosition) { position = minPosition; }
        if (position > maxPosition) { position = maxPosition; }

        gameObject.transform.position = new Vector3(position, 0f, 0f);

        AddReward(-0.01f);

        if (position == smallGoalPosition)
        {
            Done();
            AddReward(0.1f);
        }

        if (position == largeGoalPosition)
        {
            Done();
            AddReward(1f);
        }
    }

    public override void AgentReset()
    {
        position = 0;
        minPosition = -10;
        maxPosition = 10;
        smallGoalPosition = -3;
        largeGoalPosition = 7;
        smallGoal.transform.position = new Vector3(smallGoalPosition, 0f, 0f);
        largeGoal.transform.position = new Vector3(largeGoalPosition, 0f, 0f);
    }

    public override void AgentOnDone()
    {

    }

    public void FixedUpdate()
    {
        WaitTimeInference();
    }

    private void WaitTimeInference()
    {
        if (!academy.GetIsInference())
        {
            RequestDecision();
        }
        else
        {
            if (timeSinceDecision >= timeBetweenDecisionsAtInference)
            {
                timeSinceDecision = 0f;
                RequestDecision();
            }
            else
            {
                timeSinceDecision += Time.fixedDeltaTime;
            }
        }
    }

}
