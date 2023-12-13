using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private float moveSpeed = 2.03f;

    private void Start()
    {
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        Moves();
        if(Input.GetMouseButtonDown(0))
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x, 18f, _camera.transform.position.z);
        }
    }
    
    private void Moves()
    {
        if(_camera.transform.position.y > 18f)
        {
            moveSpeed = 0;
            OnActiveUI();
        }
        else
        {
            _camera.transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }
    public void OnActiveUI()
    {
        UIManager.instance.GetUI<UISaveFile>();
        UIManager.instance.GetUI<UINewGame>();
    }
}
