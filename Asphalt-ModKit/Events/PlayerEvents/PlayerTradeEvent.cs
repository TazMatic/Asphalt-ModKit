using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Items;
using Eco.Shared.Localization;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerTradeEvent : CancellableEvent
    {
        public float NumberOfItems { get; set; }
        public BoughtOrSold BoughtOrSold { get; set; }
        public User ShopOwner { get; set; }
        public User Buyer { get; set; }
        public User Seller { get; set; }
        public IInteractable WorldObject { get; set; }
        public Item WorldObjectItem { get; }

        public PlayerTradeEvent(ref float pNumberOfItems, ref BoughtOrSold pBoughtOrSold, ref User pShopOwner, ref User pBuyer, ref User pSeller, ref IInteractable pWorldObject, ref Item pWorldObjectItem) : base()
        {
            this.NumberOfItems = pNumberOfItems;
            this.BoughtOrSold = pBoughtOrSold;
            this.ShopOwner = pShopOwner;
            this.Buyer = pBuyer;
            this.Seller = pSeller;
            this.WorldObject = pWorldObject;
            this.WorldObjectItem = pWorldObjectItem;
        }
    }

    internal class PlayerTradeEventHelper
    {
        public static bool Prefix(ref float NumberOfItems, ref BoughtOrSold BoughtOrSold, ref User ShopOwner, ref User Buyer, ref User Seller, ref IInteractable WorldObject, ref Item WorldObjectItem, ref IAtomicAction __result)
        {
            PlayerTradeEvent cEvent = new PlayerTradeEvent(ref NumberOfItems, ref BoughtOrSold, ref ShopOwner, ref Buyer, ref Seller, ref WorldObject, ref WorldObjectItem);
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
