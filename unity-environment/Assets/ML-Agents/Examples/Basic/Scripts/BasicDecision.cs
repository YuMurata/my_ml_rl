using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using MLAgents;
>>>>>>> 1ead1ccc2c842bd00a372eee5c4a47e429432712

public class BasicDecision : MonoBehaviour, Decision
{
    public float[] Decide(
        List<float> vectorObs,
        List<Texture2D> visualObs,
        float reward,
        bool done,
        List<float> memory)
    {
        return new float[1] { 1f };
    }

    public List<float> MakeMemory(
        List<float> vectorObs,
        List<Texture2D> visualObs,
        float reward,
        bool done,
        List<float> memory)
    {
        return new List<float>();
    }
}
