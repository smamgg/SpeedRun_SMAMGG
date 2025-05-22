using UnityEngine;

public class PlayerFollower : MonoBehaviour
{

    public Transform playerLocation; 
    public Vector3 offset = new Vector3(0f, 0f, -20f);  // �÷��̾� ������ ������ ����
    public float followSpeed = 7f;  // �ӵ� ����
    public PlayerSlime player; 


    void FixedUpdate()
    {
        if (playerLocation == null) return; 

        // target location = player location + offset 
        Vector3 targetPosition = playerLocation.position + offset; 

        // smoothly move
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed ); 
    }
}