using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Linq;

#if ENABLE_TENSORFLOW
using TensorFlow;
#endif

<<<<<<< HEAD
/// CoreBrain which decides actions using internally embedded TensorFlow model.
public class CoreBrainInternal : ScriptableObject, CoreBrain
{

    [SerializeField]
    [Tooltip("If checked, the brain will broadcast states and actions to Python.")]
    #pragma warning disable
    private bool broadcast = true;
    #pragma warning restore

    [System.Serializable]
    private struct TensorFlowAgentPlaceholder
    {
        public enum tensorType
        {
            Integer,
            FloatingPoint
        }

        ;

        public string name;
        public tensorType valueType;
        public float minValue;
        public float maxValue;

    }

    ExternalCommunicator coord;

    [Tooltip("This must be the bytes file corresponding to the pretrained Tensorflow graph.")]
    /// Modify only in inspector : Reference to the Graph asset
    public TextAsset graphModel;

    /// Modify only in inspector : If a scope was used when training the model, specify it here
    public string graphScope;
    [SerializeField]
    [Tooltip("If your graph takes additional inputs that are fixed (example: noise level) you can specify them here.")]
    ///  Modify only in inspector : If your graph takes additional inputs that are fixed you can specify them here.
    private TensorFlowAgentPlaceholder[] graphPlaceholders;
    ///  Modify only in inspector : Name of the placholder of the batch size
    public string BatchSizePlaceholderName = "batch_size";
    ///  Modify only in inspector : Name of the state placeholder
    public string VectorObservationPlacholderName = "vector_observation";
    ///  Modify only in inspector : Name of the recurrent input
    public string RecurrentInPlaceholderName = "recurrent_in";
    ///  Modify only in inspector : Name of the recurrent output
    public string RecurrentOutPlaceholderName = "recurrent_out";
    /// Modify only in inspector : Names of the observations placeholders
    public string[] VisualObservationPlaceholderName;
    /// Modify only in inspector : Name of the action node
    public string ActionPlaceholderName = "action";
    /// Modify only in inspector : Name of the previous action node
    public string PreviousActionPlaceholderName = "prev_action";
=======
namespace MLAgents
{
    /// CoreBrain which decides actions using internally embedded TensorFlow model.
    public class CoreBrainInternal : ScriptableObject, CoreBrain
    {

        [SerializeField]
        [Tooltip("If checked, the brain will broadcast states and actions to Python.")]
#pragma warning disable
        private bool broadcast = true;
#pragma warning restore

        [System.Serializable]
        private struct TensorFlowAgentPlaceholder
        {
            public enum tensorType
            {
                Integer,
                FloatingPoint
            };

            public string name;
            public tensorType valueType;
            public float minValue;
            public float maxValue;

        }

        MLAgents.Batcher brainBatcher;

        [Tooltip("This must be the bytes file corresponding to the pretrained TensorFlow graph.")]
        /// Modify only in inspector : Reference to the Graph asset
        public TextAsset graphModel;

        /// Modify only in inspector : If a scope was used when training the model, specify it here
        public string graphScope;

        [SerializeField]
        [Tooltip(
            "If your graph takes additional inputs that are fixed (example: noise level) you can specify them here.")]
        ///  Modify only in inspector : If your graph takes additional inputs that are fixed you can specify them here.
        private TensorFlowAgentPlaceholder[] graphPlaceholders;

        ///  Modify only in inspector : Name of the placholder of the batch size
        public string BatchSizePlaceholderName = "batch_size";

        ///  Modify only in inspector : Name of the state placeholder
        public string VectorObservationPlacholderName = "vector_observation";

        ///  Modify only in inspector : Name of the recurrent input
        public string RecurrentInPlaceholderName = "recurrent_in";

        ///  Modify only in inspector : Name of the recurrent output
        public string RecurrentOutPlaceholderName = "recurrent_out";

        /// Modify only in inspector : Names of the observations placeholders
        public string[] VisualObservationPlaceholderName;

        /// Modify only in inspector : Name of the action node
        public string ActionPlaceholderName = "action";

        /// Modify only in inspector : Name of the previous action node
        public string PreviousActionPlaceholderName = "prev_action";
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
#if ENABLE_TENSORFLOW
    TFGraph graph;
    TFSession session;
    bool hasRecurrent;
    bool hasState;
    bool hasBatchSize;
    bool hasPrevAction;
    float[,] inputState;
    int[] inputPrevAction;
    List<float[,,,]> observationMatrixList;
    float[,] inputOldMemories;
    List<Texture2D> texturesHolder;
    int memorySize;
#endif

<<<<<<< HEAD
    /// Reference to the brain that uses this CoreBrainInternal
    public Brain brain;

    /// Create the reference to the brain
    public void SetBrain(Brain b)
    {
        brain = b;
    }

    /// Loads the tensorflow graph model to generate a TFGraph object
    public void InitializeCoreBrain(Communicator communicator)
    {
=======
        /// Reference to the brain that uses this CoreBrainInternal
        public Brain brain;

        /// Create the reference to the brain
        public void SetBrain(Brain b)
        {
            brain = b;
        }

        /// Loads the tensorflow graph model to generate a TFGraph object
        public void InitializeCoreBrain(MLAgents.Batcher brainBatcher)
        {
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
#if ENABLE_TENSORFLOW
#if UNITY_ANDROID
        // This needs to ba called only once and will raise an exception if 
        // there are multiple internal brains
        try{
            TensorFlowSharp.Android.NativeBinding.Init();
        }
        catch{
            
        }
#endif
<<<<<<< HEAD
        if ((communicator == null)
        || (!broadcast))
        {
            coord = null;
        }
        else if (communicator is ExternalCommunicator)
        {
            coord = (ExternalCommunicator)communicator;
            coord.SubscribeBrain(brain);
=======
        if ((brainBatcher == null)
            || (!broadcast))
        {
            this.brainBatcher = null;
        }
        else
        {
            this.brainBatcher = brainBatcher;
            this.brainBatcher.SubscribeBrain(brain.gameObject.name);
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        }

        if (graphModel != null)
        {

            graph = new TFGraph();

            graph.Import(graphModel.bytes);

            session = new TFSession(graph);

            // TODO: Make this a loop over a dynamic set of graph inputs

            if ((graphScope.Length > 1) && (graphScope[graphScope.Length - 1] != '/'))
            {
                graphScope = graphScope + '/';
            }

            if (graph[graphScope + BatchSizePlaceholderName] != null)
            {
                hasBatchSize = true;
            }
            if ((graph[graphScope + RecurrentInPlaceholderName] != null) && (graph[graphScope + RecurrentOutPlaceholderName] != null))
            {
                hasRecurrent = true;
                var runner = session.GetRunner();
                runner.Fetch(graph[graphScope + "memory_size"][0]);
                var networkOutput = runner.Run()[0].GetValue();
                memorySize = (int)networkOutput;
            }
            if (graph[graphScope + VectorObservationPlacholderName] != null)
            {
                hasState = true;
            }
            if (graph[graphScope + PreviousActionPlaceholderName] != null)
            {
                hasPrevAction = true;
            }
        }
        observationMatrixList = new List<float[,,,]>();
        texturesHolder = new List<Texture2D>();
#endif
<<<<<<< HEAD
    }



    /// Uses the stored information to run the tensorflow graph and generate 
    /// the actions.
    public void DecideAction(Dictionary<Agent, AgentInfo> agentInfo)
    {
#if ENABLE_TENSORFLOW
        if (coord != null)
        {
            coord.GiveBrainInfo(brain, agentInfo);
=======
        }



        /// Uses the stored information to run the tensorflow graph and generate 
        /// the actions.
        public void DecideAction(Dictionary<Agent, AgentInfo> agentInfo)
        {
#if ENABLE_TENSORFLOW
        if (brainBatcher != null)
        {
            brainBatcher.SendBrainInfo(brain.gameObject.name, agentInfo);
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        }
        int currentBatchSize = agentInfo.Count();
        List<Agent> agentList = agentInfo.Keys.ToList();
        if (currentBatchSize == 0)
        {
            return;
        }


        // Create the state tensor
        if (hasState)
        {
            int stateLength = 1;
            if (brain.brainParameters.vectorObservationSpaceType == SpaceType.continuous)
            {
                stateLength = brain.brainParameters.vectorObservationSize;
            }
<<<<<<< HEAD
            inputState = new float[currentBatchSize, stateLength * brain.brainParameters.numStackedVectorObservations];
=======
            inputState =
 new float[currentBatchSize, stateLength * brain.brainParameters.numStackedVectorObservations];
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

            var i = 0;
            foreach (Agent agent in agentList)
            {
                List<float> state_list = agentInfo[agent].stackedVectorObservation;
<<<<<<< HEAD
                for (int j = 0; j < brain.brainParameters.vectorObservationSize * brain.brainParameters.numStackedVectorObservations; j++)
=======
                for (int j =
 0; j < stateLength * brain.brainParameters.numStackedVectorObservations; j++)
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                {
                    inputState[i, j] = state_list[j];
                }
                i++;
            }
        }

        // Create the state tensor
        if (hasPrevAction)
        {
            inputPrevAction = new int[currentBatchSize];
            var i = 0;
            foreach (Agent agent in agentList)
            {
                float[] action_list = agentInfo[agent].storedVectorActions;
                inputPrevAction[i] = Mathf.FloorToInt(action_list[0]);
                i++;
            }
        }


        observationMatrixList.Clear();
<<<<<<< HEAD
        for (int observationIndex = 0; observationIndex < brain.brainParameters.cameraResolutions.Count(); observationIndex++){
=======
        for (int observationIndex =
 0; observationIndex < brain.brainParameters.cameraResolutions.Count(); observationIndex++){
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
            texturesHolder.Clear();
            foreach (Agent agent in agentList){
                texturesHolder.Add(agentInfo[agent].visualObservations[observationIndex]);
            }
            observationMatrixList.Add(
                BatchVisualObservations(texturesHolder, brain.brainParameters.cameraResolutions[observationIndex].blackAndWhite));

        }

        // Create the recurrent tensor
        if (hasRecurrent)
        {
            // Need to have variable memory size
            inputOldMemories = new float[currentBatchSize, memorySize];
            var i = 0;
            foreach (Agent agent in agentList)
            {
                float[] m = agentInfo[agent].memories.ToArray();
                for (int j = 0; j < m.Count(); j++)
                {
                    inputOldMemories[i, j] = m[j];
                }
                i++;
            }
        }


        var runner = session.GetRunner();
        try
        {
            runner.Fetch(graph[graphScope + ActionPlaceholderName][0]);
        }
        catch
        {
            throw new UnityAgentsException(string.Format(@"The node {0} could not be found. Please make sure the graphScope {1} is correct",
                     graphScope + ActionPlaceholderName, graphScope));
        }

        if (hasBatchSize)
        {
            runner.AddInput(graph[graphScope + BatchSizePlaceholderName][0], new int[] { currentBatchSize });
        }

        foreach (TensorFlowAgentPlaceholder placeholder in graphPlaceholders)
        {
            try
            {
                if (placeholder.valueType == TensorFlowAgentPlaceholder.tensorType.FloatingPoint)
                {
                    runner.AddInput(graph[graphScope + placeholder.name][0], new float[] { Random.Range(placeholder.minValue, placeholder.maxValue) });
                }
                else if (placeholder.valueType == TensorFlowAgentPlaceholder.tensorType.Integer)
                {
                    runner.AddInput(graph[graphScope + placeholder.name][0], new int[] { Random.Range((int)placeholder.minValue, (int)placeholder.maxValue + 1) });
                }
            }
            catch
            {
                throw new UnityAgentsException(string.Format(@"One of the Tensorflow placeholder cound nout be found.
                In brain {0}, there are no {1} placeholder named {2}.",
                        brain.gameObject.name, placeholder.valueType.ToString(), graphScope + placeholder.name));
            }
        }

        // Create the state tensor
        if (hasState)
        {
            if (brain.brainParameters.vectorObservationSpaceType == SpaceType.discrete)
            {
                var discreteInputState = new int[currentBatchSize, 1];
                for (int i = 0; i < currentBatchSize; i++)
                {
                    discreteInputState[i, 0] = (int)inputState[i, 0];
                }
                runner.AddInput(graph[graphScope + VectorObservationPlacholderName][0], discreteInputState);
            }
            else
            {
                runner.AddInput(graph[graphScope + VectorObservationPlacholderName][0], inputState);
            }
        }

        // Create the previous action tensor
        if (hasPrevAction)
        {
            runner.AddInput(graph[graphScope + PreviousActionPlaceholderName][0], inputPrevAction);
        }

        // Create the observation tensors
<<<<<<< HEAD
        for (int obs_number = 0; obs_number < brain.brainParameters.cameraResolutions.Length; obs_number++)
=======
        for (int obs_number =
 0; obs_number < brain.brainParameters.cameraResolutions.Length; obs_number++)
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
        {
            runner.AddInput(graph[graphScope + VisualObservationPlaceholderName[obs_number]][0], observationMatrixList[obs_number]);
        }

        if (hasRecurrent)
        {
            runner.AddInput(graph[graphScope + "sequence_length"][0], 1);
            runner.AddInput(graph[graphScope + RecurrentInPlaceholderName][0], inputOldMemories);
            runner.Fetch(graph[graphScope + RecurrentOutPlaceholderName][0]);
        }

        TFTensor[] networkOutput;
        try
        {
            networkOutput = runner.Run();
        }
        catch (TFException e)
        {
            string errorMessage = e.Message;
            try
            {
<<<<<<< HEAD
                errorMessage = string.Format(@"The tensorflow graph needs an input for {0} of type {1}",
=======
                errorMessage =
 string.Format(@"The tensorflow graph needs an input for {0} of type {1}",
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                    e.Message.Split(new string[] { "Node: " }, 0)[1].Split('=')[0],
                    e.Message.Split(new string[] { "dtype=" }, 0)[1].Split(',')[0]);
            }
            finally
            {
                throw new UnityAgentsException(errorMessage);
            }

        }

        // Create the recurrent tensor
        if (hasRecurrent)
        {
            float[,] recurrent_tensor = networkOutput[1].GetValue() as float[,];

            var i = 0;
            foreach (Agent agent in agentList)
            {
                var m = new float[memorySize];
                for (int j = 0; j < memorySize; j++)
                {
                    m[j] = recurrent_tensor[i, j];
                }
                agent.UpdateMemoriesAction(m.ToList());
                i++;
            }

        }

        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            var output = networkOutput[0].GetValue() as float[,];
            var i = 0;
            foreach (Agent agent in agentList)
            {
                var a = new float[brain.brainParameters.vectorActionSize];
                for (int j = 0; j < brain.brainParameters.vectorActionSize; j++)
                {
                    a[j] = output[i, j];
                }
                agent.UpdateVectorAction(a);
                i++;
            }
        }
        else if (brain.brainParameters.vectorActionSpaceType == SpaceType.discrete)
        {
            long[,] output = networkOutput[0].GetValue() as long[,];
            var i = 0;
            foreach (Agent agent in agentList)
            {
                var a = new float[1] { (float)(output[i, 0]) };
                agent.UpdateVectorAction(a);
                i++;
            }
        }




#else
<<<<<<< HEAD
        if (agentInfo.Count > 0)
        {
            throw new UnityAgentsException(string.Format(@"The brain {0} was set to Internal but the Tensorflow 
                        library is not present in the Unity project.",
                        brain.gameObject.name));
        }
#endif
    }

    /// Displays the parameters of the CoreBrainInternal in the Inspector 
    public void OnInspector()
    {
=======
            if (agentInfo.Count > 0)
            {
                throw new UnityAgentsException(string.Format(
                    @"The brain {0} was set to Internal but the Tensorflow 
                        library is not present in the Unity project.",
                    brain.gameObject.name));
            }
#endif
        }

        /// Displays the parameters of the CoreBrainInternal in the Inspector 
        public void OnInspector()
        {
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
#if ENABLE_TENSORFLOW && UNITY_EDITOR
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        broadcast = EditorGUILayout.Toggle(new GUIContent("Broadcast",
                      "If checked, the brain will broadcast states and actions to Python."), broadcast);

        var serializedBrain = new SerializedObject(this);
        GUILayout.Label("Edit the Tensorflow graph parameters here");
        var tfGraphModel = serializedBrain.FindProperty("graphModel");
        serializedBrain.Update();
        EditorGUILayout.ObjectField(tfGraphModel);
        serializedBrain.ApplyModifiedProperties();

        if (graphModel == null)
        {
            EditorGUILayout.HelpBox("Please provide a tensorflow graph as a bytes file.", MessageType.Error);
        }


<<<<<<< HEAD
        graphScope = EditorGUILayout.TextField(new GUIContent("Graph Scope", "If you set a scope while training your tensorflow model, " +
=======
        graphScope =
 EditorGUILayout.TextField(new GUIContent("Graph Scope", "If you set a scope while training your tensorflow model, " +
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                           "all your placeholder name will have a prefix. You must specify that prefix here."), graphScope);

        if (BatchSizePlaceholderName == "")
        {
            BatchSizePlaceholderName = "batch_size";
        }
<<<<<<< HEAD
        BatchSizePlaceholderName = EditorGUILayout.TextField(new GUIContent("Batch Size Node Name", "If the batch size is one of " +
=======
        BatchSizePlaceholderName =
 EditorGUILayout.TextField(new GUIContent("Batch Size Node Name", "If the batch size is one of " +
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                            "the inputs of your graph, you must specify the name if the placeholder here."), BatchSizePlaceholderName);
        if (VectorObservationPlacholderName == "")
        {
            VectorObservationPlacholderName = "state";
        }
<<<<<<< HEAD
        VectorObservationPlacholderName = EditorGUILayout.TextField(new GUIContent("Vector Observation Node Name", "If your graph uses the state as an input, " +
=======
        VectorObservationPlacholderName =
 EditorGUILayout.TextField(new GUIContent("Vector Observation Node Name", "If your graph uses the state as an input, " +
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                                                                                   "you must specify the name if the placeholder here."), VectorObservationPlacholderName);
        if (RecurrentInPlaceholderName == "")
        {
            RecurrentInPlaceholderName = "recurrent_in";
        }
<<<<<<< HEAD
        RecurrentInPlaceholderName = EditorGUILayout.TextField(new GUIContent("Recurrent Input Node Name", "If your graph uses a " +
=======
        RecurrentInPlaceholderName =
 EditorGUILayout.TextField(new GUIContent("Recurrent Input Node Name", "If your graph uses a " +
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                          "recurrent input / memory as input and outputs new recurrent input / memory, " +
                          "you must specify the name if the input placeholder here."), RecurrentInPlaceholderName);
        if (RecurrentOutPlaceholderName == "")
        {
            RecurrentOutPlaceholderName = "recurrent_out";
        }
<<<<<<< HEAD
        RecurrentOutPlaceholderName = EditorGUILayout.TextField(new GUIContent("Recurrent Output Node Name", " If your graph uses a " +
=======
        RecurrentOutPlaceholderName =
 EditorGUILayout.TextField(new GUIContent("Recurrent Output Node Name", " If your graph uses a " +
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                           "recurrent input / memory as input and outputs new recurrent input / memory, you must specify the name if " +
                           "the output placeholder here."), RecurrentOutPlaceholderName);

        if (brain.brainParameters.cameraResolutions != null)
        {
            if (brain.brainParameters.cameraResolutions.Count() > 0)
            {
                if (VisualObservationPlaceholderName == null)
                {
<<<<<<< HEAD
                    VisualObservationPlaceholderName = new string[brain.brainParameters.cameraResolutions.Count()];
                }
                if (VisualObservationPlaceholderName.Count() != brain.brainParameters.cameraResolutions.Count())
                {
                    VisualObservationPlaceholderName = new string[brain.brainParameters.cameraResolutions.Count()];
                }
                for (int obs_number = 0; obs_number < brain.brainParameters.cameraResolutions.Count(); obs_number++)
=======
                    VisualObservationPlaceholderName =
 new string[brain.brainParameters.cameraResolutions.Count()];
                }
                if (VisualObservationPlaceholderName.Count() != brain.brainParameters.cameraResolutions.Count())
                {
                    VisualObservationPlaceholderName =
 new string[brain.brainParameters.cameraResolutions.Count()];
                }
                for (int obs_number =
 0; obs_number < brain.brainParameters.cameraResolutions.Count(); obs_number++)
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                {
                    if ((VisualObservationPlaceholderName[obs_number] == "") || (VisualObservationPlaceholderName[obs_number] == null))
                    {

<<<<<<< HEAD
                        VisualObservationPlaceholderName[obs_number] = "visual_observation_" + obs_number;
=======
                        VisualObservationPlaceholderName[obs_number] =
 "visual_observation_" + obs_number;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                    }
                }
                var opn = serializedBrain.FindProperty("VisualObservationPlaceholderName");
                serializedBrain.Update();
                EditorGUILayout.PropertyField(opn, true);
                serializedBrain.ApplyModifiedProperties();
            }
        }

        if (ActionPlaceholderName == "")
        {
            ActionPlaceholderName = "action";
        }
<<<<<<< HEAD
        ActionPlaceholderName = EditorGUILayout.TextField(new GUIContent("Action Node Name", "Specify the name of the " +
=======
        ActionPlaceholderName =
 EditorGUILayout.TextField(new GUIContent("Action Node Name", "Specify the name of the " +
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
                         "placeholder corresponding to the actions of the brain in your graph. If the action space type is " +
                         "continuous, the output must be a one dimensional tensor of float of length Action Space Size, " +
                         "if the action space type is discrete, the output must be a one dimensional tensor of int " +
                         "of length 1."), ActionPlaceholderName);



        var tfPlaceholders = serializedBrain.FindProperty("graphPlaceholders");
        serializedBrain.Update();
        EditorGUILayout.PropertyField(tfPlaceholders, true);
        serializedBrain.ApplyModifiedProperties();
#endif
#if !ENABLE_TENSORFLOW && UNITY_EDITOR
<<<<<<< HEAD
        EditorGUILayout.HelpBox (
            "You need to install and enable the TensorflowSharp plugin in"+ 
            "order to use the internal brain.", MessageType.Error);
        if (GUILayout.Button("Show me how"))
        {
            Application.OpenURL("https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Getting-Started-with-Balance-Ball.md#embedding-the-trained-brain-into-the-unity-environment-experimental");
        }
#endif
    }

    /// Contains logic to convert the agent's cameras into observation list
    ///  (as list of float arrays)
    public static float[,,,] BatchVisualObservations(List<Texture2D> textures, bool BlackAndWhite)
    {
        int batchSize = textures.Count();
        int width = textures[0].width;
        int height = textures[0].height;
        int pixels = 0;
        if (BlackAndWhite)
            pixels = 1;
        else
            pixels = 3;
        float[,,,] result = new float[batchSize, height, width, pixels];

        for (int b = 0; b < batchSize; b++)
        {
            Color32[] cc = textures[b].GetPixels32();
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    Color32 currentPixel = cc[h * width + w];
                    if (!BlackAndWhite)
                    {
                        result[b, textures[b].height - h - 1, w, 0] = currentPixel.r;
                        result[b, textures[b].height - h - 1, w, 1] = currentPixel.g;
                        result[b, textures[b].height - h - 1, w, 2] = currentPixel.b;
                    }
                    else
                    {
                        result[b, textures[b].height - h - 1, w, 0] = (currentPixel.r + currentPixel.g + currentPixel.b) / 3;
                    }
                }
            }
        }
        return result;
    }

=======
            EditorGUILayout.HelpBox(
                "You need to install and enable the TensorflowSharp plugin in " +
                "order to use the internal brain.", MessageType.Error);
            if (GUILayout.Button("Show me how"))
            {
                Application.OpenURL(
                    "https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Getting-Started-with-" +
                    "Balance-Ball.md#embedding-the-trained-brain-into-the-unity-environment-experimental");
            }
#endif
        }

        /// <summary>
        /// Converts a list of Texture2D into a Tensor.
        /// </summary>
        /// <returns>
        /// A 4 dimensional float Tensor of dimension
        /// [batch_size, height, width, channel].
        /// Where batch_size is the number of input textures,
        /// height corresponds to the height of the texture,
        /// width corresponds to the width of the texture,
        /// channel corresponds to the number of channels extracted from the
        /// input textures (based on the input blackAndWhite flag
        /// (3 if the flag is false, 1 otherwise).
        /// The values of the Tensor are between 0 and 1.
        /// </returns>
        /// <param name="textures">
        /// The list of textures to be put into the tensor.
        /// Note that the textures must have same width and height.
        /// </param>
        /// <param name="blackAndWhite">
        /// If set to <c>true</c> the textures
        /// will be converted to grayscale before being stored in the tensor.
        /// </param>
        public static float[,,,] BatchVisualObservations(
            List<Texture2D> textures, bool blackAndWhite)
        {
            int batchSize = textures.Count();
            int width = textures[0].width;
            int height = textures[0].height;
            int pixels = 0;
            if (blackAndWhite)
                pixels = 1;
            else
                pixels = 3;
            float[,,,] result = new float[batchSize, height, width, pixels];
            float[] resultTemp = new float[batchSize * height * width * pixels];
            int hwp = height * width * pixels;
            int wp = width * pixels;

            for (int b = 0; b < batchSize; b++)
            {
                Color32[] cc = textures[b].GetPixels32();
                for (int h = height - 1; h >= 0; h--)
                {
                    for (int w = 0; w < width; w++)
                    {
                        Color32 currentPixel = cc[(height - h - 1) * width + w];
                        if (!blackAndWhite)
                        {
                            // For Color32, the r, g and b values are between
                            // 0 and 255.
                            resultTemp[b * hwp + h * wp + w * pixels] = currentPixel.r / 255.0f;
                            resultTemp[b * hwp + h * wp + w * pixels + 1] = currentPixel.g / 255.0f;
                            resultTemp[b * hwp + h * wp + w * pixels + 2] = currentPixel.b / 255.0f;
                        }
                        else
                        {
                            resultTemp[b * hwp + h * wp + w * pixels] =
                                (currentPixel.r + currentPixel.g + currentPixel.b)
                                / 3f / 255.0f;
                        }
                    }
                }
            }

            System.Buffer.BlockCopy(resultTemp, 0, result, 0, batchSize * hwp * sizeof(float));
            return result;
        }

    }
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712
}
