using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using MLAgents;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

public class CrawlerAcademy : Academy
{
    public override void InitializeAcademy()
    {
        Monitor.verticalOffset = 1f;
<<<<<<< HEAD
=======
        Physics.defaultSolverIterations = 12;
        Physics.defaultSolverVelocityIterations = 12;
        Time.fixedDeltaTime = 0.01333f; // (75fps). default is .2 (60fps)
        Time.maximumDeltaTime = .15f; // Default is .33
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }

    public override void AcademyReset()
    {
<<<<<<< HEAD


=======
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }

    public override void AcademyStep()
    {
<<<<<<< HEAD


=======
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }
}
