using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadGame);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
