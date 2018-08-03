using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD

public class BouncerBanana : MonoBehaviour {


    void Start(){

    }
    
=======
using MLAgents;

public class BouncerBanana : MonoBehaviour {

>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    // Update is called once per frame
    void FixedUpdate () {
        gameObject.transform.Rotate(new Vector3(1, 0, 0), 0.5f);
    }

    private void OnTriggerEnter(Collider collision)
    {
<<<<<<< HEAD

=======
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        Agent agent = collision.gameObject.GetComponent<Agent>();
        if (agent != null)
        {
            agent.AddReward(1f);
            Respawn();
        }

<<<<<<< HEAD

=======
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }

    public void Respawn(){
        gameObject.transform.localPosition = 
            new Vector3(
<<<<<<< HEAD
                (1 - 2 * Random.value) *5f, 
                2f+(Random.value) *5f, 
                (1 - 2 * Random.value) *5f);
=======
                (1 - 2 * Random.value) * 5f, 
                2f+ Random.value * 5f, 
                (1 - 2 * Random.value) * 5f);
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }

}
