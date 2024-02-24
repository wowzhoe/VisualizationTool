using UnityEngine;
using Photon.Pun;

namespace VisualizationTool.Character
{
    public class CharacterAdjustment : MonoBehaviourPun
    {
        [PunRPC]
        public void RPC_ShareView(Vector3 pos, Vector3 rot)
        {
            Character[] userList = FindObjectsOfType<Character>();
            foreach (Character userComponent in userList)
            {
                if (!userComponent.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    userComponent.transform.position = pos;
                    userComponent.transform.eulerAngles = new Vector3(rot.x, rot.y, rot.z);
                }
            }
        }
    }
}