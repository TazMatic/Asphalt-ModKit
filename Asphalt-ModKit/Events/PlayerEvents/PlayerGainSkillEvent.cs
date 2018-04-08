﻿using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    /// <summary>
    /// Called when a player press "order" on a craft interface
    /// </summary>
    public class PlayerGainSkillEvent : CancellableEvent
    {
        public Player Player { get; protected set; }

        public Skill Skill { get; protected set; }

        public PlayerGainSkillEvent(Player pPlayer, Skill pSkill) : base()
        {
            this.Player = pPlayer;
            this.Skill = pSkill;
        }
    }

    internal class PlayerGainSkillEventHelper
    {
        public IAtomicAction CreateAtomicAction(Player player, Skill skill)
        {
            PlayerGainSkillEvent cEvent = new PlayerGainSkillEvent(player, skill);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (!cEvent.IsCancelled())
                return CreateAtomicAction_original(player, skill);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(Player player, Skill skill)
        {
            throw new InvalidOperationException();
        }
    }
}
