﻿// Copyright 2015 ThoughtWorks, Inc.

// This file is part of Gauge-CSharp.

// Gauge-CSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Gauge-CSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Gauge-CSharp.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using Gauge.Messages;
using Google.Protobuf;

namespace Gauge.CSharp.Core
{
    public abstract class AbstractGaugeConnection : IDisposable
    {
        protected readonly ITcpClientWrapper TcpClientWrapper;

        protected AbstractGaugeConnection(ITcpClientWrapper tcpClientWrapper)
        {
            TcpClientWrapper = tcpClientWrapper;
        }

        public bool Connected
        {
            get { return TcpClientWrapper.Connected; }
        }

        public void WriteMessage(IMessage request)
        {
            request.WriteTo(TcpClientWrapper.GetStream());
            TcpClientWrapper.GetStream().Flush();
        }

        public IMessage ReadBytes(IMessage message)
        {
            var networkStream = TcpClientWrapper.GetStream();
            new CodedInputStream(networkStream).ReadMessage(message);
            return message;
        }

        protected static long GenerateMessageId()
        {
            return DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond;
        }

        public void Dispose()
        {
            TcpClientWrapper.Close();
        }
    }
}