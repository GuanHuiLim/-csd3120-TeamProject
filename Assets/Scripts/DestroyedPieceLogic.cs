using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedPieceLogic : MonoBehaviour
{
    public float delay = 0.1f;

    private void Start()
    {
        Invoke("EnableToBeCut", delay);
    }

    private void EnableToBeCut()
    {
        gameObject.tag = "Sliceable";
        gameObject.layer = 6;

    }

}
