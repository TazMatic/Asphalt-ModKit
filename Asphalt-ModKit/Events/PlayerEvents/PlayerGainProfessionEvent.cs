using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player gains a profession
    /// </summary>
    public class PlayerGainProfessionEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public Skill Skill { get; set; }

        public PlayerGainProfessionEvent(ref Player pPlayer, ref Skill pSkill) : base()
        {
            this.Player = pPlayer;
            this.Skill = pSkill;
        }
    }

    internal class PlayerGainProfessionEventHelper
    {
        public static bool Prefix(ref Player actor, ref Skill skill, ref IAtomicAction __result)
        {
            PlayerGainSkillEvent cEvent = new PlayerGainSkillEvent(ref actor, ref skill);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (cEvent.IsCancelled())
            {
                __result = new FailedAtomicAction(new LocString());
                return false;
            }

            return true;
        }
    }
}
