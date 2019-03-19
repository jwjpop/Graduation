using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnUseBlockMain : MonoBehaviour
{
    public GameObject ItemObject;
    private GameObject panel;

    public Transform Content;
    public Transform UseContent;

    public List<Item> ItemList;
    public List<Item> UsedItemList;

    public Text flagAddText;
    public Text flagAllText;
    public InputField ConInput;

    private bool flagAdd = true;
    private bool flagAll = true;

    private int useIndex = 0;
    private int fxIndex = 0;
    private int fxstr = 0;
    private int conIndex = 0;
    //스테이지 시작 인덱스를 기억 static 뿐만아니라 게임이 실행되는 내내 보관되어야하는 형태면 가능
    private static int stageIndex = 0;

    //스테이지마다 사용될 버튼들 배열 만들기
    private string[] stage = {"print","\"\"","print","Subject","print","X","Y","+",",","if"};
    //함수 배열
    private string[] fx = {"print","if"};
    //변수 배열
    private string[] var = {"\"\"","Subject","X","Y"};
    //부가적인 문자
    private string[] extraChar = {"+",","};
    //사용할 버튼 리스트
    private void AddListItem()
    {
        Item itemTemp;
        
        for(int i= stageIndex; i<stage.Length;i++)
        {
          /*  //1탄의 끝 만나면 더이상 추가x
            if (stage[i].Equals("/"))
            {
                stageIndex = i+1;
                break;
            }*/

            itemTemp = new Item();
            int position = i;
            
            itemTemp.Name = stage[i];
            
            itemTemp.OnItemClick = new Button.ButtonClickedEvent();
            itemTemp.OnItemClick.AddListener(()=>ItemClick_Add(position));

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

            for (int i = 0; i < fx.Length; i++)
            {
                //만약 프린트면 (조건없이 내부 채우는 변수들)
                if(item.Name.Equals("print"))
                {
                    btnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("conditionend") as Sprite;
                    itemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(100, 20);
                    itemobjectTemp.Condition.text = null;
                    break;
                }
                //만약 if같은 조건있는 변수라면
                else if (item.Name.Equals(fx[i]))
                {
                    btnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("condition") as Sprite;
                    break;
                }
            }
            for (int i = 0; i < var.Length; i++)
            {
                //만약 변수라면
                if (item.Name.Equals(var[i]))
                {
                    btnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("variable") as Sprite;
                    itemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(100, 20);
                    itemobjectTemp.Condition.text = null;
                    break;
                }
            }

            for(int i = 0; i < extraChar.Length;i++)
            {
                if(item.Name.Equals(extraChar[i]))
                {
                     btnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("sign") as Sprite;
                    itemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(100, 20);
                    itemobjectTemp.Condition.text = null;
                    break;
                }
            }
            
            itemobjectTemp.Name.text = item.Name;
            itemobjectTemp.Item.onClick = item.OnItemClick;
            itemobjectTemp.btnCon.onClick = item.OnItemClick;
            
            btnItemTemp.transform.SetParent(this.Content);
            btnItemTemp.transform.localScale = Vector3.one;

        }
    }

    void Start()
    {
        //패널 비활성화
        panel = GameObject.Find("ConPanel");
        panel.SetActive(false);

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

        for (int i = 0; i < fx.Length; i++)
        {
            if (name.Contains(fx[i]))
            {
                itemTemp.fxIndex = fxIndex;
                itemTemp.condition = "조건을 입력하세요";
                break;
            }
        }
        for (int i = 0; i < var.Length; i++)
        {
            if (name.Equals("\"\""))
            {
               itemTemp.condition = "변수를 입력하세요";
                break;
            }
        }

        //게임 오브젝트로 붙이는 부분
        usebtnItemTemp = Instantiate(this.ItemObject) as GameObject;
        
        useitemobjectTemp = usebtnItemTemp.GetComponent<ItemObject>();

        useitemobjectTemp.Name.text = itemTemp.Name;
        useitemobjectTemp.Item.onClick = new Button.ButtonClickedEvent();
        useitemobjectTemp.Item.onClick.AddListener(() => usedButtonClick(Index));
        
        //순서 알기 쉽게 디버깅용 하이어라키쪽
        usebtnItemTemp.name = itemTemp.Name + "_" + Index;

        for (int i = 0; i < fx.Length; i++)
        {
            //만약 프린트같은 조건 없는 함수라면
            if(name.Equals("print"+"시작"))
            {
                usebtnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("conditionend") as Sprite;
                useitemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(100, 20);
                useitemobjectTemp.Condition.text = null;
                break;
            }
            //만약 if같은 조건있는 함수라면
            else if (name.Equals(fx[i]+"시작"))
            {
                usebtnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("condition") as Sprite;
                useitemobjectTemp.btnCon.onClick = new Button.ButtonClickedEvent();
                useitemobjectTemp.btnCon.onClick.AddListener(() => condition(Index));
                break;
            }
            //함수의 끝이라면
            else if (name.Equals(fx[i]+"끝"))
            {
                usebtnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("conditionend") as Sprite;
                useitemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(100, 20);
                useitemobjectTemp.Condition.text = null;
                break;
            }
        }

        for (int i = 0; i < var.Length; i++)
        {
            //만약 변수라면
            if(name.Equals(var[i]))
            {
                usebtnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("variable") as Sprite;
                useitemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(100, 20);
                useitemobjectTemp.Condition.text = null;

                //텍스트 필드로써 값으로 변경되어야하는 것
                if(name.Equals("\"\""))
                {
                    useitemobjectTemp.Item.onClick = new Button.ButtonClickedEvent();
                    useitemobjectTemp.Item.onClick.AddListener(() => condition(Index));
                }
                //변수명으로써 이름이 값으로 변경되어서는 안되는 것
                if(name.Equals("Subject") || name.Equals("X") || name.Equals("Y"))
                {
                    useitemobjectTemp.Item.onClick = new Button.ButtonClickedEvent();
                    useitemobjectTemp.Item.onClick.AddListener(() => condition(Index));
                }
                break;
            }
        }

        for(int i = 0; i< extraChar.Length; i++)
        {
            if(name.Equals(extraChar[i]))
            {
                usebtnItemTemp.GetComponent<Image>().sprite = Resources.Load<Sprite>("sign") as Sprite;
                useitemobjectTemp.Item.image.rectTransform.sizeDelta = new Vector2(100, 20);
                useitemobjectTemp.Condition.text = null;
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
                this.UsedItemList.Insert(fxstr,itemTemp);

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
    }
    
    //사용된 버튼 누르면 나오는 동작
    public void usedButtonClick(int Index)
    {
        //추가 모드
        if (flagAdd)
        {
            //전체 모드
            if(flagAll)
            {
                for(int i = 0; i<UsedItemList.Count; i++)
                {
                    if (UsedItemList[i] != null)
                    {
                        if (UsedItemList[i].Index == Index)
                        {
                            for(int j=0;j<var.Length;j++)
                            {
                                //변수를 누르면
                                if (UsedItemList[i].Name.Equals(var[j]))
                                {
                                    Debug.Log("추가모드 + 전체모드 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                    break;
                                }
                            }
                            for(int j=0;j<fx.Length;j++)
                            {
                                //함수시작이면
                                if (UsedItemList[i].Name.Equals(fx[j]+"시작"))
                                {
                                    Debug.Log("함수모드로 전환 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                    fxstr = UsedItemList.IndexOf(UsedItemList[i]) + 1;
                                    FlagInnerChange();
                                    break;
                                }
                                //함수 끝이면
                                else if (UsedItemList[i].Name.Equals(fx[j] + "끝"))
                                {
                                    Debug.Log("함수모드로 전환 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                    fxstr = UsedItemList.IndexOf(UsedItemList[i]);
                                    FlagInnerChange();
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
                            for (int j = 0; j < var.Length; j++)
                            {
                                //변수를 누르면
                                if (UsedItemList[i].Name.Equals(var[j]))
                                {
                                    Debug.Log("추가모드 + 함수모드 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                    break;
                                }
                            }
                            for (int j = 0; j < fx.Length; j++)
                            {
                                //함수시작이면
                                if (UsedItemList[i].Name.Equals(fx[j] + "시작"))
                                {
                                    Debug.Log("전체모드로 전환 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                    fxstr = UsedItemList.IndexOf(UsedItemList[i]) + 1;
                                    FlagInnerChange();
                                    break;
                                }
                                //함수 끝이면
                                else if (UsedItemList[i].Name.Equals(fx[j] + "끝"))
                                {
                                    Debug.Log("전체모드로 전환 " + UsedItemList[i].Name + "의 인덱스 : " + UsedItemList.IndexOf(UsedItemList[i]));
                                    fxstr = UsedItemList.IndexOf(UsedItemList[i]);
                                    FlagInnerChange();
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
                            //변수인지 판단
                            for(int j=0;j<var.Length;j++)
                            {
                                //변수면 그것만 제거
                                if (UsedItemList[i].Name.Equals(var[j]))
                                {
                                    Destroy(UsedItemList[i].game);
                                    UsedItemList.RemoveAt(UsedItemList.IndexOf(UsedItemList[i]));
                                    break;
                                }
                                //함수면 구역을 제거
                                else if(!UsedItemList[i].Name.Contains("끝"))
                                {
                                    int fxstart = UsedItemList.IndexOf(UsedItemList[i]);
                                    int fxIndex = UsedItemList[i].fxIndex;
                                    int fxend = 0;

                                    //함수끝의 인덱스를 찾기 위한 검사
                                    for (int k = fxstart; k < UsedItemList.Count; k++)
                                    {
                                        if (UsedItemList[k] != null)
                                        {
                                            //함수 끝이면서 fxIndex로 짝이 맞을 때
                                            if (UsedItemList[k].game.name.Contains("끝") && UsedItemList[k].fxIndex == fxIndex)
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
        
    }

    //사용할 버튼 누르면 사용된 버튼 리스트에 추가됨
    public void ItemClick_Add(int position)
    {
        //전체 편집 모드
        if(flagAll)
        {
            //추가 모드
            if(flagAdd)
            {
                for(int i=0;i<fx.Length;i++)
                {
                    //함수면
                    if(stage[position].Equals(fx[i]))
                    {
                        AddUseListItem(stage[position] + "시작");
                        AddUseListItem(stage[position] + "끝");
                        fxIndex++;
                        break;
                    }
                }
                for(int i=0; i<var.Length;i++)
                {
                    //변수면
                    if(stage[position].Equals(var[i]))
                    {
                        AddUseListItem(stage[position]);
                        break;
                    }
                }
                for(int i = 0; i<extraChar.Length;i++)
                {
                    if(stage[position].Equals(extraChar[i]))
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
                        AddUseListItem(stage[position] + "끝");
                        fxIndex++;
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
                for (int i = 0; i< extraChar.Length; i++)
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
        if (flagAdd)
        {
            flagAdd = false;
            flagAddText.text = "제거";
        }
        else
        {
            flagAdd = true;
            flagAddText.text = "추가";
        }
    }

    //함수 내부모드 전환
    public void FlagInnerChange()
    {
        if (flagAll)
        {
            flagAll = false;
            flagAllText.text = "함수 내부";
        }
        else
        {
            flagAll = true;
            flagAllText.text = "전체 모드";
            
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
                    if(UsedItemList[i].Name.Equals("\"\""))
                    {
                        //인풋을 컨디션에 저장
                        UsedItemList[i].condition = input;
                        //컴포넌트 가져옴
                        ItemObject itemobjectTemp = UsedItemList[i].game.GetComponent<ItemObject>();
                        //인풋을 뿌려줌
                        itemobjectTemp.Name.text = "\"" + input + "\"";

                        //인풋이 5자 이상일경우 줄임말로 표현해줌
                        if (input.Length > 5)
                        {
                            itemobjectTemp.Name.text = "\"" + input.Substring(0, 5) + "...";
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
                        if (input.Length > 4)
                        {
                            itemobjectTemp.Condition.text = input.Substring(0, 4) + "...";
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
}
