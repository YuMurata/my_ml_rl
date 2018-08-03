using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
<<<<<<< HEAD
[System.Serializable]
/// Contains exceptions specific to ML-Agents.
public class UnityAgentsException : System.Exception
{
    /// When a UnityAgentsException is called, the timeScale is set to 0.
    /// The simulation will end since no steps will be taken.
    public UnityAgentsException(string message) : base(message)
    {
        Time.timeScale = 0f;
    }

    /// A constructor is needed for serialization when an exception propagates 
    /// from a remoting server to the client. 
    protected UnityAgentsException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    { }
=======

namespace MLAgents
{

    [System.Serializable]
    /// Contains exceptions specific to ML-Agents.
    public class UnityAgentsException : System.Exception
    {
        /// When a UnityAgentsException is called, the timeScale is set to 0.
        /// The simulation will end since no steps will be taken.
        public UnityAgentsException(string message) : base(message)
        {

        }

        /// A constructor is needed for serialization when an exception propagates 
        /// from a remoting server to the client. 
        protected UnityAgentsException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        {
        }
    }
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
}
