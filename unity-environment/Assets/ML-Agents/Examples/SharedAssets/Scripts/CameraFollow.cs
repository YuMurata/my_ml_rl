using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class CameraFollow : MonoBehaviour {

    public Transform target;
    Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = gameObject.transform.position - target.position;
    }
    
    // Update is called once per frame
    void Update () {
        // gameObject.transform.position = target.position + offset;
        Vector3 newPosition = new Vector3(target.position.x + offset.x, transform.position.y, target.position.z + offset.z);
        gameObject.transform.position = newPosition;
=======
namespace MLAgents
{

    public class CameraFollow : MonoBehaviour
    {

        public Transform target;
        Vector3 offset;

        // Use this for initialization
        void Start()
        {
            offset = gameObject.transform.position - target.position;
        }

        // Update is called once per frame
        void Update()
        {
            // gameObject.transform.position = target.position + offset;
            Vector3 newPosition = new Vector3(target.position.x + offset.x, transform.position.y,
                target.position.z + offset.z);
            gameObject.transform.position = newPosition;
        }
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }
}
