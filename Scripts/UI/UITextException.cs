using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextException : MonoBehaviour
{
    [SerializeField] private Button _okButton;
    [SerializeField] private TextMeshProUGUI _guidText;
    
    void Start()
    {
        _okButton.onClick.AddListener(CloseTextException);
    }
    
    public void CloseTextException()
    {
        GameManager.instance.IsButtonClick = true;
        gameObject.SetActive(false);
    }
    
    public void ChangeNullValueText()
    {
        _guidText.text = "값을 모두 입력하세요.";
    }
    
    public void ChangePlayerNameValueText()
    {
        _guidText.text = "이름은 최대 6글자 이하만 가능합니다.";
    }
    
    public void ChangeFarmNameValueText()
    {
        _guidText.text = "농장 이름은 최대 6글자 이하만 가능합니다.";
    }
    
    public void ChangeFavoriteValueText()
    {
         _guidText.text = "좋아하는 것은 최대 6글자 이하만 가능합니다.";
    }
    
    public void ChangeFullSlotText()
    {
        _guidText.text = "저장 파일이 가득 찼습니다.";
    }
}

