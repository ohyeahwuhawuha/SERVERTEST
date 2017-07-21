using SuperSocket.SocketBase;
using System.IO;
using ProtoMessage;
using ProtoBuf;
using System;
using System.Collections.Generic;
using ServerA.Script.Base;
//using SuperSocket.SocketBase.Command;

namespace ServerP04
{
    class MyAppSession : AppSession<MyAppSession, MyRequestInfo>
    {
        private MemoryStream _memoryStream = new MemoryStream();
        public int uid = 0;
        public int posX = 0;
        public int posY = 0;
        public int posZ = 0;
        public int dir = 0;
        public int moveX = 0;
        public int moveZ = 0;
        public int time = 0;
        
        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
            uid = AppServer.SessionCount + 1;
            time = ServerTime.GetTime();
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }

        public void OnNewRequestReceived(MyRequestInfo requestInfo)
        {            
            switch (requestInfo.MessageType)
            {
                case NetMessage.EnterLevel:
                    REQ_EnterLevel(requestInfo.MessageType, requestInfo.Body);
                    break;

                case NetMessage.Move:
                    REQ_Move(requestInfo.MessageType, requestInfo.Body);
                    break;

                default:break;
            }            
        }

        public void SendMessage<T>(ushort msgType, T body) where T : IExtensible
        {
            _memoryStream.SetLength(0L);
            _memoryStream.Position = 0;
            Serializer.Serialize<T>(_memoryStream, body);

            ushort _size = Convert.ToUInt16(_memoryStream.Length);

            byte[] _sizeBuffer = BitConverter.GetBytes(_size);
            byte[] _typeBuffer = BitConverter.GetBytes(msgType);
            byte[] bufferBody = _memoryStream.ToArray();

            _memoryStream.Position = 0;
            _memoryStream.Write(_typeBuffer, 0, 2);
            _memoryStream.Write(_sizeBuffer, 0, 2);            
            _memoryStream.Write(bufferBody, 0, bufferBody.Length);

            byte[] _buffer = _memoryStream.ToArray();
            Send(_buffer, 0, _buffer.Length);
        }

        protected void REQ_EnterLevel(ushort _type, byte[] _buffer)
        {
            _memoryStream.SetLength(0L);
            _memoryStream.Position = 0;
            _memoryStream.Write(_buffer, 0, _buffer.Length);
            _memoryStream.Position = 0;
            REQEnterLevel _req = Serializer.Deserialize<REQEnterLevel>(_memoryStream);

            time = ServerTime.GetTime();

            RLTEnterLevel _rlt = new RLTEnterLevel();
            _rlt.uid = uid;
            foreach (var session in AppServer.GetAllSessions())
            {
                if(session != this)
                {
                    RLTEnterLevel.Actor actor = new RLTEnterLevel.Actor();
                    actor.uid = session.uid;
                    actor.posX = session.posX;
                    actor.posY = session.posY;
                    actor.posZ = session.posZ;
                    actor.dir = session.dir;
                    actor.moveX = session.moveX;
                    actor.moveY = session.moveZ;
                    actor.st = session.time;
                    _rlt.actors.Add(actor);
                }               
            }                      

            SendMessage<RLTEnterLevel>(_type, _rlt);

            BCEnterLevel _bc = new BCEnterLevel();
            _bc.uid = uid;
            _bc.posX = posX;
            _bc.posY = posY;
            _bc.posZ = posZ;
            _bc.dir = dir;
            _bc.st = time;
            MyAppServer _server = AppServer as MyAppServer;
            _server.BroadcastMessage<BCEnterLevel>(NetMessage.BC_EnterLevel, _bc);            
        }

        protected void REQ_Move(ushort _type, byte[] _buffer)
        {
            _memoryStream.SetLength(0L);
            _memoryStream.Position = 0;
            _memoryStream.Write(_buffer, 0, _buffer.Length);
            _memoryStream.Position = 0;
            REQMove _req = Serializer.Deserialize<REQMove>(_memoryStream);

            posX = _req.posX;
            posY = _req.posY;
            posZ = _req.posZ;
            moveX = _req.moveX;
            moveZ = _req.moveY;
            dir = _req.dir;
            time = _req.st;

            BCMove _rlt = new BCMove();
            _rlt.posX = posX;
            _rlt.posY = posY;
            _rlt.posZ = posZ;
            _rlt.moveX = moveX;
            _rlt.moveY = moveZ;
            _rlt.dir = dir;
            _rlt.uid = uid;
            _rlt.st = time;

            MyAppServer _server = AppServer as MyAppServer;
            _server.BroadcastMessage<BCMove>(NetMessage.BC_Move, _rlt);
        }
    }
}
