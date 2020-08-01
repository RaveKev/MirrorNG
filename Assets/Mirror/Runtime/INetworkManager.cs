using System;

namespace Mirror
{
    public interface INetworkManager
    {
        void StartClient(Uri uri);

        void StopHost();

        void StopServer();

        void StopClient();
    }
}
