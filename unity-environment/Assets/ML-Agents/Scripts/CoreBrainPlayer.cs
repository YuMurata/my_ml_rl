<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
#if UNITY_EDITOR
using UnityEditor;
#endif

<<<<<<< HEAD

/// CoreBrain which decides actions using Player input.
public class CoreBrainPlayer : ScriptableObject, CoreBrain
{
    [SerializeField]
    private bool broadcast = true;

    [System.Serializable]
    private struct DiscretePlayerAction
    {
        public KeyCode key;
        public int value;
    }

    [System.Serializable]
    private struct ContinuousPlayerAction
    {
        public KeyCode key;
        public int index;
        public float value;
    }

    ExternalCommunicator coord;

    [SerializeField]
    [Tooltip("The list of keys and the value they correspond to for continuous control.")]
    /// Contains the mapping from input to continuous actions
    private ContinuousPlayerAction[] continuousPlayerActions;
    [SerializeField]
    [Tooltip("The list of keys and the value they correspond to for discrete control.")]
    /// Contains the mapping from input to discrete actions
    private DiscretePlayerAction[] discretePlayerActions;
    [SerializeField]
    private int defaultAction = 0;

    /// Reference to the brain that uses this CoreBrainPlayer
    public Brain brain;

    /// Create the reference to the brain
    public void SetBrain(Brain b)
    {
        brain = b;
    }

    /// Nothing to implement
    public void InitializeCoreBrain(Communicator communicator)
    {
        if ((communicator == null)
            || (!broadcast))
        {
            coord = null;
        }
        else if (communicator is ExternalCommunicator)
        {
            coord = (ExternalCommunicator)communicator;
            coord.SubscribeBrain(brain);
        }
    }

    /// Uses the continuous inputs or dicrete inputs of the player to 
    /// decide action
    public void DecideAction(Dictionary<Agent, AgentInfo> agentInfo)
    {
		if (coord != null)
		{
			coord.GiveBrainInfo(brain, agentInfo);
		}
        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            foreach (Agent agent in agentInfo.Keys)
            {
                var action = new float[brain.brainParameters.vectorActionSize];
                foreach (ContinuousPlayerAction cha in continuousPlayerActions)
                {
                    if (Input.GetKey(cha.key))
                    {
                        action[cha.index] = cha.value;
                    }
                }

                agent.UpdateVectorAction(action);
            }

        }
        else
        {
            foreach (Agent agent in agentInfo.Keys)
            {
                var action = new float[1] { defaultAction };
                foreach (DiscretePlayerAction dha in discretePlayerActions)
                {
                    if (Input.GetKey(dha.key))
                    {
                        action[0] = (float)dha.value;
                        break;
                    }
                }


                agent.UpdateVectorAction(action);
                
            }
        }

    }

    /// Displays continuous or discrete input mapping in the inspector
    public void OnInspector()
    {
#if UNITY_EDITOR
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        broadcast = EditorGUILayout.Toggle(new GUIContent("Broadcast",
                      "If checked, the brain will broadcast states and actions to Python."), broadcast);
=======
namespace MLAgents
{
    /// CoreBrain which decides actions using Player input.
    public class CoreBrainPlayer : ScriptableObject, CoreBrain
    {
        [SerializeField] private bool broadcast = true;


        [System.Serializable]
        private struct DiscretePlayerAction
        {
            public KeyCode key;
            public int value;
        }

        [System.Serializable]
        private struct KeyContinuousPlayerAction
        {
            public KeyCode key;
            public int index;
            public float value;
        }
        
        [System.Serializable]
        private struct AxisContinuousPlayerAction
        {
            public string axis;
            public int index;
            public float scale;
        }

        MLAgents.Batcher brainBatcher;

        [SerializeField]
        [FormerlySerializedAs("continuousPlayerActions")]
        [Tooltip("The list of keys and the value they correspond to for continuous control.")]
        /// Contains the mapping from input to continuous actions
        private KeyContinuousPlayerAction[] keyContinuousPlayerActions;
        
        [SerializeField]
        [Tooltip("The list of axis actions.")]
        /// Contains the mapping from input to continuous actions
        private AxisContinuousPlayerAction[] axisContinuousPlayerActions;
        
        [SerializeField]
        [Tooltip("The list of keys and the value they correspond to for discrete control.")]
        /// Contains the mapping from input to discrete actions
        private DiscretePlayerAction[] discretePlayerActions;
        [SerializeField]
        private int defaultAction = 0;

        /// Reference to the brain that uses this CoreBrainPlayer
        public Brain brain;

        /// Create the reference to the brain
        public void SetBrain(Brain b)
        {
            brain = b;
        }

        /// Nothing to implement
        /// Nothing to implement
        public void InitializeCoreBrain(MLAgents.Batcher brainBatcher)
        {
            if ((brainBatcher == null)
                || (!broadcast))
            {
                this.brainBatcher = null;
            }
            else
            {
                this.brainBatcher = brainBatcher;
                this.brainBatcher.SubscribeBrain(brain.gameObject.name);
            }
        }

        /// Uses the continuous inputs or dicrete inputs of the player to 
        /// decide action
        public void DecideAction(Dictionary<Agent, AgentInfo> agentInfo)
        {
            if (brainBatcher != null)
            {
                brainBatcher.SendBrainInfo(brain.gameObject.name, agentInfo);
            }

            if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
            {
                foreach (Agent agent in agentInfo.Keys)
                {
                    var action = new float[brain.brainParameters.vectorActionSize];
                    foreach (KeyContinuousPlayerAction cha in keyContinuousPlayerActions)
                        {
                            if (Input.GetKey(cha.key))
                            {
                                action[cha.index] = cha.value;
                            }
                        }
    

                    foreach (AxisContinuousPlayerAction axisAction in axisContinuousPlayerActions)
                    {
                        var axisValue = Input.GetAxis(axisAction.axis);
                        axisValue *= axisAction.scale;
                        if (Mathf.Abs(axisValue) > 0.0001)
                        {
                            action[axisAction.index] = axisValue;
                        }
                    }
                    agent.UpdateVectorAction(action);
                } 
            }
            else
            {
                foreach (Agent agent in agentInfo.Keys)
                {
                    var action = new float[1] {defaultAction};
                    foreach (DiscretePlayerAction dha in discretePlayerActions)
                    {
                        if (Input.GetKey(dha.key))
                        {
                            action[0] = (float) dha.value;
                            break;
                        }
                    }


                    agent.UpdateVectorAction(action);

                }
            }

        }

        /// Displays continuous or discrete input mapping in the inspector
        public void OnInspector()
        {
#if UNITY_EDITOR
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        broadcast = EditorGUILayout.Toggle(
            new GUIContent("Broadcast",
                "If checked, the brain will broadcast states and actions to Python."), broadcast);
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        var serializedBrain = new SerializedObject(this);
        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            GUILayout.Label("Edit the continuous inputs for your actions", EditorStyles.boldLabel);
<<<<<<< HEAD
            var chas = serializedBrain.FindProperty("continuousPlayerActions");
            serializedBrain.Update();
            EditorGUILayout.PropertyField(chas, true);
            serializedBrain.ApplyModifiedProperties();
            if (continuousPlayerActions == null)
            {
                continuousPlayerActions = new ContinuousPlayerAction[0];
            }
            foreach (ContinuousPlayerAction cha in continuousPlayerActions)
            {
                if (cha.index >= brain.brainParameters.vectorActionSize)
                {
                    EditorGUILayout.HelpBox(string.Format("Key {0} is assigned to index {1} but the action size is only of size {2}"
                        , cha.key.ToString(), cha.index.ToString(), brain.brainParameters.vectorActionSize.ToString()), MessageType.Error);
                }
            }

=======
            var keyActionsProp = serializedBrain.FindProperty("keyContinuousPlayerActions");
            var axisActionsProp = serializedBrain.FindProperty("axisContinuousPlayerActions");
            serializedBrain.Update();
            EditorGUILayout.PropertyField(keyActionsProp , true);
            EditorGUILayout.PropertyField(axisActionsProp, true);
            serializedBrain.ApplyModifiedProperties();
            if (keyContinuousPlayerActions == null)
            {
                keyContinuousPlayerActions = new KeyContinuousPlayerAction[0];
            }
            if (axisContinuousPlayerActions == null)
            {
                axisContinuousPlayerActions = new AxisContinuousPlayerAction[0];
            }
            foreach (KeyContinuousPlayerAction action in keyContinuousPlayerActions)
            {
                if (action.index >= brain.brainParameters.vectorActionSize)
                {
                    EditorGUILayout.HelpBox(
                        string.Format(
                            "Key {0} is assigned to index {1} " +
                            "but the action size is only of size {2}"
                        , action.key.ToString(), action.index.ToString(), 
                            brain.brainParameters.vectorActionSize.ToString()), 
                        MessageType.Error);
                }
            }
            foreach (AxisContinuousPlayerAction action in axisContinuousPlayerActions)
            {
                if (action .index >= brain.brainParameters.vectorActionSize)
                {
                    EditorGUILayout.HelpBox(
                        string.Format(
                            "Axis {0} is assigned to index {1} " +
                            "but the action size is only of size {2}"
                        , action.axis, action.index.ToString(),
                            brain.brainParameters.vectorActionSize.ToString()), 
                        MessageType.Error);
                }
            }
            GUILayout.Label("You can change axis settings from Edit->Project Settings->Input", 
                EditorStyles.helpBox );
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        }
        else
        {
            GUILayout.Label("Edit the discrete inputs for your actions", EditorStyles.boldLabel);
            defaultAction = EditorGUILayout.IntField("Default Action", defaultAction);
            var dhas = serializedBrain.FindProperty("discretePlayerActions");
            serializedBrain.Update();
            EditorGUILayout.PropertyField(dhas, true);
            serializedBrain.ApplyModifiedProperties();
        }
#endif
<<<<<<< HEAD
=======
        }
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
    }
}
