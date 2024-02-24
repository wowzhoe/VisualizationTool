using UnityEngine;
using UnityEngine.UI;

public class RoomList : MonoBehaviour
{
    [SerializeField]
    private Text _roomNameText;
    private Text RoomNameText
    {
        get { return _roomNameText; }
    }

    private void Start()
    {
        GameObject lobbyCanvasGO = CanvasManager.Instance.LobbyFunction.gameObject;
        if (lobbyCanvasGO == null) return;

        LobbyFunction lobbyFunction = lobbyCanvasGO.GetComponent<LobbyFunction>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => lobbyFunction.OnClickRoom(RoomNameText.text));
    }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public string RoomName { get; private set; }
    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }

    public bool Updated { get; set; }
}