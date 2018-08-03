using UnityEngine;
<<<<<<< HEAD
=======
using MLAgents;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

public class ReacherAgent : Agent {

    public GameObject pendulumA;
    public GameObject pendulumB;
    public GameObject hand;
    public GameObject goal;
<<<<<<< HEAD
    float goalDegree;
    Rigidbody rbA;
    Rigidbody rbB;
    float goalSpeed;
=======
    private ReacherAcademy myAcademy;
    float goalDegree;
    private Rigidbody rbA;
    private Rigidbody rbB;
    private float goalSpeed;
    private float goalSize;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

    /// <summary>
    /// Collect the rigidbodies of the reacher in order to resue them for 
    /// observations and actions.
    /// </summary>
    public override void InitializeAgent()
    {
        rbA = pendulumA.GetComponent<Rigidbody>();
        rbB = pendulumB.GetComponent<Rigidbody>();
<<<<<<< HEAD
=======
        myAcademy = GameObject.Find("Academy").GetComponent<ReacherAcademy>();
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }

    /// <summary>
    /// We collect the normalized rotations, angularal velocities, and velocities of both
    /// limbs of the reacher as well as the relative position of the target and hand.
    /// </summary>
    public override void CollectObservations()
    {
<<<<<<< HEAD
=======
        AddVectorObs(pendulumA.transform.localPosition);
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        AddVectorObs(pendulumA.transform.rotation);
        AddVectorObs(rbA.angularVelocity);
        AddVectorObs(rbA.velocity);

<<<<<<< HEAD
=======
        AddVectorObs(pendulumB.transform.localPosition);
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        AddVectorObs(pendulumB.transform.rotation);
        AddVectorObs(rbB.angularVelocity);
        AddVectorObs(rbB.velocity);

<<<<<<< HEAD
        Vector3 localGoalPosition = goal.transform.position - transform.position;
        AddVectorObs(localGoalPosition);

        Vector3 localHandPosition = hand.transform.position - transform.position;
        AddVectorObs(localHandPosition);
=======
        AddVectorObs(goal.transform.localPosition);
        AddVectorObs(hand.transform.localPosition);
        
        AddVectorObs(goalSpeed);
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
	}

    /// <summary>
    /// The agent's four actions correspond to torques on each of the two joints.
    /// </summary>
    public override void AgentAction(float[] vectorAction, string textAction)
	{
        goalDegree += goalSpeed;
        UpdateGoalPosition();

<<<<<<< HEAD
        float torque_x = Mathf.Clamp(vectorAction[0], -1, 1) * 100f;
        float torque_z = Mathf.Clamp(vectorAction[1], -1, 1) * 100f;
        rbA.AddTorque(new Vector3(torque_x, 0f, torque_z));

        torque_x = Mathf.Clamp(vectorAction[2], -1, 1) * 100f;
        torque_z = Mathf.Clamp(vectorAction[3], -1, 1) * 100f;
        rbB.AddTorque(new Vector3(torque_x, 0f, torque_z));
=======
        var torqueX = Mathf.Clamp(vectorAction[0], -1f, 1f) * 150f;
        var torqueZ = Mathf.Clamp(vectorAction[1], -1f, 1f) * 150f;
        rbA.AddTorque(new Vector3(torqueX, 0f, torqueZ));

        torqueX = Mathf.Clamp(vectorAction[2], -1f, 1f) * 150f;
        torqueZ = Mathf.Clamp(vectorAction[3], -1f, 1f) * 150f;
        rbB.AddTorque(new Vector3(torqueX, 0f, torqueZ));
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
	}

    /// <summary>
    /// Used to move the position of the target goal around the agent.
    /// </summary>
<<<<<<< HEAD
    void UpdateGoalPosition() {
        float radians = (goalDegree * Mathf.PI) / 180f;
        float goalX = 8f * Mathf.Cos(radians);
        float goalY = 8f * Mathf.Sin(radians);

=======
    void UpdateGoalPosition() 
    {
        var radians = goalDegree * Mathf.PI / 180f;
        var goalX = 8f * Mathf.Cos(radians);
        var goalY = 8f * Mathf.Sin(radians);
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        goal.transform.position = new Vector3(goalY, -1f, goalX) + transform.position;
    }

    /// <summary>
    /// Resets the position and velocity of the agent and the goal.
    /// </summary>
    public override void AgentReset()
    {
        pendulumA.transform.position = new Vector3(0f, -4f, 0f) + transform.position;
        pendulumA.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
<<<<<<< HEAD
        rbA.velocity = new Vector3(0f, 0f, 0f);
        rbA.angularVelocity = new Vector3(0f, 0f, 0f);

        pendulumB.transform.position = new Vector3(0f, -10f, 0f) + transform.position;
        pendulumB.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        rbB.velocity = new Vector3(0f, 0f, 0f);
        rbB.angularVelocity = new Vector3(0f, 0f, 0f);

=======
        rbA.velocity = Vector3.zero;
        rbA.angularVelocity = Vector3.zero;

        pendulumB.transform.position = new Vector3(0f, -10f, 0f) + transform.position;
        pendulumB.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        rbB.velocity = Vector3.zero;
        rbB.angularVelocity = Vector3.zero;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

        goalDegree = Random.Range(0, 360);
        UpdateGoalPosition();

<<<<<<< HEAD
        ReacherAcademy academy = GameObject.Find("Academy").GetComponent<ReacherAcademy>();
        float goalSize = academy.goalSize;
        goalSpeed = academy.goalSpeed;
=======
        goalSize = myAcademy.goalSize;
        goalSpeed = Random.Range(-1f, 1f) * myAcademy.goalSpeed;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

        goal.transform.localScale = new Vector3(goalSize, goalSize, goalSize);
    }
}
