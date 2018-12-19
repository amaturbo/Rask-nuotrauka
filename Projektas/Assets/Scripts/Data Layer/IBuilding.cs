using UnityEngine;

interface IBuilding
{
    //building ID
    int ID { get; set; }

    //name of the building
    string Name { get; set; }

    //how much wood dos it cost
    int CostWood { get; set; }

    //how much rock dos it cost
    int CostRock { get; set; }

    //how much clay dos it cost
    int CostClay { get; set; }

    //description of the building
    string BuildingInfo { get; set; }

    //color that the building will turn when hovered over by the cursor to highlight it
    Color HoverColor { get; set; }

    //color that was set
    Color StartColor { get;  set; }

    //the render of the object
    Renderer Render { get; set; }

    //do you have enough wood
    bool EnoughWood();

    //do you have enough rock
    bool EnoughRock();

    //do you have enough clay
    bool EnoughClay();

    //builds the building
    void Build();

    //upgrades the building
    void Upgrade();

    //Deconstructs the building, harvesting back some of the used matterial
    void Deconstruct();
}