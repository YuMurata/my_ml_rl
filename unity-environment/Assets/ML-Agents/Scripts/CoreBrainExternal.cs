using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
/// CoreBrain which decides actions via communication with an external system such as Python.
public class CoreBrainExternal : ScriptableObject, CoreBrain
{
    /**< Reference to the brain that uses this CoreBrainExternal */
    public Brain brain;

    ExternalCommunicator coord;

    /// Creates the reference to the brain
    public void SetBrain(Brain b)
    {
        brain = b;
    }

    /// Generates the communicator for the Academy if none was present and
    ///  subscribe to ExternalCommunicator if it was present.
    public void InitializeCoreBrain(Communicator communicator)
    {
        if (communicator == null)
        {
            coord = null;
            throw new UnityAgentsException(string.Format("The brain {0} was set to" +
                " External mode" +
                " but Unity was unable to read the" +
                " arguments passed at launch.", brain.gameObject.name));
        }
        else if (communicator is ExternalCommunicator)
        {
            coord = (ExternalCommunicator)communicator;
            coord.SubscribeBrain(brain);
        }

    }

    /// Uses the communicator to retrieve the actions, memories and values and
    ///  sends them to the agents
    public void DecideAction(Dictionary<Agent, AgentInfo> agentInfo)
    {
        if (coord != null)
        {
            coord.GiveBrainInfo(brain, agentInfo);
        }
        return ;
    }

    /// Nothing needs to appear in the inspector 
    public void OnInspector()
    {

=======
namespace MLAgents
{
    /// CoreBrain which decides actions via communication with an external system such as Python.
    public class CoreBrainExternal : ScriptableObject, CoreBrain
    {
        /**< Reference to the brain that uses this CoreBrainExternal */
        public Brain brain;

        MLAgents.Batcher brainBatcher;

        /// Creates the reference to the brain
        public void SetBrain(Brain b)
        {
            brain = b;
        }

        /// Generates the communicator for the Academy if none was present and
        ///  subscribe to ExternalCommunicator if it was present.
        public void InitializeCoreBrain(MLAgents.Batcher brainBatcher)
        {
            if (brainBatcher == null)
            {
                brainBatcher = null;
                throw new UnityAgentsException(string.Format("The brain {0} was set to" +
                                                             " External mode" +
                                                             " but Unity was unable to read the" +
                                                             " arguments passed at launch.",
                    brain.gameObject.name));
            }
            else
            {
                this.brainBatcher = brainBatcher;
                this.brainBatcher.SubscribeBrain(brain.gameObject.name);
            }

        }

        /// Uses the communicator to retrieve the actions, memories and values and
        ///  sends them to the agents
        public void DecideAction(Dictionary<Agent, AgentInfo> agentInfo)
        {
            if (brainBatcher != null)
            {
                brainBatcher.SendBrainInfo(brain.gameObject.name, agentInfo);
            }

            return;
        }

        /// Nothing needs to appear in the inspector 
        public void OnInspector()
        {

        }
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }
}
