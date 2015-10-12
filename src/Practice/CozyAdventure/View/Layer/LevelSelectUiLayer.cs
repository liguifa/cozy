﻿using CocosSharp;
using Cozy.Game.Manager;
using CozyAdventure.Game.Logic;
using CozyAdventure.Game.Object;
using CozyAdventure.Public.Controls;
using CozyAdventure.View.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Layer
{
    public class LevelSelectUiLayer : CCLayer
    {
        public override void OnEnter()
        {
            base.OnEnter();
            InitUI();
            RegisterEvent();
        }

        public override void OnExit()
        {
            base.OnExit();
            UnregisterEvent();
        }

        private void RegisterEvent()
        {
            MessageManager.RegisterMessage("Message.GotoMap.Success", OnGotoMapSuccess);
            MessageManager.RegisterMessage("Message.GotoMap.Failed", OnGotoMapFailed);
        }

        private void UnregisterEvent()
        {
            MessageManager.UnRegisterMessage("Message.GotoMap.Failed", OnGotoMapFailed);
            MessageManager.UnRegisterMessage("Message.GotoMap.Success", OnGotoMapSuccess);
        }

        private void InitUI()
        {
            int count = 0;
            for(int i = 0; i < 2; ++i)
            {
                for(int j = 0; j < 3; ++j)
                {
                    if(count < 3)
                    {
                        int index   = count;
                        var bt      = new CozyColorSampleButton(100, 100, CCColor4B.Blue, CCColor4B.Green)
                        {
                            Text        = string.Format("level : {0}", index),
                            Position    = new CCPoint(i * (100 + 10), j * (100 + 10)),
                            OnClick     = () =>
                            {
                                PlayerObject.Instance.Self.CurrLevel = index;
                                FarmMapLogic.EnterMap(PlayerObject.Instance.Self.CurrLevel);
                            }
                        };
                        this.AddChild(bt);
                        this.AddEventListener(bt.EventListener);
                        ++count;
                    }
                }
            }
        }

        private void OnGotoMapSuccess()
        {
            AppDelegate.SharedWindow.DefaultDirector.ReplaceScene(new AdventureScene());
        }

        private void OnGotoMapFailed()
        {

        }
    }
}