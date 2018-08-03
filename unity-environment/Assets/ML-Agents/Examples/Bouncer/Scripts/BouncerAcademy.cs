using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using MLAgents;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

public class BouncerAcademy : Academy {

    public float gravityMultiplier = 1f;

    public override void InitializeAcademy()
    {
        Physics.gravity = new Vector3(0,-9.8f*gravityMultiplier,0);
    }

    public override void AcademyReset()
    {


    }

    public override void AcademyStep()
    {


    }

}
