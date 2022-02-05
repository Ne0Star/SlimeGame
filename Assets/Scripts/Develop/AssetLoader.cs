using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.UI;

public class AssetLoader : MonoBehaviour
{
    [SerializeField]
    private Sprite[] all_sprites;

    private string EditorPath = "Assets/Resources/", NormalPath = "textures/material/";

    public void OnEnable()
    {
        init();
    }
    public void OnDisable()
    {
        finit();
    }
    private void finit()
    {
        all_sprites = null;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
    private void init()
    {
        all_sprites = Resources.LoadAll<Sprite>(NormalPath);
        for (int i = 0; i < all_sprites.Length; i++)
        {
            GameObject s = new GameObject();
            Image img = s.gameObject.AddComponent<Image>() as Image;
            Button btn = s.gameObject.AddComponent<Button>() as Button;
            s.transform.SetParent(gameObject.transform);
            s.name = all_sprites[i].name;
            img.sprite = all_sprites[i];
            btn.onClick.AddListener(() => Click(s));
        }
    }
    private void Click(GameObject who)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject == who.gameObject)
            {
                //FindObjectOfType<Canvas>().GetComponent<DevelopController>().UpdateImage(GetPath(who));
                gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }

    }
    public string GetPath(GameObject imageObject)
    {
        string path = "";
        path = NormalPath + imageObject.name;
        return path;
    }
}
