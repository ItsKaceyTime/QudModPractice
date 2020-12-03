// XRL.World.QuestManagers.KaceyMod_KingsHope
using System;
using XRL.Core;
using XRL.World;
using XRL.World.Parts;

[Serializable]
public class KaceyMod_KingsHope : QuestManager
{
    public override void OnQuestAdded()
    {
        XRLCore.Core.Game.Player.Body.Inventory.ForeachObject(delegate(GameObject GO)
        {
            if (GO.FindObjectInInventory("KaceyMod_QuestGlove"))
            {
                XRLCore.Core.Game.FinishQuestStep("King's Hope", "Find the Campsite");
                XRLCore.Core.Game.FinishQuestStep("King's Hope", "Investigate the Campsite");
                return false;
            }
            return true;
        });
        IComponent<GameObject>.ThePlayer.AddPart(this);
        IComponent<GameObject>.ThePlayer.RegisterPartEvent(this, "Took");
    }
    public override void Register(GameObject Object)
    {
        Object.RegisterPartEvent(this, "EnteredCell");
        base.Register(Object);
    }
    public override void OnQuestComplete()
    {
        IComponent<GameObject>.ThePlayer.RemovePart(this);
    }

    public override GameObject GetQuestInfluencer()
    {
        return GameObject.findByBlueprint("KaceyMod_King");
    }

    public override bool FireEvent(Event E)
    {
        if (E.ID == "Took")
        {
            GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
            if (gameObjectParameter.HasPart("KaceyMod_QuestGlove"))
            {
                XRLCore.Core.Game.FinishQuestStep("King's Hope", "Return to King");
            }
                XRLCore.Core.Game.Player.Body.Inventory.ForeachObject(delegate(GameObject GO)
                {
                    if (GO.HasPart("KaceyMod_QuestGlove"))
                    {
                        XRLCore.Core.Game.FinishQuestStep("King's Hope", "Investigate the Campsite");
                        return false;
                    }
                    return true;
                });
                IComponent<GameObject>.ThePlayer.AddPart(this);
                IComponent<GameObject>.ThePlayer.RegisterPartEvent(this, "Took");
        }
        	else if (E.ID == "EnteredCell" && (ParentObject.InZone("JoppaWorld.14.20.2.0.10")))
		{
			XRLCore.Core.Game.FinishQuestStep("King's Hope", "Find the Campsite");
		}
		return base.FireEvent(E);
    }

}