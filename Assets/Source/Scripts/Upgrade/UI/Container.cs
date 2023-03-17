using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public IReadOnlyList<Transform> CreateSlots(int capacity)
    {
        List<Transform> slots = new List<Transform>();

        for (int i = 0; i < capacity; i++)
        {
            Transform newTransform = new GameObject("Position " + i, typeof(RectTransform)).transform;
            newTransform.parent = transform;
            newTransform.localScale = Vector3.one;
            slots.Add(newTransform);
        }

        return slots;
    }
}
