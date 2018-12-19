using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem {

    /// <summary>
    /// Item ID
    /// </summary>`
    int ID { get; set; }

    /// <summary>
    /// Name of the item
    /// </summary>
    /// 

    string  Name        { get; }
    /// <summary>
    /// Weight of the item
    /// </summary>
    /// 

    float   Weight      { get; set; }

    /// <summary>
    /// Is the item usable?
    /// </summary>
    bool    IsUsable    { get; set; }

    /// <summary>
    /// Provides basic information about the item
    /// </summary>
    string  ItemInfo    { get; set; }

    /// <summary>
    /// Quantity of item
    /// </summary>
    int Quantity { get; set; }

    Sprite Icon { get; set; }

    /// <summary>
    /// Copies an item object
    /// </summary>
    /// <returns> A copy of an item</returns>
    IItem Copy();

    //string Slug ? (string to get resources for an item (e.g. a sprite, etc..))

}
