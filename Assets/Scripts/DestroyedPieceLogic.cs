using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedPieceLogic : MonoBehaviour
{
    public float delay = 0.1f;

    private void Start()
    {
        Invoke("ChangeTag", delay);
    }

    private void ChangeTag()
    {
        gameObject.tag = "Sliceable";
    }

}
