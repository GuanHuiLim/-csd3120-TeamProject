using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameOnDestroy : MonoBehaviour
{
    public GameController gameController;

    private void OnDestroy()
    {
        gameController.startGameEvent.Invoke();
    }


}
