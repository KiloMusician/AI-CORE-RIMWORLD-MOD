Creating a custom scenario in RimWorld involves creating a new ScenarioDef in your mod. A ScenarioDef defines the starting conditions for a game, such as the starting items, the map conditions, and the player's faction.

Here's a basic example of how you might define a new ScenarioDef in XML:

```xml
<ScenarioDef>
  <defName>MyCustomScenario</defName>
  <label>My Custom Scenario</label>
  <description>This is my custom scenario.</description>
  <scenario>
    <parts>
      <li Class="ScenPart_StartWithPawns">
        <count>3</count>
      </li>
      <li Class="ScenPart_StartingThing_Defined">
        <def>MealSurvivalPack</def>
        <count>30</count>
      </li>
    </parts>
  </scenario>
</ScenarioDef>
```

This scenario starts with 3 colonists and 30 survival meals. You can add more parts to the scenario to customize it further.

To add this scenario to your mod, you would create a new XML file in your mod's [``Defs``](command:_github.copilot.openRelativePath?%5B%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fc%3A%2FProgram%20Files%20(x86)%2FSteam%2Fsteamapps%2Fcommon%2FRimWorld%2FMods%2FAI-CORE-RIMWORLD-MOD%2FDefs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%5D "c:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\AI-CORE-RIMWORLD-MOD\Defs") folder and paste the above XML into it. Then, when you load your mod in RimWorld, your custom scenario should be available in the scenario selection screen.

Please note that creating a custom scenario in RimWorld requires a basic understanding of XML and the RimWorld modding API. You may need to refer to the RimWorld modding documentation or other modding resources for more detailed information.