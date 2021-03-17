﻿using Asphalt.Api.Event.PlayerEvents;
using Asphalt.Api.Event.RpcEvents;
using Asphalt.Api.Util;
using Asphalt.Events.InventoryEvents;
using Asphalt.Events.WorldObjectEvent;
using Eco.Gameplay.Components;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
//using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.GameActions;
using Eco.Shared.Math;
using Eco.Shared.Networking;
using System;
using System.Linq;

namespace Asphalt.Events
{
    internal static class EventManagerHelper
    {
        internal static void injectEvent(Type pEventType)
        {
            switch (pEventType.Name) //We hope Event names are unique
            {
                // Inventory Events

                case nameof(InventoryChangeSelectedSlotEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(SelectionInventory), typeof(InventoryChangeSelectedSlotEventHelper), "SelectIndex");
                    break;
                case nameof(InventoryMoveItemEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(InventoryChangeSet), typeof(InventoryMoveItemEventHelper1), "MoveStacks", new Type[] { typeof(ItemStack), typeof(ItemStack), typeof(User) });
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(InventoryChangeSet), typeof(InventoryMoveItemEventHelper2), "MoveStacks", new Type[] { typeof(ItemStack), typeof(ItemStack), typeof(int), typeof(User) });
                    break;

                // Player Events
                
                case nameof(PlayerTradeEvent):
                    Injection.InstallCreateAtomicAction(typeof(TradeAction), typeof(PlayerTradeEventHelper));
                    break;
                case nameof(PlayerClaimPropertyEvent):
                    Injection.InstallCreateAtomicAction(typeof(ClaimOrUnclaimProperty), typeof(PlayerClaimPropertyEventHelper));
                    break;
                case nameof(PlayerCompleteContractEvent):
                    Injection.InstallCreateAtomicAction(typeof(CompletedContract), typeof(PlayerCompleteContractEventHelper));
                    break;
                case nameof(PlayerCraftEvent):
                    Injection.InstallCreateAtomicAction(typeof(ItemCraftedAction), typeof(PlayerCraftEventHelper));
                    break;
                case nameof(PlayerEatEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(Stomach), typeof(PlayerEatEventHelper), "Eat");
                    break;
                case nameof(PlayerGainSkillEvent):
                    Injection.InstallCreateAtomicAction(typeof(GainSpecialty), typeof(PlayerGainSkillEventHelper));
                    break;
                case nameof(PlayerGainProfessionEvent):
                    Injection.InstallCreateAtomicAction(typeof(GainProfession), typeof(PlayerGainProfessionEventHelper));
                    break;
                case nameof(PlayerGetElectedEvent):
                    Injection.InstallCreateAtomicAction(typeof(WonElection), typeof(PlayerGetElectedEventHelper));
                    break;
                case nameof(PlayerHarvestEvent):
                    Injection.InstallCreateAtomicAction(typeof(HarvestOrHunt), typeof(PlayerHarvestEventHelper));
                    break;
                case nameof(PlayerInteractEvent):
                    Injection.InstallWithOriginalHelperPublicStatic(typeof(InteractionExtensions), typeof(PlayerInteractEventHelper), "MakeContext");
                    break;
                case nameof(PlayerLoginEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(User), typeof(PlayerLoginEventHelper), "Login");
                    break;
                case nameof(PlayerLogoutEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(User), typeof(PlayerLogoutEventHelper), "Logout");
                    break;
                case nameof(PlayerPayTaxEvent):
                    Injection.InstallCreateAtomicAction(typeof(PayTax), typeof(PlayerPayTaxEventHelper));
                    break;
                case nameof(PlayerPickUpOrPlaceObjectEvent):
                    //Injection.InstallCreateAtomicAction(typeof(PickUpPlayerActionManager), typeof(PlayerPickUpEventHelper));
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(PlaceOrPickUpObject), typeof(PlayerPickUpOrPlaceObjectEventHelper), "CreateAtomicAction", new Type[] { typeof(User), typeof(Item), typeof(Vector3i) });
                    //Injection.InstallWithOriginalHelperPublicInstance(typeof(PickUpPlayerActionManager), typeof(PlayerPickUpEventHelper2), "CreateAtomicAction", new Type[] { typeof(User), typeof(Type), typeof(Vector3i) });
                    break;
                /*case nameof(PlayerProposeVoteEvent):
                    Injection.InstallCreateAtomicAction(typeof(ProposeVotePlayerActionManager), typeof(PlayerProposeVoteEventHelper));
                    break;*/
                case nameof(PlayerReceiveGovernmentFundsEvent):
                    Injection.InstallCreateAtomicAction(typeof(ReceiveGovernmentFunds), typeof(PlayerReceiveGovernmentFundsEventHelper));
                    break;
                case nameof(PlayerRunForElectionEvent):
                    Injection.InstallCreateAtomicAction(typeof(ElectionAction), typeof(PlayerRunForElectionEventHelper));
                    break;
                case nameof(PlayerSendMessageEvent):
                    Injection.InstallCreateAtomicAction(typeof(ChatSent), typeof(PlayerSendMessageEventHelper));
                    break;
                case nameof(PlayerTeleportEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(Player), typeof(PlayerTeleportEventHelper), "SetPosition");
                    break;
                case nameof(PlayerUnlearnSkillEvent):
                    Injection.InstallCreateAtomicAction(typeof(SkillAction), typeof(PlayerUnlearnSkillEventHelper));
                    break;
                case nameof(PlayerVoteEvent):
                    Injection.InstallCreateAtomicAction(typeof(Vote), typeof(PlayerVoteEventHelper));
                    break;

                // RPC Events
                case nameof(RpcInvokeEvent):
                    Injection.Install(typeof(RPCManager).GetMethods(Injection.PUBLIC_STATC).First(mi => mi.Name == "InvokeOn" && mi.GetParameters().Length == 4), typeof(RpcInvokeEventHelper));
                    break;

                // World Events

                case nameof(WorldPolluteEvent):
                    Injection.InstallCreateAtomicAction(typeof(PolluteAir), typeof(WorldPolluteEventHelper));
                    break;

                // WorldObject Events

                case nameof(RubbleSpawnEvent):
                    Injection.InstallWithOriginalHelperPublicStatic(typeof(EcoObjectManager), typeof(RubbleSpawnEventHelper), "Add");
                    break;
                case nameof(TreeFellEvent):
                    Injection.InstallWithOriginalHelperNonPublicInstance(typeof(TreeEntity), typeof(TreeFellEventHelper), "FellTree");
                    break;
                case nameof(TreeChopEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(TreeEntity), typeof(TreeChopEventHelper), "TryApplyDamage");
                    break;

                case nameof(WorldObjectChangeTextEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(CustomTextComponent), typeof(WorldObjectChangeTextEventHelper), "SetText");
                    break;
                case nameof(WorldObjectDestroyedEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(WorldObject), typeof(WorldObjectDestroyedEventHelper), "Destroy");
                    break;
                case nameof(WorldObjectEnabledChangedEvent):
                    Injection.InstallWithOriginalHelperNonPublicInstance(typeof(WorldObject), typeof(WorldObjectEnabledChangedEventHelper), "set_Enabled");
                    break;
                case nameof(WorldObjectNameChangedEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(WorldObject), typeof(WorldObjectNameChangedEventHelper), "SetName");
                    break;
                case nameof(WorldObjectOperatingChangedEvent):
                    Injection.InstallWithOriginalHelperNonPublicInstance(typeof(WorldObject), typeof(WorldObjectOperatingChangedEventHelper), "set_Operating");
                    break;
                case nameof(WorldObjectPickupEvent):
                    Injection.InstallWithOriginalHelperPublicInstance(typeof(WorldObject), typeof(WorldObjectPickupEventHelper), "TryPickUp");
                    break;
            }
        }
    }
}
