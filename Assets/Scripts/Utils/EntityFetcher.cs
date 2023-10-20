using UnityEngine;
using System.Collections;

public class EntityFetcher : MonoBehaviour
{

    //No necesitsn instancia (propiedades de la clase)
    public static EntityFetcher Instance { get; private set; }
    public static GameObject PlayerRef => Instance.playerRef;
    public static GameObject GameManagerRef => Instance.gameManagerRef;

    //Necesitan una instancia para ser referenciadas
    private GameObject playerRef;
    private GameObject gameManagerRef;
    private const string PLAYER_TAG = "Player";


    //Awake happens before Start
    private void Awake()
    {
        Instance = this;
        this.playerRef = GameObject.FindWithTag(PLAYER_TAG);
        this.gameManagerRef = this.gameObject;
    }

}

