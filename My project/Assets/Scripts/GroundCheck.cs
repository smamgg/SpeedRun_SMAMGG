using UnityEngine;
using static UnityEngine.UI.Image;

public class GroundCheck : MonoBehaviour
{
    public PlayerSlime player;
    Rigidbody2D rigid;
    Vector2 boxSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        boxSize = new Vector2(transform.localScale.x * 1f, transform.localScale.y * 1f); // 아래 짧게 판정);
    }

    // Update is called once per frame
    void Update()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
        CheckingGrounded();
    }



    void CheckingGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
        origin: transform.position,
        size: boxSize,
        angle: 0f,
        direction: Vector2.zero,
        distance: 0.1f,
        layerMask: LayerMask.GetMask("Ground"));

        if (rigid.linearVelocity.y < 0) // y값 감지해서 내려갈때만 스캔
        {
            if (hit.collider != null)
            {
                if (hit.distance < 0.5f)
                {
                    //점프중인지 아닌지 판단해서 상태변경
                    player.isGrounded = true;
                    player.SetState(PlayerSlime.PlayerState.Normal);
                }
            }
            if (hit.collider == null || hit.distance >= 0.5f) //  바닥에 닿지 않았는지 체크
            {
                //점프중인지 2단체크, 아니라면 점프중으로 변경
                player.isGrounded = false;
                if (player.currentState != PlayerSlime.PlayerState.Jumping)
                {
                    player.SetState(PlayerSlime.PlayerState.Jumping);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // 기준 위치 (이 오브젝트의 위치 + 오프셋)
        Vector2 origin = (Vector2)transform.position;
        Vector2 halfSize = boxSize * 0.5f;

        // 꼭짓점 계산
        Vector2 topLeft = origin + new Vector2(-halfSize.x, halfSize.y);
        Vector2 topRight = origin + new Vector2(halfSize.x, halfSize.y);
        Vector2 bottomRight = origin + new Vector2(halfSize.x, -halfSize.y);
        Vector2 bottomLeft = origin + new Vector2(-halfSize.x, -halfSize.y);

        // 사각형 그리기
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}

