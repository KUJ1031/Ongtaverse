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
                Debug.Log("��ȣ�ۿ� : " + collision.gameObject.name);
                communicationHandler.SetActiveChatWindow();
                communicationHandler.CheckNPCNameAndChangeImage(collision.gameObject.name);

            }
            else
            {
                Debug.LogError("communicationHandler�� null�Դϴ�. �浹�� NPC���� ������Ʈ�� �����Դ��� Ȯ���ϼ���.");
            }

            isColliding = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false; // �浹 ���� �� ���� �ʱ�ȭ
        communicationHandler.CommunicationEnd();
        Debug.Log(isColliding);
    }
}
