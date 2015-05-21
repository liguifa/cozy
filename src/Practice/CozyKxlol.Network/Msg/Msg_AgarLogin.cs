﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_AgarLogin : MsgBase
    {
        public int Id { get { return MsgId.AgarLogin; } }

        public string Name { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(Name);
        }

        public void R(NetIncomingMessage im)
        {
            Name = im.ReadString();
        }
    }
}
