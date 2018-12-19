using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainer  {

    List<IItem> itemContainer { get; set; }

    List<int> startingItems { get; set; }


    List<int> randomItemSelectionPool { get; set; }
    int randomItemCount { get; set; }

    bool hasRandomItems { get; set; }



}
