using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopUpdater : MonoBehaviour
{
    private CustomList effectList, materialList;

    private void Start()
    {
        effectList = gameObject.transform.GetChild(0).GetComponent<CustomList>();
        materialList = gameObject.transform.GetChild(1).GetComponent<CustomList>();

        DevelopManager.Instance.onSelectItem.AddListener(UpdateData);
    }

    private void UpdateData(SlimeMaterial material, List<float> effect)
    {

        effectList.Clear();
        effectList.Load(material.MaterialEffecting, null, CustomList.ListItemType.Effect_0);

        materialList.Clear();
        materialList.Load(material.MaterialCrafting.ToArray(), CustomList.ListItemType.Material_1);

    }

}
