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
                    communicationName.text = "문";
                    DoorAnimationHandler handler1 = GetComponentInChildren<DoorAnimationHandler>();
                    if (handler1 != null)
                    {
                        if (SystemManager.instance.IsLv1DoorOpened)
                        {
                            SystemManager.instance.OpenDoor_Lv1();
                        }
                        else
                        {
                            handler1.Lock(); // 잠김 애니메이션
                            Invoke("UnlockDoor", 1f);
                        }
                    }
                    break;

                case "Door_Lv2":
                    communicationName.text = "문";
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
                    communicationName.text = "문";
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
                    communicationName.text = "전설의 모험가";
                    imageComponent.sprite = opponentSprites[0];
                    rectTransform.sizeDelta = new Vector2(140f, 220f);  // 너비 200, 높이 100으로 설정
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
                    AddDialogueToQueue("오, 마침내...", ChangeOpponentImage);
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("마침내, 모든 게임을 클리어한 자가 나타나다니!", ChangeOpponentImage);
                    AddDialogueToQueue("정말 축하하네, 상으로 엄청난 선물을 주겠네.");
                    AddDialogueToQueue("그건 바로...");
                    AddDialogueToQueue("무한 모드를 해금해주겠네! 이제 목표량 상관 없이 최고 점수를 노려볼 수 있을거야!");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("...", ChangeOpponentImage);
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("...", ChangeOpponentImage);
                    AddDialogueToQueue("그렇게나 기쁜가?");
                }
                else
                {
                    AddDialogueToQueue("오, 자네는 누구인가?", ChangeOpponentImage);
                    AddDialogueToQueue("어... 제 이름은...", ChangeMyImage);
                    AddDialogueToQueue("아, 됐네, 크게 중요한 사항은 아니니.", ChangeOpponentImage);
                    AddDialogueToQueue("그보다도, 저 앞의 세 개의 문이 보이는가?");
                    AddDialogueToQueue("들어가보면 재미있는 일이 기다리고 있을 걸세.");
                    AddDialogueToQueue("한 번 가보는 걸 추천하지.");
                    AddDialogueToQueue("...", ChangeMyImage);
                }                    
                break;
            case "NPC_Angel":
                if (SystemManager.instance.IsLv1DoorOpened && SystemManager.instance.IsLv2DoorOpened && GameManager.Instance.isLv3Clear)
                {
                    AddDialogueToQueue("에엥? 3단계도 깼다구요?", ChangeOpponentImage);
                    AddDialogueToQueue("축하해요. 이건 진심이에요.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(얘 축하도 할 줄 아는구나...)");
                    AddDialogueToQueue("근데 저한테 말 걸 시간이 있나요?", ChangeOpponentImage);
                    AddDialogueToQueue("저 밑에 전설의 마법사가 기다리고 있는걸요?");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(그럼 그렇지...)");
                    break;
                }
                if (SystemManager.instance.IsLv1DoorOpened && SystemManager.instance.IsLv2DoorOpened && SystemManager.instance.IsLv3DoorOpened)
                {
                    AddDialogueToQueue("엥? 2단계도 깼어요?", ChangeOpponentImage);
                    AddDialogueToQueue("조금 대단하시네요. 저만큼은 아니지만.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("근데 왜 자꾸 일부러 말 걸러 오시죠?", ChangeOpponentImage);
                    AddDialogueToQueue("심심하신 건 알겠는데, 아직 해야할 일이 남으셨잖아요?");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(얘 천사 맞아?)", ChangeMyImage);
                    break;
                }
                if (SystemManager.instance.IsLv1DoorOpened && SystemManager.instance.IsLv2DoorOpened)
                {
                    AddDialogueToQueue("오, 1단계 깨셨네요?", ChangeOpponentImage);
                    AddDialogueToQueue("축하해요. 근데 그렇게 대단한 일은 아니네요.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("옆에 문 열렸을테니까 2단계 하러가세요.", ChangeOpponentImage);
                    AddDialogueToQueue("전 좀 쉴게요.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(이거 인권센터에 고발 못 하나..?)", ChangeMyImage);
                    break;
                }
                if (SystemManager.instance.IsLv1DoorOpened)
                {
                    AddDialogueToQueue("뭐야, 문 열린 거 못 봤어요?", ChangeOpponentImage);
                    AddDialogueToQueue("빨리 들어가요. 안에서 난이도 조절도 가능하니 알아서 하세요.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(한 대 쥐어박고 싶다...)", ChangeMyImage);
                    break;
                }
                else
                {
                    AddDialogueToQueue("당신은 누구신가요?", ChangeOpponentImage);
                    AddDialogueToQueue("아, 됐어요. 안에 들어가실거에요?");
                    AddDialogueToQueue("..?", ChangeMyImage);
                    AddDialogueToQueue("게임 설명은 안에 들어가면 있어요.", ChangeOpponentImage);
                    AddDialogueToQueue("하든지 말든지 상관은 없지만, 이거 못 깨면 옆에 녀석이 문 안 열어줄걸요?");
                    AddDialogueToQueue("뭐, 첫 번째 문은 열어줄테니까 들어가보든가 해요.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(이렇게 불친절한 천사가 있나..?)");
                    SystemManager.instance.IsLv1DoorOpened = true;
                }
                break;
                
            case "NPC_Demon":
                if (SystemManager.instance.IsLv2DoorOpened && GameManager.Instance.isLv3Clear)
                {
                    AddDialogueToQueue("뭐야, 3단계를 깬 건가!", ChangeOpponentImage);
                    AddDialogueToQueue("정말 훌륭하군! 특별히 그림일기장이라도 하나 사주겠네!");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(왜 이렇게 일기를 좋아하지?)");
                    AddDialogueToQueue("밑에 마법사에게 가보게.", ChangeOpponentImage);
                    AddDialogueToQueue("분명 자네에게 어마어마한 선물을 줄 걸세.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(가보자.)");
                    break;
                }
                if (SystemManager.instance.IsLv2DoorOpened && SystemManager.instance.IsLv3DoorOpened)
                {
                    AddDialogueToQueue("오. 훌륭하게 2단계를 클리어해냈구만.", ChangeOpponentImage);
                    AddDialogueToQueue("잘했네. 오늘 일기장에 써도 좋아.");
                    AddDialogueToQueue("(...일기장?)", ChangeMyImage);
                    AddDialogueToQueue("옆에 좀비한테 가보지 않겠나?", ChangeOpponentImage);
                    AddDialogueToQueue("엄청 기뻐할 것같구만.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    break;
                }
                if (SystemManager.instance.IsLv2DoorOpened)
                {
                    AddDialogueToQueue("오, 1단계 게임을 깨고 왔군.", ChangeOpponentImage);
                    AddDialogueToQueue("그래도 최소한의 실력은 있는 것같구만.");
                    AddDialogueToQueue("문을 열어주지. 이제 앞으로 갈 수 있을거야.");
                    AddDialogueToQueue("힘내라구. 친구.");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(역시 이 녀석 친절하네...)");
                }
                else
                {
                    AddDialogueToQueue("..?", ChangeOpponentImage);
                    AddDialogueToQueue("뭐야, 허접해보이는 녀석.");
                    AddDialogueToQueue("최소 1단계 게임은 깨고 오라구.");
                    AddDialogueToQueue("(1단계를 깨고 와야 할 듯하다...)", ChangeMyImage);
                    AddDialogueToQueue("...");
                    AddDialogueToQueue("(그래도 천사보단 친절하네...)");
                }
                    break;

            case "NPC_Zombie":
                if (GameManager.Instance.isLv3Clear)
                {
                    AddDialogueToQueue("아니!!! 3단계를 깼다고!!!", ChangeOpponentImage);
                    AddDialogueToQueue("이렇게 대애애단할 수가!! 밑에 마법사가 정말 좋아할걸세!!!!");
                    AddDialogueToQueue("내 정말 축하하ㄴ...");
                    AddDialogueToQueue("...");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("...?");
                    AddDialogueToQueue("으어어...", ChangeOpponentImage);
                    AddDialogueToQueue("끄어어어어...");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(충격에 다시 말을 잃어버린 듯하다...)");
                    break;
                }
                if (SystemManager.instance.IsLv3DoorOpened)
                {
                    AddDialogueToQueue("으어...", ChangeOpponentImage);
                    AddDialogueToQueue("그어어어...");
                    AddDialogueToQueue("끄어어어어어...?");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("뭐야 친구! 2단계 게임까지 깨고 온 건가!", ChangeOpponentImage);
                    AddDialogueToQueue("...??", ChangeMyImage);
                    AddDialogueToQueue("대애애애단하구만! 내 소싯적 시절에도 2단계는 못 깼었는데.", ChangeOpponentImage);
                    AddDialogueToQueue("이거이거, 너무 놀라서 500년간 잠겨있던 내 말문이 다 트여버렸구만!");
                    AddDialogueToQueue("하하하하하하하하!!");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(이런 캐릭터였나..?)");
                    AddDialogueToQueue("문을 열어주지, 마지막 게임도 힘내라고 친구!", ChangeOpponentImage);
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(진짜 천사 빼고는 다 친절하네...)");
                }
                else
                {
                    AddDialogueToQueue("으어...", ChangeOpponentImage);
                    AddDialogueToQueue("그어어어...");
                    AddDialogueToQueue("끄어어어어어...");
                    AddDialogueToQueue("...", ChangeMyImage);
                    AddDialogueToQueue("(말이 통하질 않는 듯하다.)");
                    AddDialogueToQueue("(2단계 게임을 깨고 오면 방법이 생길지도..?)");
                }

                    break;
            case "Door_Lv1":
                if (!SystemManager.instance.IsLv1DoorOpened) AddDialogueToQueue("열리지 않는다.", ChangeMyImage);
                break;
            case "Door_Lv2":
                if (!SystemManager.instance.IsLv2DoorOpened) AddDialogueToQueue("열리지 않는다.", ChangeMyImage);
                break;
            case "Door_Lv3":
                if (!SystemManager.instance.IsLv3DoorOpened) AddDialogueToQueue("열리지 않는다.", ChangeMyImage);
                break;
            case "Door_Exit":
                if (!SystemManager.instance.IsExit)
                {
                    AddDialogueToQueue("오, 나가고 싶은건가?", ChangeOpponentImage);
                    AddDialogueToQueue("굳이 나가겠다면 말리지야 않겠네.");
                    AddDialogueToQueue("문을 열어주지, 안녕히 가시게.");
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
            Debug.LogError("chatWindow가 null입니다. Inspector에 GameObject를 할당했는지 확인하세요.");
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
            Debug.Log("ExitAlertWindow 활성화.");
        }
        else
        {
            Debug.LogError("ExitAlertWindow null입니다. Inspector에 GameObject를 할당했는지 확인하세요.");
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