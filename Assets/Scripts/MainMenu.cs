using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private const float CAMERA_TRANSITION_SPEED = 3.0f;

    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;

    public GameObject shopButtonPrefab;
    public GameObject shopButtonContainer;

    public Text currencyText;

    public Material playerMaterial;

    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;

    private void Start()
    {
        //For mac users
        //PlayerPrefs.DeleteAll();
        //ChangePlayerSkin(14);
        ChangePlayerSkin(GameManager.Instance.currentSkinIndex);
        //Display currencyText
        currencyText.text = "Currency : " + GameManager.Instance.currency.ToString();
        cameraTransform = Camera.main.transform;

        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        foreach (Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(levelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelButtonContainer.transform, false);

            string sceneName = thumbnail.name;
            container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
        }

        int textureIndex = 0;
        Sprite[] textures = Resources.LoadAll<Sprite>("Player");
        foreach (Sprite texture in textures)
        {
            GameObject container = Instantiate(shopButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(shopButtonContainer.transform, false);

            int index = textureIndex;
            container.GetComponent<Button>().onClick.AddListener(() => ChangePlayerSkin(index));
            //Return the transform of our overlay
            if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
            {
                container.transform.GetChild(0).gameObject.SetActive(false);
            }
            textureIndex++;
        }
    }

    private void Update()
    {
        if (cameraDesiredLookAt != null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, CAMERA_TRANSITION_SPEED * Time.deltaTime);
        }
    }

    private void LoadLevel(string sceneName)
    {
        Debug.Log(sceneName);

        SceneManager.LoadScene(sceneName);

    }

    public void LookAtMenu(Transform menuTransform)
    {
        cameraDesiredLookAt = menuTransform;
    }

    private void ChangePlayerSkin(int index)
    {
        //Check the index bit number
        if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
        {
            //Slice the texture into 4
            float x = ((int)index % 4) * 0.25f;
            float y = ((int)index / 4) * 0.25f;

            //if (y == 0.0f)
            //    y = 0.75f;
            //else if (y == 0.25f)
            //    y = 0.5f;
            //else if (y == 0.50f)
            //    y = 0.25f;
            //else if (y == 0.75f)
            //    y = 0f;

            playerMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));
            //Change currentSkinIndex
            GameManager.Instance.currentSkinIndex = index;
            //Save currentSkinIndex
            GameManager.Instance.Save();
        }
        else
        {
            // You do not have the skin, do you want to buy it?
            int cost = 100;

            if (GameManager.Instance.currency >= cost)
            {
                GameManager.Instance.currency -= cost;
                GameManager.Instance.skinAvailability += 1 << index;
                GameManager.Instance.Save();
                currencyText.text = "Currency : " + GameManager.Instance.currency.ToString();

                //The shopItemContainer disapear
                //GetChild(0) means overlay
                shopButtonContainer.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                //This will re-run ChangePlayerSkin to update color in shop menu
                ChangePlayerSkin(index);
            }
        }
    }
}
