﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg.Happy
{
    public struct Msg_HappyPlayerMove : MsgBase
    {
        public int Id { get { return MsgId.HappyPlayerMove; } }

        public uint Uid { get; set; }
        public int X { get; set; }
        public int Y { get; set; }


        public void W(NetOutgoingMessage om)
        {
            om.Write(Uid);
            om.Write(X);
            om.Write(Y);
        }

        public void R(NetIncomingMessage im)
        {
            Uid = im.ReadUInt32();
            X = im.ReadInt32();
            Y = im.ReadInt32();
        }
    }
}
