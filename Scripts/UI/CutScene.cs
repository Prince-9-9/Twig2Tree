using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CutScene : MonoBehaviour
{
    [SerializeField] GameObject[] images;
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;
    public TextMeshProUGUI text;
    int index;

    void Start()
    {
        index = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
            text.text = (index + 1).ToString();
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && index != 0)
        {
            index--;
            text.text = (index + 1).ToString();
        }
        if (index < images.Length)
        {
            foreach(GameObject image in images)
            {
                image.SetActive(false);
            }
            images[index].SetActive(true);
        }

        if(index == 0)
        {
            leftArrow.SetActive(false);
        }
        else{
            leftArrow.SetActive(true);
        }

        if(index == images.Length - 1)
        {
            rightArrow.SetActive(false);
        }
        else{
            rightArrow.SetActive(true);
        }

        if (index == images.Length)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
