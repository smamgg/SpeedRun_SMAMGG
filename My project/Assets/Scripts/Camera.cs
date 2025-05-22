using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public PlayerSlime player;
    private UnityEngine.Camera mainCam;

    public float baseSize = 60f;        // �⺻ �� ũ��
    public float zoomPerAccel = 1.12f;  // ���ӵ� 1�� �� ������
    public float zoomLerpSpeed = 3f;    // �� ���� �ӵ�
    public float minZoom = 50f;         // �ּ� �� (�����)
    public float maxZoom = 90f;         // �ִ� �� (�־���)


    private void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = followTarget.position;


        float targetFov = baseSize + player.accel * zoomPerAccel;
        targetFov = Mathf.Clamp(targetFov, minZoom, maxZoom);


        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFov, Time.deltaTime * zoomLerpSpeed);
    }
}