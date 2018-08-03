using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
/** \brief An interface which defines the functions needed for a CoreBrain. */
/** There is no need to modify or implement CoreBrain to create a Unity environment.
 */
public interface CoreBrain
{

    /// Implement setBrain so let the coreBrain know what brain is using it
    void SetBrain(Brain b);
    /// Implement this method to initialize CoreBrain
    void InitializeCoreBrain(Communicator communicator);
    /// Implement this method to define the logic for deciding actions
    void DecideAction(Dictionary<Agent, AgentInfo> agentInfo);
    /// Implement this method to define what should be displayed in the brain Inspector
    void OnInspector();
=======
namespace MLAgents
{
/** \brief An interface which defines the functions needed for a CoreBrain. */
/** There is no need to modify or implement CoreBrain to create a Unity environment.
 */
    public interface CoreBrain
    {

        /// Implement setBrain so let the coreBrain know what brain is using it
        void SetBrain(Brain b);

        /// Implement this method to initialize CoreBrain
        void InitializeCoreBrain(MLAgents.Batcher brainBatcher);

        /// Implement this method to define the logic for deciding actions
        void DecideAction(Dictionary<Agent, AgentInfo> agentInfo);

        /// Implement this method to define what should be displayed in the brain Inspector
        void OnInspector();
    }
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
}
