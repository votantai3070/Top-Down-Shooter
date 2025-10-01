using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z - 5);
    }
}
