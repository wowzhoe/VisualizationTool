using UnityEngine;

public class PlayerGameNetwork : MonoBehaviour
{
    public static PlayerGameNetwork Instance;
    public string Name { get; private set; }

    // Update is called once per frame
    private void Awake()
    {
        DontDestroyOnLoad(this);

        Instance = this;

        Name = "Player #" + Random.Range(0, 9999);
    }
}