using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetAllLevels : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform parent;

    private int levelsCount;

    // Start is called before the first frame update
    void Start()
    {
        levelsCount = GetLevelsCount();
        CreateLevelButtons();
    }

    private int GetLevelsCount()
    {
        int result = SceneManager.sceneCountInBuildSettings - 1;

        return result;
    }

    private void CreateLevelButtons()
    {
        Text btnText;

        for (int i = 1; i <= levelsCount; i++)
        {
            GameObject btnGameObject = Instantiate(buttonPrefab, parent);
            Button btn = btnGameObject.GetComponent<Button>();


            btnText = btn.GetComponentInChildren<Text>();
            btnText.text = i.ToString();

            btn.onClick.AddListener(() => ChooseLevel(btn));
        }
    }

    private void ChooseLevel(Button button)
    {
        Text buttonText = button.GetComponentInChildren<Text>();
        string s = buttonText.text;

        int levelIndex = int.Parse(s);

        SceneManager.LoadScene(levelIndex);
    }
}
