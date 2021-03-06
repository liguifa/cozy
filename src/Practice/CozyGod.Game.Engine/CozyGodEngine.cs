﻿using CozyGod.Game.CardLibrary;
using CozyGod.Game.Craft;
using CozyGod.Game.GameConfig;
using CozyGod.Game.Interface;
using CozyGod.Game.Raffle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyGod.Game.Interface.ConfigEnum;

namespace CozyGod.Game.Engine
{
    public class CozyGodEngine : ICozyGodEngine
    {
        private ICraft craft;
        private IRaffle raffle;
        private ICardLibrary cardLibrary;
        private IGameConfig config;

        public void Init()
        {
            config = new GameConfigImpl();

            string contextPath;
            if (!config.TryGetStringConfig(StringConfigEnum.ContentPath, out contextPath))
            {
                return;
            }

            cardLibrary = new CardLibraryImpl(contextPath + "/Data/card.json");

            var c = new CraftImpl();
            c.Init(this);
            craft = c;

            var raf = new RaffleImpl();
            raf.Init(this);
            raffle = raf;
        }

        public ICraft GetCraft()
        {
            return craft;
        }

        public IRaffle GetRaffle()
        {
            return raffle;
        }

        public ICardLibrary GetCardLibrary()
        {
            return cardLibrary;
        }

        public IGameConfig GetConfig()
        {
            return config;
        }
    }
}
