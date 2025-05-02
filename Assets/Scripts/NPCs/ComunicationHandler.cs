using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Enums;
using TMPro;
using System.ComponentModel;
using Unity.VisualScripting;

public class CommunicationHandler : NPC, ICommunication
{
    [SerializeField] private GameObject chatWindow;

    [SerializeField] private GameObject opponent;
    [SerializeField] private Sprite[] opponentSprites; // �� opponent�� ������ ��������Ʈ �迭

    [SerializeField] private GameObject my;
    [SerializeField] private TextMeshProUGUI communicationName;
    [SerializeField] private TextMeshProUGUI communication;
    protected DoorAnimationHandler doorAnimationHandler;
    [SerializeField] private GameObject doorParent;

    private Queue<DialogueEntry> dialogueQueue;
    private bool isDialogueActive = false;
    private bool waitingForNextLine = false;

    private void Awake()
    {
        dialogueQueue = new Queue<DialogueEntry>();
        opponent.gameObject.SetActive(false);
        my.gameObject.SetActive(false);
        doorAnimationHandler = GetComponent<DoorAnimationHandler>();
    }

    private void Update()
    {
        if (chatWindow.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            SpeechLine();
        }
    }

    public void SetActiveChatWindow()
    {
        if (chatWindow != null)
        {
            chatWindow.SetActive(true);
        }
        else
        {
            Debug.LogError("chatWindow�� null�Դϴ�. Inspector�� GameObject�� �Ҵ��ߴ��� Ȯ���ϼ���.");
        }
    }
    public void CheckNPCNameAndChangeImage(string NPCName)
    {
        Image imageComponent = opponent.GetComponent<Image>();
        RectTransform rectTransform = imageComponent.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            switch (NPCName)
            {

                case "NPC_Legend":
                    communicationName.text = "������ ���谡";
                    imageComponent.sprite = opponentSprites[0];
                    rectTransform.sizeDelta = new Vector2(140f, 220f);  // �ʺ� 200, ���� 100���� ����

                    break;
                case "NPC_Angel":
                    communicationName.text = "õ��?";
                    imageComponent.sprite = opponentSprites[1];
                    rectTransform.sizeDelta = new Vector2(200f, 200f);  // �ʺ� 200, ���� 100���� ����

                    break;
                case "NPC_Demon":
                    communicationName.text = "����";
                    imageComponent.sprite = opponentSprites[2];
                    rectTransform.sizeDelta = new Vector2(200f, 200f);  // �ʺ� 200, ���� 100���� ����

                    break;
                case "NPC_Zombie":
                    communicationName.text = "����";
                    imageComponent.sprite = opponentSprites[3];
                    rectTransform.sizeDelta = new Vector2(200f, 200f);  // �ʺ� 200, ���� 100���� ����
                    break;
                case "Door_Lv1":
                case "Door_Lv2":
                case "Door_Lv3":
                    communicationName.text = "��";
                    DoorAnimationHandler handler = GetComponent<DoorAnimationHandler>();
                    if (handler != null)
                    {
                        handler.Lock(); // �޼��� �̸��� ��Ȳ�� ���� ����
                        Invoke("UnlockDoor", 1f);
                    }
                    break;
            }
        }
        Communication(NPCName);
        SpeechLine();
    }

    public void Communication(string NPCName)
    {
        switch (NPCName)
        {
            case "NPC_Legend":
                AddDialogueToQueue("��, �ڳ״� �����ΰ�?", ChangeOpponentImage);
                AddDialogueToQueue("��... �� �̸���...", ChangeMyImage);
                AddDialogueToQueue("��, �Ƴ�, ũ�� �߿��� ������ �ƴϴ�.", ChangeOpponentImage);
                AddDialogueToQueue("�׺��ٵ�, �� ���� �� ���� ���� ���̴°�?");
                AddDialogueToQueue("������ ����ִ� ���� ��ٸ��� ���� �ɼ�.");
                AddDialogueToQueue("�� �� ������ �� ��õ����.");
                AddDialogueToQueue("...", ChangeMyImage);
                break;
            case "NPC_Angel":
                AddDialogueToQueue("����� �����Ű���?", ChangeOpponentImage);
                AddDialogueToQueue("��, �ƾ��. PlappyKnight ���� �Ͻǰſ���?");
                AddDialogueToQueue("..?", ChangeMyImage);
                AddDialogueToQueue("������ �ȿ� ���� �־��.", ChangeOpponentImage);
                AddDialogueToQueue("�ϵ��� ������ ����� ������, �̰� �� ���� ���� �༮�� �� �� �����ٰɿ�?");
                AddDialogueToQueue("...", ChangeMyImage);
                AddDialogueToQueue("(�̷��� ��ģ���� õ�簡 �ֳ�..?)");
                break;
            case "NPC_Demon":
                AddDialogueToQueue("..?", ChangeOpponentImage);
                AddDialogueToQueue("����, �����غ��̴� �༮.");
                AddDialogueToQueue("�ּ� 1�ܰ� ������ ���� ����.");
                AddDialogueToQueue("(PlappyKnight�� ���� �;� �� ���ϴ�...)", ChangeMyImage);
                AddDialogueToQueue("...");
                AddDialogueToQueue("(�׷��� õ�纸�� ģ���ϳ�...)");
                break;
            case "NPC_Zombie":
                AddDialogueToQueue("����...", ChangeOpponentImage);
                AddDialogueToQueue("�׾���...");
                AddDialogueToQueue("��������...");
                AddDialogueToQueue("...", ChangeMyImage);
                AddDialogueToQueue("(���� ������ �ʴ� ���ϴ�.)");
                AddDialogueToQueue("(2�ܰ� ������ ���� ���� ����� ��������..?)");
                break;
            case "Door_Lv1":
            case "Door_Lv2":
            case "Door_Lv3":
                AddDialogueToQueue("������ �ʴ´�.");
                break;
        }
        
    }

    // ��縦 ť�� �߰�
    private void AddDialogueToQueue(string dialogue, System.Action onDisplay = null)
    {
        dialogueQueue.Enqueue(new DialogueEntry(dialogue, onDisplay));
    }

    // ��縦 ť���� �ϳ��� ������ ���
    public void SpeechLine()
    {
        if (dialogueQueue.Count > 0)
        {
            var entry = dialogueQueue.Dequeue();
            entry.OnDisplay?.Invoke(); // �̹��� �� �׼� ����
            communication.text = entry.Line;
            waitingForNextLine = true;
        }
        else if (waitingForNextLine)
        {
            CommunicationEnd();
            waitingForNextLine = false;
        }
    }

    public void ChangeOpponentImage()
    {
        opponent.gameObject.SetActive(true);
        my.gameObject.SetActive(false);
    }
    public void ChangeMyImage()
    {
        opponent.gameObject.SetActive(false);
        my.gameObject.SetActive(true);
    }

    public void CommunicationEnd()
    {
        chatWindow.SetActive(false);
    }

    void UnlockDoor()
    {
        DoorAnimationHandler handler = GetComponent<DoorAnimationHandler>();
        handler.UnLock();
    }

    private class DialogueEntry
    {
        public string Line;
        public System.Action OnDisplay;

        public DialogueEntry(string line, System.Action onDisplay = null)
        {
            Line = line;
            OnDisplay = onDisplay;
        }
    }

}