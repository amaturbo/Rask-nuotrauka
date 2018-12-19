using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class WorkableItemInfo : MonoBehaviour
{
    public int requiredItemId,
        resourceId,
        resourceQty, // quantity of available resource
        itemUsagePerResource;
    public ToolType RequiredToolType;

}
