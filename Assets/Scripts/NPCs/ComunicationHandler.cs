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
    [SerializeField] private Sprite[] opponentSprites;

    [SerializeField] private GameObject my;
    [SerializeField] private TextMeshProUGUI communicationName;
    [SerializeField] private TextMeshProUGUI communication;
    protected DoorAnimationHandler doorAnimationHandler;

    public GameManager gameManager;

    private Queue<DialogueEntry> dialogueQueue;
    private bool isDialogueActive = false;
    private bool waitingForNextLine = false;

    private void Awake()
    {
        dialogueQueue = new Queue<DialogueEntry>();
        opponent.gameObject.SetActive(false);
        my.gameObject.SetActive(false);
        doorAnimationHandler = GetComponent<DoorAnimationHandler>();

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager를 찾을 수 없습니다. 씬에 존재하는지 확인하세요.");
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

    public void SetActiveChatWindow()
    {
        if (chatWindow != null)
        {
            chatWindow.SetActive(true);
        }
        else
        {
            Debug.LogError("chatWindow가 null입니다. Inspector에 GameObject를 할당했는지 확인하세요.");
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
                    communicationName.text = "전설의 모험가";
                    imageComponent.sprite = opponentSprites[0];
                    rectTransform.sizeDelta = new Vector2(140f, 220f);  // 너비 200, 높이 100으로 설정
                    break;
                case "NPC_Angel":
                    communicationName.text = "천사?";
                    imageComponent.sprite = opponentSprites[1];
                    rectTransform.sizeDelta = new Vector2(200f, 200f);  // 너비 200, 높이 100으로 설정
                    
                    break;
                case "NPC_Demon":
                    communicationName.text = "데몬";
                    imageComponent.sprite = opponentSprites[2];
                    rectTransform.sizeDelta = new Vector2(200f, 200f);  // 너비 200, 높이 100으로 설정

                    break;
                case "NPC_Zombie":
                    communicationName.text = "좀비";
                    imageComponent.sprite = opponentSprites[3];
                    rectTransform.sizeDelta = new Vector2(200f, 200f);  // 너비 200, 높이 100으로 설정
                    break;
                case "Door_Lv1":
                case "Door_Lv2":
                case "Door_Lv3":
                    communicationName.text = "문";
                    DoorAnimationHandler handler = GetComponentInChildren<DoorAnimationHandler>();
                    if (handler != null)
                    {
                        if (GameManager.instance.IsLv1DoorOpened)
                        {
                            GameManager.instance.OpenDoor_Lv1();
                        }
                        if (GameManager.instance.IsLv2DoorOpened)
                        {
                            GameManager.instance.OpenDoor_Lv2();
                        }
                        if (GameManager.instance.IsLv3DoorOpened)
                        {
                            GameManager.instance.OpenDoor_Lv3();
                        }
                        else
                            handler.Lock(); // 메서드 이름은 상황에 따라 변경
                        Invoke("UnlockDoor", 1f);
                    }
                    break;
                case "Door_Exit":
                    communicationName.text = "전설의 모험가";
                    imageComponent.sprite = opponentSprites[0];
                    rectTransform.sizeDelta = new Vector2(140f, 220f);  // 너비 200, 높이 100으로 설정
                    GameManager.instance.OpenDoor_Exit();
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
                AddDialogueToQueue("오, 자네는 누구인가?", ChangeOpponentImage);
                AddDialogueToQueue("어... 제 이름은...", ChangeMyImage);
                AddDialogueToQueue("아, 됐네, 크게 중요한 사항은 아니니.", ChangeOpponentImage);
                AddDialogueToQueue("그보다도, 저 앞의 세 개의 문이 보이는가?");
                AddDialogueToQueue("들어가보면 재미있는 일이 기다리고 있을 걸세.");
                AddDialogueToQueue("한 번 가보는 걸 추천하지.");
                AddDialogueToQueue("...", ChangeMyImage);
                break;
            case "NPC_Angel":
                if (GameManager.instance.IsLv1DoorOpened)
                {
                    AddDialogueToQueue("뭐야, 문 열린 거 못 봤어요?", ChangeOpponentImage);
                    AddDialogueToQueue("빨리 들어가요. 안에서 난이도 조절도 가능하니 알아서 하세요.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(한 대 쥐어박고 싶다...)", ChangeMyImage);
                }
                else
                {
                    AddDialogueToQueue("당신은 누구신가요?", ChangeOpponentImage);
                    AddDialogueToQueue("아, 됐어요. PlappyKnight 게임 하실거에요?");
                    AddDialogueToQueue("..?", ChangeMyImage);
                    AddDialogueToQueue("설명은 안에 들어가면 있어요.", ChangeOpponentImage);
                    AddDialogueToQueue("하든지 말든지 상관은 없지만, 이거 못 깨면 옆에 녀석이 문 안 열어줄걸요?");
                    AddDialogueToQueue("뭐, 첫 번째 문은 열어줄테니까 들어가보든가 해요.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(이렇게 불친절한 천사가 있나..?)");
                    GameManager.instance.IsLv1DoorOpened = true;
                }
                
                break;
            case "NPC_Demon":
                AddDialogueToQueue("..?", ChangeOpponentImage);
                AddDialogueToQueue("뭐야, 허접해보이는 녀석.");
                AddDialogueToQueue("최소 1단계 게임은 깨고 오라구.");
                AddDialogueToQueue("(PlappyKnight를 깨고 와야 할 듯하다...)", ChangeMyImage);
                AddDialogueToQueue("...");
                AddDialogueToQueue("(그래도 천사보단 친절하네...)");
                break;
            case "NPC_Zombie":
                AddDialogueToQueue("으어...", ChangeOpponentImage);
                AddDialogueToQueue("그어어어...");
                AddDialogueToQueue("끄어어어어어...");
                AddDialogueToQueue("...", ChangeMyImage);
                AddDialogueToQueue("(말이 통하질 않는 듯하다.)");
                AddDialogueToQueue("(2단계 게임을 깨고 오면 방법이 생길지도..?)");
                break;
            case "Door_Lv1":
                if (!GameManager.instance.IsLv1DoorOpened) AddDialogueToQueue("열리지 않는다.", ChangeMyImage);
                break;
            case "Door_Lv2":
                if (!GameManager.instance.IsLv2DoorOpened) AddDialogueToQueue("열리지 않는다.", ChangeMyImage);
                break;
            case "Door_Lv3":
                if (!GameManager.instance.IsLv3DoorOpened) AddDialogueToQueue("열리지 않는다.", ChangeMyImage);
                break;
            case "Door_Exit":
                if (!GameManager.instance.IsExit)
                {
                    AddDialogueToQueue("오, 나가고 싶은건가?", ChangeOpponentImage);
                    AddDialogueToQueue("굳이 나가겠다면 말리지야 않겠네.");
                    AddDialogueToQueue("문을 열어주지, 안녕히 가시게.");
                    GameManager.instance.IsExit = true;
                }
                
                break;

                /*
                AddDialogueToQueue("문을 열어주지,안녕히 가시게.", () =>
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; // 에디터 모드 종료
#else
            Application.Quit(); // 빌드된 앱 종료
#endif
                });
                */
                break;
        }

    }

    // 대사를 큐에 추가
    private void AddDialogueToQueue(string dialogue, System.Action onDisplay = null)
    {
        dialogueQueue.Enqueue(new DialogueEntry(dialogue, onDisplay));
    }

    // 대사를 큐에서 하나씩 꺼내서 출력
    public void SpeechLine()
    {
        if (dialogueQueue.Count > 0)
        {
            var entry = dialogueQueue.Dequeue();
            entry.OnDisplay?.Invoke(); // 이미지 등 액션 실행
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