using System.Collections.Generic;
using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public GameObject plunger, start, rope;

    void Update()
    {
        rope.GetComponent<Rope>().ropeLength = Vector3.Distance(plunger.transform.position, start.transform.position) + 2;
    }
}
