using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomList : MonoBehaviour
{
    public ListItemType itemType;
    public ListLoadType listType;
    public GameObject content;

    private SlimeMaterial[] UploadMaterials;
    private SlimeEffects effectsView;
    private List<GameObject> itemList;

    private void Start()
    {
        UploadMaterials = DevelopManager.Instance?.develop.UploadMaterials;

        TextAsset texts = (TextAsset)Resources.Load<TextAsset>("data/AllEffects");
        effectsView = JsonUtility.FromJson<SlimeEffects>(texts.text);

        if (listType != ListLoadType.LoadManual)
            Load(null, UploadMaterials, itemType);
    }
    private IEnumerator Load()
    {

        yield return null;
    }
    public void Load(SlimeMaterial.CraftInfo[] materials, ListItemType type)
    {
        itemList = new List<GameObject>();
        if (type == ListItemType.Material_1)
        {

            if (materials.Length == 0)
            {
                content.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                content.transform.GetChild(0).gameObject.SetActive(false);


                for (int i = 0; i < materials.Length; i++)
                {
                    GameObject newItem = CreateItem(type);
                    ListItem.Material_1 item = newItem.AddComponent<ListItem.Material_1>();
                    item.currentCount.text = "0";
                    item.itemName.text = materials[i].material.MaterialName;
                    item.itemImage.sprite = materials[i].material.MaterialImage;
                }
            }
        }
    }
    public void Load(List<float> effects, SlimeMaterial[] materials, ListItemType type)
    {
        itemList = new List<GameObject>();
        if (type == ListItemType.Effect_0)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                GameObject newItem = CreateItem(type);
                ListItem.Effect_0 item = newItem.AddComponent<ListItem.Effect_0>();
                item.itemInfo.text = effectsView.effect[i].effectInfo;
                item.itemName.text = effectsView.effect[i].effectName;
                item.itemValue.text = effects[i] + " f";
            }
        }
        else if (type == ListItemType.Material_0)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                GameObject newItem = CreateItem(type);
                ListItem.Material_0 item = newItem.AddComponent<ListItem.Material_0>();
                item.itemInfo.text = materials[i].MaterialInfo;
                item.itemName.text = materials[i].MaterialName;
                item.itemImage.sprite = materials[i].MaterialImage;

                SlimeMaterial material = materials[i];
                item.button.onClick.AddListener(() => DevelopManager.Instance.onSelectItem.Invoke(material, effects));
            }
        }
    }
    private GameObject CreateItem(ListItemType type)
    {
        GameObject newItem = null;
        newItem = Instantiate((GameObject)Resources.Load("prefab/" + type.ToString()) as GameObject);

        itemList.Add(newItem);

        newItem.transform.SetParent(content.transform);
        newItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        return newItem;
    }
    public void Clear()
    {
        if (itemList != null)
            for (int i = 0; i < itemList.Count; i++)
            {
                Destroy(itemList[i].gameObject);
            }
    }
    public enum ListItemType
    {
        Material_0,
        Effect_0,
        Material_1
    }
    public enum ListLoadType
    {
        LoadAll,
        LoadManual
    }

    public class ListItem
    {
        public class Material_1 : MonoBehaviour
        {
            public Image itemImage;
            public Text needCount, currentCount, itemName;
            private void Awake()
            {
                Initial();
            }
            private void OnEnable()
            {
                Initial();
            }
            private void OnDisable()
            {
                Finitial();
            }
            private void OnDestroy()
            {
                Finitial();
            }
            private void Initial()
            {
                itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
                needCount = gameObject.transform.GetChild(2).GetComponent<Text>();
                currentCount = gameObject.transform.GetChild(4).GetComponent<Text>();
                itemName = gameObject.transform.GetChild(5).GetComponent<Text>();
            }
            private void Finitial()
            {
                itemImage = null;
                needCount = null;
                currentCount = null;
                itemName = null;
            }
        }
        public class Effect_0 : MonoBehaviour
        {
            public Text itemName, itemInfo, itemValue;
            private void Awake()
            {
                Initial();
            }
            private void OnEnable()
            {
                Initial();
            }
            private void OnDisable()
            {
                Finitial();
            }
            private void OnDestroy()
            {
                Finitial();
            }
            private void Initial()
            {
                itemName = gameObject.transform.GetChild(0).GetComponent<Text>();
                itemInfo = gameObject.transform.GetChild(1).GetComponent<Text>();
                itemValue = gameObject.transform.GetChild(4).GetComponent<Text>();
            }
            private void Finitial()
            {
                itemInfo = null;
                itemName = null;
                itemValue = null;
            }
        }
        public class Material_0 : MonoBehaviour
        {
            public Text itemName, itemInfo;
            public Image itemImage;
            public Button button;
            private void Awake()
            {
                Initial();
            }
            private void OnEnable()
            {
                Initial();
            }
            private void OnDisable()
            {
                Finitial();
            }
            private void OnDestroy()
            {
                Finitial();
            }
            private void Initial()
            {
                itemName = gameObject.transform.GetChild(2).GetComponent<Text>();
                itemInfo = gameObject.transform.GetChild(3).GetComponent<Text>();
                itemImage = gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
                button = gameObject.GetComponent<Button>();
            }
            private void Finitial()
            {
                itemImage = null;
                itemInfo = null;
                itemName = null;
                button = null;
            }

        }
    }

}
