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
    [SerializeField] private GameObject ExitAlertWindow;
    

    [SerializeField] private GameObject opponent;
    [SerializeField] private Sprite[] opponentSprites;

    [SerializeField] private GameObject my;
    [SerializeField] private TextMeshProUGUI communicationName;
    [SerializeField] private TextMeshProUGUI communication;
    protected DoorAnimationHandler doorAnimationHandler;

    public SystemManager systemManager;

    private Queue<DialogueEntry> dialogueQueue;
    private bool isDialogueActive = false;
    private bool waitingForNextLine = false;

    private void Awake()
    {
        dialogueQueue = new Queue<DialogueEntry>();
        opponent.gameObject.SetActive(false);
        my.gameObject.SetActive(false);
        doorAnimationHandler = GetComponent<DoorAnimationHandler>();

       // ExitAlertWindow.SetActive(false);

        if (systemManager == null)
        {
            systemManager = FindObjectOfType<SystemManager>();
            if (systemManager == null)
            {
                Debug.LogError("GameManager�� ã�� �� �����ϴ�. ���� �����ϴ��� Ȯ���ϼ���.");
            }
        }
    }

    private void Update()
    {
        if (chatWindow.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            SpeechLine();
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
                    communicationName.text = "��";
                    DoorAnimationHandler handler1 = GetComponentInChildren<DoorAnimationHandler>();
                    if (handler1 != null)
                    {
                        if (SystemManager.instance.IsLv1DoorOpened)
                        {
                            SystemManager.instance.OpenDoor_Lv1();
                        }
                        else
                        {
                            handler1.Lock(); // ��� �ִϸ��̼�
                            Invoke("UnlockDoor", 1f);
                        }
                    }
                    break;

                case "Door_Lv2":
                    communicationName.text = "��";
                    DoorAnimationHandler handler2 = GetComponentInChildren<DoorAnimationHandler>();
                    if (handler2 != null)
                    {
                        if (SystemManager.instance.IsLv2DoorOpened)
                        {
                            SystemManager.instance.OpenDoor_Lv2();
                        }
                        else
                        {
                            handler2.Lock();
                            Invoke("UnlockDoor", 1f);
                        }
                    }
                    break;

                case "Door_Lv3":
                    communicationName.text = "��";
                    DoorAnimationHandler handler3 = GetComponentInChildren<DoorAnimationHandler>();
                    if (handler3 != null)
                    {
                        if (SystemManager.instance.IsLv3DoorOpened)
                        {
                            SystemManager.instance.OpenDoor_Lv3();
                        }
                        else
                        {
                            handler3.Lock();
                            Invoke("UnlockDoor", 1f);
                        }
                    }
                    break;
                case "Door_Exit":
                    communicationName.text = "������ ���谡";
                    imageComponent.sprite = opponentSprites[0];
                    rectTransform.sizeDelta = new Vector2(140f, 220f);  // �ʺ� 200, ���� 100���� ����
                    SystemManager.instance.OpenDoor_Exit();
                    break;
                case "Ladder_Lv1":
                    {
                        SystemManager.instance.Init_Level1();
                    }
                    break;
                case "Ladder_Lv2":
                    {
                        SystemManager.instance.Init_Level2();
                    }
                    break;
                case "Ladder_Lv3":
                    {
                        SystemManager.instance.Init_Level3();
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
                if (GameManager.Instance.isLv3Clear)
                {
                    AddDialogueToQueue("��, ��ħ��...", ChangeOpponentImage);
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("��ħ��, ��� ������ Ŭ������ �ڰ� ��Ÿ���ٴ�!", ChangeOpponentImage);
                    AddDialogueToQueue("���� �����ϳ�, ������ ��û�� ������ �ְڳ�.");
                    AddDialogueToQueue("�װ� �ٷ�...");
                    AddDialogueToQueue("���� ��带 �ر����ְڳ�! ���� ��ǥ�� ��� ���� �ְ� ������ ����� �� �����ž�!");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("...", ChangeOpponentImage);
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("...", ChangeOpponentImage);
                    AddDialogueToQueue("�׷��Գ� ��۰�?");
                }
                else
                {
                    AddDialogueToQueue("��, �ڳ״� �����ΰ�?", ChangeOpponentImage);
                    AddDialogueToQueue("��... �� �̸���...", ChangeMyImage);
                    AddDialogueToQueue("��, �Ƴ�, ũ�� �߿��� ������ �ƴϴ�.", ChangeOpponentImage);
                    AddDialogueToQueue("�׺��ٵ�, �� ���� �� ���� ���� ���̴°�?");
                    AddDialogueToQueue("������ ����ִ� ���� ��ٸ��� ���� �ɼ�.");
                    AddDialogueToQueue("�� �� ������ �� ��õ����.");
                    AddDialogueToQueue("...", ChangeMyImage);
                }                    
                break;
            case "NPC_Angel":
                if (SystemManager.instance.IsLv1DoorOpened && SystemManager.instance.IsLv2DoorOpened && GameManager.Instance.isLv3Clear)
                {
                    AddDialogueToQueue("����? 3�ܰ赵 ���ٱ���?", ChangeOpponentImage);
                    AddDialogueToQueue("�����ؿ�. �̰� �����̿���.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(�� ���ϵ� �� �� �ƴ±���...)");
                    AddDialogueToQueue("�ٵ� ������ �� �� �ð��� �ֳ���?", ChangeOpponentImage);
                    AddDialogueToQueue("�� �ؿ� ������ �����簡 ��ٸ��� �ִ°ɿ�?");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(�׷� �׷���...)");
                    break;
                }
                if (SystemManager.instance.IsLv1DoorOpened && SystemManager.instance.IsLv2DoorOpened && SystemManager.instance.IsLv3DoorOpened)
                {
                    AddDialogueToQueue("��? 2�ܰ赵 �����?", ChangeOpponentImage);
                    AddDialogueToQueue("���� ����Ͻó׿�. ����ŭ�� �ƴ�����.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("�ٵ� �� �ڲ� �Ϻη� �� �ɷ� ������?", ChangeOpponentImage);
                    AddDialogueToQueue("�ɽ��Ͻ� �� �˰ڴµ�, ���� �ؾ��� ���� �������ݾƿ�?");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(�� õ�� �¾�?)", ChangeMyImage);
                    break;
                }
                if (SystemManager.instance.IsLv1DoorOpened && SystemManager.instance.IsLv2DoorOpened)
                {
                    AddDialogueToQueue("��, 1�ܰ� ���̳׿�?", ChangeOpponentImage);
                    AddDialogueToQueue("�����ؿ�. �ٵ� �׷��� ����� ���� �ƴϳ׿�.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("���� �� �������״ϱ� 2�ܰ� �Ϸ�������.", ChangeOpponentImage);
                    AddDialogueToQueue("�� �� ���Կ�.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(�̰� �αǼ��Ϳ� ��� �� �ϳ�..?)", ChangeMyImage);
                    break;
                }
                if (SystemManager.instance.IsLv1DoorOpened)
                {
                    AddDialogueToQueue("����, �� ���� �� �� �þ��?", ChangeOpponentImage);
                    AddDialogueToQueue("���� ����. �ȿ��� ���̵� ������ �����ϴ� �˾Ƽ� �ϼ���.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(�� �� ���ڰ� �ʹ�...)", ChangeMyImage);
                    break;
                }
                else
                {
                    AddDialogueToQueue("����� �����Ű���?", ChangeOpponentImage);
                    AddDialogueToQueue("��, �ƾ��. �ȿ� ���ǰſ���?");
                    AddDialogueToQueue("..?", ChangeMyImage);
                    AddDialogueToQueue("���� ������ �ȿ� ���� �־��.", ChangeOpponentImage);
                    AddDialogueToQueue("�ϵ��� ������ ����� ������, �̰� �� ���� ���� �༮�� �� �� �����ٰɿ�?");
                    AddDialogueToQueue("��, ù ��° ���� �������״ϱ� �����簡 �ؿ�.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(�̷��� ��ģ���� õ�簡 �ֳ�..?)");
                    SystemManager.instance.IsLv1DoorOpened = true;
                }
                break;
                
            case "NPC_Demon":
                if (SystemManager.instance.IsLv2DoorOpened && GameManager.Instance.isLv3Clear)
                {
                    AddDialogueToQueue("����, 3�ܰ踦 �� �ǰ�!", ChangeOpponentImage);
                    AddDialogueToQueue("���� �Ǹ��ϱ�! Ư���� �׸��ϱ����̶� �ϳ� ���ְڳ�!");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(�� �̷��� �ϱ⸦ ��������?)");
                    AddDialogueToQueue("�ؿ� �����翡�� ������.", ChangeOpponentImage);
                    AddDialogueToQueue("�и� �ڳ׿��� ���� ������ �� �ɼ�.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(������.)");
                    break;
                }
                if (SystemManager.instance.IsLv2DoorOpened && SystemManager.instance.IsLv3DoorOpened)
                {
                    AddDialogueToQueue("��. �Ǹ��ϰ� 2�ܰ踦 Ŭ�����س±���.", ChangeOpponentImage);
                    AddDialogueToQueue("���߳�. ���� �ϱ��忡 �ᵵ ����.");
                    AddDialogueToQueue("(...�ϱ���?)", ChangeMyImage);
                    AddDialogueToQueue("���� �������� ������ �ʰڳ�?", ChangeOpponentImage);
                    AddDialogueToQueue("��û �⻵�� �Ͱ�����.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    break;
                }
                if (SystemManager.instance.IsLv2DoorOpened)
                {
                    AddDialogueToQueue("��, 1�ܰ� ������ ���� �Ա�.", ChangeOpponentImage);
                    AddDialogueToQueue("�׷��� �ּ����� �Ƿ��� �ִ� �Ͱ�����.");
                    AddDialogueToQueue("���� ��������. ���� ������ �� �� �����ž�.");
                    AddDialogueToQueue("������. ģ��.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(���� �� �༮ ģ���ϳ�...)");
                }
                else
                {
                    AddDialogueToQueue("..?", ChangeOpponentImage);
                    AddDialogueToQueue("����, �����غ��̴� �༮.");
                    AddDialogueToQueue("�ּ� 1�ܰ� ������ ���� ����.");
                    AddDialogueToQueue("(1�ܰ踦 ���� �;� �� ���ϴ�...)", ChangeMyImage);
                    AddDialogueToQueue("...");
                    AddDialogueToQueue("(�׷��� õ�纸�� ģ���ϳ�...)");
                }
                    break;

            case "NPC_Zombie":
                if (GameManager.Instance.isLv3Clear)
                {
                    AddDialogueToQueue("�ƴ�!!! 3�ܰ踦 ���ٰ�!!!", ChangeOpponentImage);
                    AddDialogueToQueue("�̷��� ��־ִ��� ����!! �ؿ� �����簡 ���� �����Ұɼ�!!!!");
                    AddDialogueToQueue("�� ���� �����Ϥ�...");
                    AddDialogueToQueue("...");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("...?");
                    AddDialogueToQueue("�����...", ChangeOpponentImage);
                    AddDialogueToQueue("�������...");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(��ݿ� �ٽ� ���� �Ҿ���� ���ϴ�...)");
                    break;
                }
                if (SystemManager.instance.IsLv3DoorOpened)
                {
                    AddDialogueToQueue("����...", ChangeOpponentImage);
                    AddDialogueToQueue("�׾���...");
                    AddDialogueToQueue("��������...?");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("���� ģ��! 2�ܰ� ���ӱ��� ���� �� �ǰ�!", ChangeOpponentImage);
                    AddDialogueToQueue("...??", ChangeMyImage);
                    AddDialogueToQueue("��־־ִ��ϱ���! �� �ҽ��� �������� 2�ܰ�� �� �����µ�.", ChangeOpponentImage);
                    AddDialogueToQueue("�̰��̰�, �ʹ� ��� 500�Ⱓ ����ִ� �� ������ �� Ʈ�����ȱ���!");
                    AddDialogueToQueue("����������������!!");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(�̷� ĳ���Ϳ���..?)");
                    AddDialogueToQueue("���� ��������, ������ ���ӵ� ������� ģ��!", ChangeOpponentImage);
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(��¥ õ�� ����� �� ģ���ϳ�...)");
                }
                else
                {
                    AddDialogueToQueue("����...", ChangeOpponentImage);
                    AddDialogueToQueue("�׾���...");
                    AddDialogueToQueue("��������...");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(���� ������ �ʴ� ���ϴ�.)");
                    AddDialogueToQueue("(2�ܰ� ������ ���� ���� ����� ��������..?)");
                }

                    break;
            case "Door_Lv1":
                if (!SystemManager.instance.IsLv1DoorOpened) AddDialogueToQueue("������ �ʴ´�.", ChangeMyImage);
                break;
            case "Door_Lv2":
                if (!SystemManager.instance.IsLv2DoorOpened) AddDialogueToQueue("������ �ʴ´�.", ChangeMyImage);
                break;
            case "Door_Lv3":
                if (!SystemManager.instance.IsLv3DoorOpened) AddDialogueToQueue("������ �ʴ´�.", ChangeMyImage);
                break;
            case "Door_Exit":
                if (!SystemManager.instance.IsExit)
                {
                    AddDialogueToQueue("��, ������ �����ǰ�?", ChangeOpponentImage);
                    AddDialogueToQueue("���� �����ڴٸ� �������� �ʰڳ�.");
                    AddDialogueToQueue("���� ��������, �ȳ��� ���ð�.");
                    SystemManager.instance.IsExit = true;
                }
                break;
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

    public void SetActiveExit()
    {
        SystemManager.instance.Exit();
    }

    public void CheckExit()
    {
        SetActiveExit();
    }

    public void SetActiveLv1GameStartWindow()
    {
        if (ExitAlertWindow != null)
        {
            ExitAlertWindow.SetActive(true);
            Debug.Log("ExitAlertWindow Ȱ��ȭ.");
        }
        else
        {
            Debug.LogError("ExitAlertWindow null�Դϴ�. Inspector�� GameObject�� �Ҵ��ߴ��� Ȯ���ϼ���.");
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
        chatWindow.gameObject.SetActive(false);
        if (ExitAlertWindow) ExitAlertWindow.SetActive(false);
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