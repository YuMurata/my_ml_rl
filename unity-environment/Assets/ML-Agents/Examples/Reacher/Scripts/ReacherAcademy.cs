using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using MLAgents;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

public class ReacherAcademy : Academy {

    public float goalSize;
    public float goalSpeed;


    public override void AcademyReset()
    {
        goalSize = (float)resetParameters["goal_size"];
        goalSpeed = (float)resetParameters["goal_speed"];
    }

    public override void AcademyStep()
    {


    }

}
