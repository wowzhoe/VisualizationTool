using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [SerializeField]
    private LobbyFunction _lobbyFunction;
    public LobbyFunction LobbyFunction
    {
        get { return _lobbyFunction; }
    }

    private void Awake()
    {
        Instance = this;
    }
}