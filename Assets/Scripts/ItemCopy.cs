using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemCopy
{
    //생성되는 버튼 이름
    public string Name;
    //생성될 때 버튼 고유 인덱스 값 != 실제 배열상의 인덱스 값
    public int Index;
    //함수 번호
    public int fxIndex;
    //조건
    public string condition;
    //게임 오브젝트
    //public GameObject game;
    //클릭이벤트
    public Button.ButtonClickedEvent OnItemClick;
}
