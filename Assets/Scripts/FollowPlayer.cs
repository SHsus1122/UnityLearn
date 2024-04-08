using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -7);

    // Start is called before the first frame update
    // Start 의 경우 게임이 시작되자마자 무언가를 해야 하는 특정 오브젝트가 있는 경우에 유용합니다.
    // 하지만, 사용하고 싶을 때만 특정 동작을 수행하는 오브젝트가 있는 경우(예로 벽을 움직이려는 경우)에 사용합니다.
    // 즉, 누군가 버튼을 누르면 벽에 대해 Awake 메서드가 호출되어 벽이 어떠한 동작을 하게 되는 경우를 말합니다.
    // 따라서 게임이 시작될 때가 아니라 오브젝트가 동작할 때 생성하려는 특정 값이 있을 경우에 사용하게 됩니다.
    void Start()
    {
        
    }

    // Update is called once per frame
    // LateUpdate 의 경우에는 카메라 위치를 계산할 때 특히 유용하며 플레이어를 따라가는 카메라에는 보통 이를 사용합니다.
    // 이렇게 하면 카메라가 플레이어의 움직임에 대한 모든 계산이 끝났음을 인식하고 게임이 업데이트 될 때 어디로 이동해야 하는지 파악하게 됩니다.
    void LateUpdate()
    {
        // Offset the camera behind the player by adding to the player's position
        transform.position = player.transform.position + offset;
    }
}
