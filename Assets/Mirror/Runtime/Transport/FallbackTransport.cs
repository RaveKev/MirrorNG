﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Mirror
{
    public class FallbackTransport : Transport
    {

        public Transport[] transports;

        public override IEnumerable<string> Scheme
        {
            get
            {
                return GetTransport().Scheme;
            }
        }

        private Transport GetTransport()
        {
            foreach (Transport transport in transports)
            {
                if (transport.Supported)
                    return transport;
            }
            throw new PlatformNotSupportedException("None of the transports is supported in this platform");
        }

        public override bool Supported => GetTransport() != null;

        public override Task<IConnection> AcceptAsync()
        {
            return GetTransport().AcceptAsync();
        }

        public override Task<IConnection> ConnectAsync(Uri uri)
        {
            return GetTransport().ConnectAsync(uri);
        }

        public override void Disconnect()
        {
            foreach (Transport transport in transports)
            {
                if (transport.Supported)
                    transport.Disconnect();
            }
        }

        public override Task ListenAsync()
        {
            return GetTransport().ListenAsync();
        }

        public override IEnumerable<Uri> ServerUri()
        {
            return GetTransport().ServerUri();
        }
    }
}