using raylib_cs_playground.Core.Enums;
using Steamworks;
using Steamworks.Data;

namespace raylib_cs_playground.Core;

public interface INetworkManager
{
    public void Close();
}

public class ServerSocketNetworkManager : ISocketManager, INetworkManager
{
    private readonly SocketManager _socketManager;

    public ServerSocketNetworkManager(ushort port)
    {
        _socketManager = SteamNetworkingSockets.CreateNormalSocket(NetAddress.AnyIp(port), this);
    }

    public void Close()
    {
        _socketManager.Close();
    }

    public void OnConnecting(Connection connection, ConnectionInfo data)
    {
        connection.Accept();
        Console.WriteLine($"{data.Identity} is connecting");
    }

    public void OnConnected(Connection connection, ConnectionInfo data)
    {
        Console.WriteLine($"{data.Identity} has joined the game");
    }

    public void OnDisconnected(Connection connection, ConnectionInfo data)
    {
        Console.WriteLine($"{data.Identity} is out of here");
    }

    public void OnMessage(Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum,
        long recvTime, int channel)
    {
        Console.WriteLine($"We got a message from {identity}!");

        connection.SendMessage(data, size);
    }
}

public class ClientSocketNetworkManager : IConnectionManager, INetworkManager
{
    private readonly ConnectionManager _connectionManager;

    public ClientSocketNetworkManager(NetAddress address)
    {
        _connectionManager = SteamNetworkingSockets.ConnectNormal(address, this);
    }

    public void OnConnecting(ConnectionInfo info)
    {
    }

    public void OnConnected(ConnectionInfo info)
    {
    }

    public void OnDisconnected(ConnectionInfo info)
    {
    }

    public void OnMessage(IntPtr data, int size, long messageNum, long recvTime, int channel)
    {
    }

    public void Close()
    {
        _connectionManager.Close();
    }
}

public class Network
{
    private INetworkManager _manager;
    private NetworkState _state = NetworkState.Disconnected;
    public bool IsHost => _state == NetworkState.Server;
    public bool IsConnected => _state == NetworkState.Client;

    public void Init()
    {
        try
        {
            SteamClient.Init(480);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void Close()
    {
        SteamClient.Shutdown();
        if (_state != NetworkState.Disconnected)
            _manager.Close();
    }

    public bool CreateServer(ushort port)
    {
        if (_state != NetworkState.Disconnected) return false;

        _state = NetworkState.Connecting;

        try
        {
            _manager = new ServerSocketNetworkManager(port);
        }
        catch (Exception e)
        {
            _state = NetworkState.Disconnected;
            Console.WriteLine(e);
            return false;
        }

        _state = NetworkState.Server;

        return true;
    }

    public bool CreateClient(NetAddress address)
    {
        if (_state != NetworkState.Disconnected) return false;

        _state = NetworkState.Connecting;

        try
        {
            _manager = new ClientSocketNetworkManager(address);
        }
        catch (Exception e)
        {
            _state = NetworkState.Disconnected;
            Console.WriteLine(e);
            return false;
        }

        _state = NetworkState.Client;

        return true;
    }
}