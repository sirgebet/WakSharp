﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SilverSock;

namespace WakSharp.Network.Realm
{
    public class RealmSession
    {
        public SilverSocket _socket { get; set; }

        public RealmSession(SilverSocket socket)
        {
            this._socket = socket;
            this._socket.OnDataArrivalEvent += new SilverEvents.DataArrival(_socket_OnDataArrivalEvent);
            this._socket.OnSocketClosedEvent += new SilverEvents.SocketClosed(_socket_OnSocketClosedEvent);
        }

        private void _socket_OnSocketClosedEvent()
        {
            
        }

        private void _socket_OnDataArrivalEvent(byte[] data)
        {
            try
            {
                var packet = new WakfuClientMessage(data);
                Utilities.ConsoleStyle.Debug("OPCODE : " + packet.OPCode.ToString() + ", size : " + packet.Size);

                switch (packet.OPCode)
                {
                    case WakfuOPCode.CMSG_VERSION:
                        this.Handle_CMSG_VERSION(new Packets.CMSG_VERSION(data));
                        break;
                }
            }
            catch (Exception e)
            {
                Utilities.ConsoleStyle.Error("Can't read packet : " + e.ToString());
            }
        }

        private void Handle_CMSG_VERSION(Packets.CMSG_VERSION packet)
        {
            Utilities.ConsoleStyle.Debug("Check client version .. " + packet.ToString());
        }
    }
}
