using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMusicChanger : MonoBehaviour
{
    public RoomMusicSO roomMusic; 

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManagement.Instance.PlayMusic(roomMusic);
        }
    }


}