using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class UnUseBlockMain : MonoBehaviour
{
    public GameObject ItemObject;

    private GameObject panel;

    private GameObject upDownPanel;
    public Text okCancelText;

    private GameObject okCancelPanel;
    private GameObject ButtonAdd;
    private GameObject buttonHighlightStart;
    private GameObject buttonHighlightCondition;
    private GameObject buttonHighlightEnd;

    private GameObject unUseScrollView;
    private GameObject useScrollView;

    public Transform Content;
    public Transform UseContent;

    public List<Item> ItemList;
    public List<Item> UsedItemList;

    public Text title;
    public Text CurText;
    public InputField ConInput;

    private bool flagAdd = true;
    private bool flagAll = true;

    //같은 함수인지 확인하기 위한 플래그
    private int nowFx = -1;
    //동작 확인 판넬 플래그
    private int panelFlag = 0;
    //각 블록을 식별할 수 있는 내부 인덱스
    private int useIndex = 0;
    //함수 짝을 맞추기 위한 인덱스
    private int fxIndex = 0;
    //함수에 블록을 추가할 때 위치를 잡아주는 인덱스
    private int fxstr = 0;
    //조건 입력 창에서 사용되는 인덱스
    private int conIndex = 0;
    //스테이지 시작 인덱스를 기억 static 뿐만아니라 게임이 실행되는 내내 보관되어야하는 형태면 가능
    private int stageIndex = 0;
    //스테이지 타이틀을 달기 위한 인덱스 1부터 시작
    public static int stageTitleIndex = 0;
    //스테이지마다 사용될 블록 배열
    private string[] stage = {"0번","9","12","15","22",
                              "29","36","44","64",
                              "print","\"\"","/",
                              "print","Subject","/",
                              "print","X","+","Y","\"\"",",","/",
                              "print","X","+","Y","\"\"",",","/",
                              "if","모자","빨간색","==","print","\"\"","/",
                              "if","문자","1","==","print","\"\"","else","/",
                              "if","체크카드 잔액","=","메뉴","10000","8000","6000","4000","2000",">=","print","치킨","elif","떡볶이","else","짜장면","학식","라면","굶기","/",
                              "if","과제","==","0","&&","벚꽃","1","print","\"\"","/"};

    //스테이지 타이틀
    private string[] stageTitle = {"0번","첫 학기, 새 친구에게 인사하기","어떤 수업을 들을까","자료형을 이해하자","복합 프린트","특정 친구에게 인사하기","휴강이 될까요","체크카드에 얼마가 있을까요","과제와 벚꽃 시즌"};
    //함수 배열
    private string[] fx = { "print", "if","else","elif"};
    //변수 배열
    private string[] var = { "\"\"", "Subject", "X", "Y" }; 
    //부가적인 문자
    private string[] extraChar = { "+", "==", "모자", "빨간색" ,",","문자", "&&", "벚꽃",
                                   "과제","1","0",">=","치킨","떡볶이","짜장면","학식","라면",
                                   "굶기","10000","8000","6000","4000","2000","체크카드 잔액",
                                   "=","메뉴"};

    //사용할 버튼 리스트
    private void AddListItem()
    {
        Item itemTemp;
        // 타이틀 달아줌
        if (DataControl.where == 1)
        {
            stageIndex = Convert.ToInt32(stage[ReviewScript.clickedStage]);
            title.text = stageTitle[ReviewScript.clickedStage];
        }
        else
        {
            stageIndex = Convert.ToInt32(stage[stageTitleIndex]);
            title.text = stageTitle[stageTitleIndex];
        }
        
        /* //스테이지별 시작 인덱스 테스트용
        for (int i = 1; i <= 8; i++)
        {

            itemTemp = new Item();
            int position = i;

            itemTemp.Name = stage[Convert.ToInt32(stage[i])];

            itemTemp.OnItemClick = new Button.ButtonClickedEvent();
            itemTemp.OnItemClick.AddListener(() => ItemClick_Add(position));

            this.ItemList.Add(itemTemp);
        }*/

        for (int i = stageIndex; i < stage.Length; i++)
        {
            // /를 만나면 종료
            if (stage[i].Equals("/"))
            {
                break;
            }

            itemTemp = new Item();
            int position = i;

            itemTemp.Name = stage[i];

            itemTemp.OnItemClick = new Button.ButtonClickedEvent();
            itemTemp.OnItemClick.AddListener(() => ItemClick_Add(position));

            this.ItemList.Add(itemTemp);
        }
    }
    //사용할 버튼 바인딩
    private void Binding()
    {
        GameObject btnItemTemp;
        ItemObject itemobjectTemp;

        foreach (Item item in this.ItemList)
        {
            btnItemTemp = Instantiate(this.ItemObject) as GameObject;
            itemobjectTemp = btnItemTemp.GetComponent<ItemObject>();

            btnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("fx") as Sprite;
            itemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(70, 70);
            itemobjectTemp.Condition.text = null;
            
            //만약 변수면
            for (int i = 0; i < var.Length; i++)
            {
                if (item.Name.Equals(var[i]))
                {
                    btnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("var") as Sprite;
                    break;
                }
            }

            //만약 extra 면
            for (int i = 0; i < extraChar.Length; i++)
            {
                if (item.Name.Equals(extraChar[i]))
                {
                    btnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("sign") as Sprite;
                    break;
                }
            }

            itemobjectTemp.Name.text = item.Name;
            itemobjectTemp.Item.onClick = item.OnItemClick;

            btnItemTemp.transform.SetParent(this.Content);
            btnItemTemp.transform.localScale = Vector3.one;

        }
    }

    void Start()
    {
        //패널 비활성화
        panel = GameObject.Find("ConPanel");
        panel.SetActive(false);
        upDownPanel = GameObject.Find("UpDownPanel");
        upDownPanel.SetActive(false);
        okCancelPanel = GameObject.Find("OkCancelPanel");
        okCancelPanel.SetActive(false);
        ButtonAdd = GameObject.Find("Button_FlagAdd");
        CurText.GetComponent<Text>().text = DataControl.CurMoney.ToString();

        unUseScrollView = GameObject.Find("UnUseScrollView");
        useScrollView = GameObject.Find("UseScrollView");

        AddListItem();
        this.Binding();
    }

    //사용된 버튼 리스트
    //리스트에 넣음과 동시에 버튼에 바인딩
    private void AddUseListItem(string name)
    {
        GameObject usebtnItemTemp;
        ItemObject useitemobjectTemp;
        Item itemTemp;

        int Index = useIndex;

        //보여지는 버튼에 대한 속성들
        itemTemp = new Item();
        itemTemp.Name = name;
        itemTemp.Index = Index;
        itemTemp.fxIndex = fxIndex;

        for (int i = 0; i < var.Length; i++)
        {
            if (name.Equals("\"\""))
            {
                itemTemp.condition = "";
                break;
            }
        }

        //게임 오브젝트로 붙이는 부분
        usebtnItemTemp = Instantiate(this.ItemObject) as GameObject;

        useitemobjectTemp = usebtnItemTemp.GetComponent<ItemObject>();

        useitemobjectTemp.Name.text = itemTemp.Name;
        useitemobjectTemp.Item.onClick = new Button.ButtonClickedEvent();
        useitemobjectTemp.Item.onClick.AddListener(() => usedButtonClick(Index));

        //순서 알기 쉽게 디버깅용 하이어라키쪽 ★
        usebtnItemTemp.name = itemTemp.Name + "_" + Index + " " + fxIndex;
        usebtnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;
        useitemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(194, 70);
        useitemobjectTemp.Condition.text = null;
        usebtnItemTemp.tag = "fx";

        //만약 변수라면
        for (int i = 0; i < var.Length; i++)
        {
            if (name.Equals(var[i]))
            {
                usebtnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("var") as Sprite;
                useitemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(70, 70);
                useitemobjectTemp.Item.onClick = new Button.ButtonClickedEvent();
                useitemobjectTemp.Item.onClick.AddListener(() => condition(Index));
                usebtnItemTemp.tag = "var";
                break;
            }
        }

        //만약 extra라면
        for (int i = 0; i < extraChar.Length; i++)
        {
            if (name.Equals(extraChar[i]))
            {
                usebtnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("sign") as Sprite;
                useitemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(70, 70);
                useitemobjectTemp.Item.onClick = new Button.ButtonClickedEvent();
                useitemobjectTemp.Item.onClick.AddListener(() => extra(Index));
                usebtnItemTemp.tag = "extra";
                break;
            }
        }

        //게임 오브젝트
        itemTemp.game = usebtnItemTemp;

        //추가모드
        if (flagAdd)
        {
            //전체 모드
            if (flagAll)
            {
                UsedItemList.Add(itemTemp);
                for (int i = 0; i < UsedItemList.Count; i++)
                {
                    if (UsedItemList[i] != null)
                    {
                        UsedItemList[i].game.transform.SetParent(this.UseContent);
                        UsedItemList[i].game.transform.localScale = Vector3.one;
                    }
                }
            }
            //함수 모드
            else
            {
                this.UsedItemList.Insert(fxstr, itemTemp);

                for (int i = 0; i < UsedItemList.Count; i++)
                {
                    if (UsedItemList[i] != null)
                    {

                        UsedItemList[i].game.transform.SetParent(this.UseContent);
                        UsedItemList[i].game.transform.SetSiblingIndex(i);
                        UsedItemList[i].game.transform.localScale = Vector3.one;
                    }
                }
            }
        }
        fxstr++;
        useIndex++;

        if(name.Contains("끝"))
        {
            fxIndex++;
        }
    }

    //함수 누르면 나오는 동작
    public void usedButtonClick(int Index)
    {
        //추가 모드
        if (flagAdd)
        {
            //전체 모드
            if (flagAll)
            {
                for (int i = 0; i < UsedItemList.Count; i++)
                {
                    if (UsedItemList[i] != null)
                    {
                        if (UsedItemList[i].Index == Index)
                        {
                            for (int j = 0; j < fx.Length; j++)
                            {
                                //함수시작이면
                                if (UsedItemList[i].Name.Equals(fx[j] + "시작"))
                                {
                                    Debug.Log("함수모드로 전환 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                    fxstr = UsedItemList.IndexOf(UsedItemList[i]) + 1;
                                    FlagInnerChange();
                                    //여기 네임 규칙이 ★ 표시해놓은 곳이랑 같아야함
                                    buttonHighlightStart = GameObject.Find(UsedItemList[i].Name + "_" + UsedItemList[i].Index + " " + UsedItemList[i].fxIndex);
                                    buttonHighlightStart.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;

                                    int endIndex = Convert.ToInt32(UsedItemList[i].Index)+1;
                                    if (fx[j].Equals("if") || fx[j].Equals("elif"))
                                    {
                                        buttonHighlightCondition = GameObject.Find("조건_" + endIndex + " " + UsedItemList[i].fxIndex);
                                        buttonHighlightCondition.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;
                                        endIndex++;
                                    }
                                    buttonHighlightEnd = GameObject.Find(fx[j]+ "끝" + "_" + endIndex + " " + UsedItemList[i].fxIndex);
                                    buttonHighlightEnd.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;

                                    nowFx = UsedItemList[i].fxIndex;
                                    break;
                                }
                                //함수 끝이면
                                else if (UsedItemList[i].Name.Equals(fx[j] + "끝"))
                                {
                                    Debug.Log("함수모드로 전환 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                    fxstr = UsedItemList.IndexOf(UsedItemList[i]);
                                    FlagInnerChange();
                                    buttonHighlightStart = GameObject.Find(UsedItemList[i].Name + "_" + UsedItemList[i].Index + " " + UsedItemList[i].fxIndex);
                                    buttonHighlightStart.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;

                                    int endIndex = Convert.ToInt32(UsedItemList[i].Index) - 1;
                                    if (fx[j].Equals("if") || fx[j].Equals("elif"))
                                    {
                                        buttonHighlightCondition = GameObject.Find("조건_" + endIndex + " " + UsedItemList[i].fxIndex);
                                        buttonHighlightCondition.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;
                                        endIndex--;
                                    }
                                    buttonHighlightEnd = GameObject.Find(fx[j] + "시작" + "_" + endIndex + " " + UsedItemList[i].fxIndex);
                                    buttonHighlightEnd.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;

                                    nowFx = UsedItemList[i].fxIndex;
                                    break;
                                }
                                else if (UsedItemList[i].Name.Equals("조건"))
                                {
                                    Debug.Log("함수모드로 전환 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                    fxstr = UsedItemList.IndexOf(UsedItemList[i]);
                                    FlagInnerChange();

                                    Debug.Log("if시작" + "_" + (UsedItemList[i].Index - 1) + " " + UsedItemList[i].fxIndex);
                                    buttonHighlightStart = GameObject.Find("if시작" + "_" + (UsedItemList[i].Index - 1) + " " + UsedItemList[i].fxIndex);
                                    if (buttonHighlightStart != null)
                                    {
                                        buttonHighlightStart.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;
                                    }
                                    else
                                    {
                                        buttonHighlightStart = GameObject.Find("elif시작" + "_" + (UsedItemList[i].Index - 1) + " " + UsedItemList[i].fxIndex);
                                        buttonHighlightStart.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;
                                    }

                                    buttonHighlightCondition = GameObject.Find("조건_" + UsedItemList[i].Index + " " + UsedItemList[i].fxIndex);
                                    buttonHighlightCondition.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;

                                    buttonHighlightEnd = GameObject.Find("if끝" + "_" + (UsedItemList[i].Index + 1) + " " + UsedItemList[i].fxIndex);
                                    if (buttonHighlightEnd != null)
                                    {
                                        buttonHighlightEnd.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;
                                    }
                                    else
                                    {
                                        buttonHighlightEnd = GameObject.Find("elif끝" + "_" + (UsedItemList[i].Index + 1) + " " + UsedItemList[i].fxIndex);
                                        buttonHighlightEnd.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLongHighlight") as Sprite;
                                    }
                                    upDownPanel.SetActive(true);

                                    nowFx = UsedItemList[i].fxIndex;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            //함수 모드
            else
            {
                for (int i = 0; i < UsedItemList.Count; i++)
                {
                    if (UsedItemList[i] != null)
                    {
                        if (UsedItemList[i].Index == Index)
                        {
                            for (int j = 0; j < fx.Length; j++)
                            {
                                //함수시작이면
                                if (UsedItemList[i].Name.Equals(fx[j] + "시작"))
                                {
                                    //같은 함수면
                                    if (nowFx == UsedItemList[i].fxIndex)
                                    {
                                        Debug.Log("전체모드로 전환 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                        fxstr = UsedItemList.IndexOf(UsedItemList[i]) + 1;
                                        FlagInnerChange();
                                        buttonHighlightStart = GameObject.Find(UsedItemList[i].Name + "_" + UsedItemList[i].Index + " " + UsedItemList[i].fxIndex);
                                        buttonHighlightStart.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;
                                        int endIndex = Convert.ToInt32(UsedItemList[i].Index) + 1;
                                        if (fx[j].Equals("if") || fx[j].Equals("elif"))
                                        {
                                            buttonHighlightCondition = GameObject.Find("조건_" + endIndex + " " + UsedItemList[i].fxIndex);
                                            buttonHighlightCondition.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;
                                            endIndex++;
                                        }
                                        buttonHighlightEnd = GameObject.Find(fx[j] + "끝" + "_" + endIndex + " " + UsedItemList[i].fxIndex);
                                        buttonHighlightEnd.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;
                                    }
                                    //다른 함수면
                                    else
                                    {
                                        GameObject[] fx = GameObject.FindGameObjectsWithTag("fx");
                                        for (int k= 0; k < fx.Length; k++)
                                        {
                                            fx[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;
                                        }
                                        FlagInnerChange();
                                        usedButtonClick(UsedItemList[i].Index);
                                    }
                                    break;
                                }
                                //함수 끝이면
                                else if (UsedItemList[i].Name.Equals(fx[j] + "끝"))
                                {
                                    //같은 함수면
                                    if (nowFx == UsedItemList[i].fxIndex)
                                    {
                                        Debug.Log("전체모드로 전환 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                        fxstr = UsedItemList.IndexOf(UsedItemList[i]);
                                        FlagInnerChange();
                                        buttonHighlightStart = GameObject.Find(UsedItemList[i].Name + "_" + UsedItemList[i].Index + " " + UsedItemList[i].fxIndex);
                                        buttonHighlightStart.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;

                                        int endIndex = Convert.ToInt32(UsedItemList[i].Index) - 1;
                                        if (fx[j].Equals("if") || fx[j].Equals("elif"))
                                        {
                                            buttonHighlightCondition = GameObject.Find("조건_" + endIndex + " " + UsedItemList[i].fxIndex);
                                            buttonHighlightCondition.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;
                                            endIndex--;
                                        }
                                        buttonHighlightEnd = GameObject.Find(fx[j] + "시작" + "_" + endIndex + " " + UsedItemList[i].fxIndex);
                                        buttonHighlightEnd.GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;
                                    }
                                    //다른 함수면
                                    else
                                    {
                                        GameObject[] fx = GameObject.FindGameObjectsWithTag("fx");
                                        for (int k = 0; k < fx.Length; k++)
                                        {
                                            fx[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;
                                        }
                                        FlagInnerChange();
                                        usedButtonClick(UsedItemList[i].Index);
                                    }
                                    break;
                                }
                                else if (UsedItemList[i].Name.Equals("조건"))
                                {
                                    //같은 함수면
                                    if (nowFx == UsedItemList[i].fxIndex)
                                    {
                                        Debug.Log("함수모드로 전환 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                        fxstr = UsedItemList.IndexOf(UsedItemList[i]);
                                        upDownPanel.SetActive(true);
                                    }
                                    //다른 함수면
                                    else
                                    {
                                        GameObject[] fx = GameObject.FindGameObjectsWithTag("fx");
                                        for (int k = 0; k < fx.Length; k++)
                                        {
                                            fx[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;
                                        }
                                        FlagInnerChange();
                                        usedButtonClick(UsedItemList[i].Index);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        //제거 모드
        else
        {
            //전체 모드
            if (flagAll)
            {
                for (int i = 0; i < UsedItemList.Count; i++)
                {
                    if (UsedItemList[i] != null)
                    {
                        if (UsedItemList[i].Index == Index)
                        {
                            //var 외에 제거 이루어지는 부분
                            if (!UsedItemList[i].Name.Contains("끝") && !UsedItemList[i].Name.Contains("조건"))
                            {
                                int fxstart = UsedItemList.IndexOf(UsedItemList[i]);
                                int setfxIndex = UsedItemList[i].fxIndex;
                                int fxend = 0;

                                //함수끝의 인덱스를 찾기 위한 검사
                                for (int k = fxstart; k < UsedItemList.Count; k++)
                                {
                                    if (UsedItemList[k] != null)
                                    {
                                        //함수 끝이면서 fxIndex로 짝이 맞을 때
                                        if (UsedItemList[k].game.name.Contains("끝") && UsedItemList[k].fxIndex == setfxIndex)
                                        {
                                            fxend = UsedItemList.IndexOf(UsedItemList[k]);
                                            break;
                                        }
                                    }
                                }
                                //함수 시작부터 끝까지 통으로 삭제
                                for (int k = fxstart; k <= fxend; k++)
                                {
                                    Destroy(UsedItemList[k].game);
                                }

                                UsedItemList.RemoveRange(fxstart, fxend - fxstart + 1);

                                break;
                            }

                        }
                    }
                }
            }
        }

    }

    //사용할 버튼 누르면 사용된 버튼 리스트에 추가됨
    public void ItemClick_Add(int position)
    {
        //전체 편집 모드
        if (flagAll)
        {
            //추가 모드
            if (flagAdd)
            {
                for (int i = 0; i < fx.Length; i++)
                {
                    //함수면
                    if (stage[position].Equals(fx[i]))
                    {
                        AddUseListItem(stage[position] + "시작");
                        if (stage[position].Equals("if") || stage[position].Equals("elif"))
                            AddUseListItem("조건");
                        AddUseListItem(stage[position] + "끝");
                        break;
                    }
                }
                for (int i = 0; i < var.Length; i++)
                {
                    //변수면
                    if (stage[position].Equals(var[i]))
                    {
                        AddUseListItem(stage[position]);
                        break;
                    }
                }
                for (int i = 0; i < extraChar.Length; i++)
                {
                    if (stage[position].Equals(extraChar[i]))
                    {
                        AddUseListItem(stage[position]);
                        break;
                    }
                }
            }
        }
        //함수 내부 편집 모드일 때 함수 시작 다음 부분부터 넣어줘야함
        else
        {
            if (flagAdd)
            {
                for (int i = 0; i < fx.Length; i++)
                {
                    //함수면
                    if (stage[position].Equals(fx[i]))
                    {
                        AddUseListItem(stage[position] + "시작");
                        if (stage[position].Equals("if") || stage[position].Equals("elif"))
                            AddUseListItem("조건");
                        AddUseListItem(stage[position] + "끝");
                        break;
                    }
                }
                for (int i = 0; i < var.Length; i++)
                {
                    //변수면
                    if (stage[position].Equals(var[i]))
                    {
                        AddUseListItem(stage[position]);
                        break;
                    }
                }
                for (int i = 0; i < extraChar.Length; i++)
                {
                    if (stage[position].Equals(extraChar[i]))
                    {
                        AddUseListItem(stage[position]);
                        break;
                    }
                }
            }
        }

    }

    //추가,제거 모드 전환
    public void FlagAddChange()
    {
        //삭제모드
        if (flagAdd)
        {
            flagAdd = false;
            ButtonAdd.GetComponent<Image>().sprite = Resources.Load<Sprite>("modeDelete") as Sprite;
            useScrollView.GetComponent<Image>().sprite = Resources.Load<Sprite>("deleteModeOn") as Sprite;
            unUseScrollView.GetComponent<Image>().sprite = Resources.Load<Sprite>("addModeOff") as Sprite;

            //함수 모드일 경우에 빠져나오고 하이라이트 빼준다.
            if (!flagAll)
            {
                flagAll = true;
                GameObject[] fx = GameObject.FindGameObjectsWithTag("fx");
                for(int i=0;i<fx.Length;i++)
                {
                    fx[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("fxLong") as Sprite;
                }
            }

        }
        //추가모드
        else
        {
            flagAdd = true;
            ButtonAdd.GetComponent<Image>().sprite = Resources.Load<Sprite>("modeAdd") as Sprite;
            useScrollView.GetComponent<Image>().sprite = Resources.Load<Sprite>("deleteModeOff") as Sprite;
            unUseScrollView.GetComponent<Image>().sprite = Resources.Load<Sprite>("addModeOn") as Sprite;
        }
    }

    //함수 내부모드 전환
    public void FlagInnerChange()
    {
        if (flagAll)
        {
            flagAll = false;
        }
        else
        {
            flagAll = true;

        }
    }

    //조건 입력 창 열어줌
    public void condition(int Index)
    {
        //ConSubmit 함수와 함께 쓰는 변수
        conIndex = Index;
        //추가모드면
        if (flagAdd)
        {
            for (int i = 0; i < UsedItemList.Count; i++)
            {
                if (UsedItemList[i] != null)
                {
                    if (UsedItemList[i].Index == conIndex)
                    {
                        ConInput.GetComponent<InputField>().text = UsedItemList[i].condition;
                        break;
                    }
                }
            }
            panel.SetActive(true);
        }
        //var 제거가 이루어지는 부분
        //제거모드면
        else
        {
            for (int i = 0; i < UsedItemList.Count; i++)
            {
                if (UsedItemList[i] != null)
                {
                    if (UsedItemList[i].Index == conIndex)
                    {
                        Destroy(UsedItemList[i].game);
                        UsedItemList.RemoveAt(UsedItemList.IndexOf(UsedItemList[i]));
                        break;
                    }
                }
            }
        }
    }

    //extra 제거 함수
    public void extra(int Index)
    {
        int extraIndex = Index;

        if (!flagAdd)
        {
            for (int i = 0; i < UsedItemList.Count; i++)
            {
                if (UsedItemList[i] != null)
                {
                    if (UsedItemList[i].Index == extraIndex)
                    {
                        Destroy(UsedItemList[i].game);
                        UsedItemList.RemoveAt(UsedItemList.IndexOf(UsedItemList[i]));
                        break;
                    }
                }
            }
        }
    }
    //조건 입력한 것 저장하고 닫아줌
    //버튼에 직접 달아놨음
    public void ConSubmit()
    {
        string input = ConInput.GetComponent<InputField>().text;

        for (int i = 0; i < UsedItemList.Count; i++)
        {
            if (UsedItemList[i] != null)
            {
                if (UsedItemList[i].Index == conIndex)
                {
                    //만약 변경값을 뿌려주는 ""형 변수면
                    if (UsedItemList[i].Name.Equals("\"\""))
                    {
                        //인풋을 컨디션에 저장
                        UsedItemList[i].condition = input;
                        //컴포넌트 가져옴
                        ItemObject itemobjectTemp = UsedItemList[i].game.GetComponent<ItemObject>();
                        //인풋을 뿌려줌
                        itemobjectTemp.Name.text = "\"" + input + "\"";

                        //인풋이 5자 이상일경우 줄임말로 표현해줌
                        if (input.Length > 6)
                        {
                            itemobjectTemp.Name.text = "\"" + input.Substring(0, 6) + "...";

                        }
                        break;
                    }
                    //만약 서브젝트처럼 변수명은 있고 속만 바뀐다면?
                    else if (UsedItemList[i].Name.Equals("Subject") || UsedItemList[i].Name.Equals("X") || UsedItemList[i].Name.Equals("Y"))
                    {
                        //인풋을 컨디션에 저장만해줌 뿌려주진 않음
                        UsedItemList[i].condition = input;
                        break;
                    }
                    //그 외
                    else
                    {
                        UsedItemList[i].condition = input;
                        //컴포넌트 가져옴
                        ItemObject itemobjectTemp = UsedItemList[i].game.GetComponent<ItemObject>();
                        //인풋을 뿌려줌
                        itemobjectTemp.Condition.text = input;

                        //인풋이 4자 이상일경우 줄임말로 표현해줌
                        if (input.Length > 6)
                        {
                            itemobjectTemp.Condition.text = input.Substring(0, 6) + "...";
                        }
                        break;
                    }

                }
            }
        }
        panel.SetActive(false);
    }

    //조건창 그냥 닫아줌 <취소>
    //버튼에 직접 달아놨음
    public void ConCancel()
    {
        panel.SetActive(false);
    }

    public void conUp()
    {
        upDownPanel.SetActive(false);
    }

    public void conDown()
    {
        fxstr += 1;
        upDownPanel.SetActive(false);
    }

    public void ChangeSceneBeforeCoding()
    {
        okCancelPanel.SetActive(true);
        okCancelText.text = "현재 사용된 블록들이 사라집니다.\n시나리오를 다시 보시겠습니까?";
        panelFlag = 4;
    }

    public void ChangeSceneHome()
    {
        okCancelPanel.SetActive(true);
        okCancelText.text = "현재 사용된 블록들이 사라집니다.\n귀가 하시겠습니까?";
        panelFlag = 3;
    }

    public void ChangeSceneResult()
    {
        okCancelPanel.SetActive(true);
        okCancelText.text = "현재 사용된 블록으로 채점합니다.\n제출 하시겠습니까?";
        panelFlag = 2;
    }

    public void SaveCode()
    {
        okCancelPanel.SetActive(true);
        okCancelText.text = "먼저 저장된 블록에 덮어씌워집니다.\n현재 블록을 저장하시겠습니까?";
        panelFlag = 1;
    }
    
    public void LoadCode()
    {
        okCancelPanel.SetActive(true);
        okCancelText.text = "사용했던 블록이 초기화 됩니다.\n저장된 블록을 불러오시겠습니까?";
        panelFlag = 0;
    }
    
    public void ButtonOfPanelOk()
    {
        //Load
        if (panelFlag == 0)
        {
            okCancelPanel.SetActive(false);
            for (int i = 0; i < UsedItemList.Count; i++)
            {
                Destroy(UsedItemList[i].game);
            }

            UsedItemList.Clear();
            useIndex = 0;
            fxstr = 0;

            int UsedCount = PlayerPrefs.GetInt("UsedCount", 0);
            ItemCopy cp;

            for (int i = 0; i < UsedCount; i++)
            {
                var data = PlayerPrefs.GetString("Code" + UnUseBlockMain.stageTitleIndex + "_" + i);

                if (!string.IsNullOrEmpty(data))
                {
                    var binaryFormatter = new BinaryFormatter();
                    var memoryStream = new MemoryStream(Convert.FromBase64String(data));

                    // 가져온 데이터를 바이트 배열로 변환하고
                    // 사용하기 위해 다시 리스트로 캐스팅해줍니다.
                    cp = (ItemCopy)binaryFormatter.Deserialize(memoryStream);
                    AddUseListItem(cp.Name);
                }
            }
        }
        //Save
        else if (panelFlag == 1)
        {
            okCancelPanel.SetActive(false);
            for (int i = 0; i < UsedItemList.Count; i++)
            {
                var binaryFormatter = new BinaryFormatter();
                var memoryStream = new MemoryStream();
                ItemCopy itemCopy = new ItemCopy();

                itemCopy.Name = UsedItemList[i].Name;
                itemCopy.Index = UsedItemList[i].Index;
                itemCopy.fxIndex = UsedItemList[i].fxIndex;
                itemCopy.condition = UsedItemList[i].condition;
                itemCopy.OnItemClick = UsedItemList[i].OnItemClick;

                binaryFormatter.Serialize(memoryStream, itemCopy);

                PlayerPrefs.SetString("Code" + UnUseBlockMain.stageTitleIndex + "_" + i, Convert.ToBase64String(memoryStream.GetBuffer()));

            }
            PlayerPrefs.SetInt("UsedCount", UsedItemList.Count);
            PlayerPrefs.Save();
        }
        //Submit
        else if(panelFlag == 2)
        {
            okCancelPanel.SetActive(false);
            CheckAns.ans.Clear();

            for (int i = 0; i < UsedItemList.Count; i++)
            {
                if (UsedItemList[i] != null)
                {
                    CheckAns.ans.Add(UsedItemList[i]);
                }
            }
            SceneManager.LoadScene("ResultScene");
        }
        //GoHome
        else if(panelFlag == 3)
        {
            okCancelPanel.SetActive(false);
            SceneManager.LoadScene("HomeScene");
        }
        //Review
        else if (panelFlag == 4)
        {
            okCancelPanel.SetActive(false);
            SceneManager.LoadScene("BeforeCodingScene");
        }
    }
    public void ButtonOfPanelCancel()
    {
        okCancelPanel.SetActive(false);
    }
}
