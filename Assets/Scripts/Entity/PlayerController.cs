using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera;
    private bool isColliding = false;

    CommunicationHandler communicationHandler;

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        communicationHandler = collision.gameObject.GetComponent<CommunicationHandler>();
        Debug.Log(isColliding);
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isColliding && Input.GetKey(KeyCode.Space))
        {
            if (communicationHandler != null)
            {
                Debug.Log("상호작용 : " + collision.gameObject.name);
                communicationHandler.SetActiveChatWindow();
                communicationHandler.CheckNPCNameAndChangeImage(collision.gameObject.name);

            }
            else
            {
                Debug.LogError("communicationHandler가 null입니다. 충돌한 NPC에서 컴포넌트를 가져왔는지 확인하세요.");
            }

            isColliding = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false; // 충돌 종료 시 상태 초기화
        communicationHandler.CommunicationEnd();
        Debug.Log(isColliding);
    }
}
