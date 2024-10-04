using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionSceneChange : MonoBehaviour
{
    [Header("Escena a cargar al colisionar")]
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SceneChanger"))
        {
            SceneTransitionManager.Instance.FadeToScene(sceneToLoad);
        }
    }
}
