from multiprocessing import Process
from game_list import GameList
from game_hub import GameHub
import socket
from threading import Thread
from packets.game_packet import *

PORT = 10500
CONN_BACKLOG = 5

class GameServer:
    def __init__(self):
        # Instantiate game list
        self.game_list = GameList()

    def handshake(self, client):
        # Configure timeout
        # client.settimeout(5)
        # Get packet
        packet = GamePacket.recv(client)
        # Respond to packet
        # print(packet)
        if type(packet) is CreateGamePacket:
            print("PACKET: Create Game")
            # Delegate to GameHub object
            new_hub = GameHub(self.game_list, packet)
            # Create process for Game Hub
            Process(target=new_hub.run).start()
        elif type(packet) is JoinGamePacket:
            print("PACKET: Join Game")
            # Check code
            game_hub = self.game_list.find(packet.code.decode('ascii'))
            if game_hub is not None:
                print("Game found")
                # Delegate responsability to GameHub objetct
                game_hub.queue_join(client)
            else:
                print("Room not found")
                # Send "RoomNotFound" packet
                RoomNotFoundPacket().send(client)
        else:
            print("Wrong handshake")
            # Wrong handshake packet received, ignore
            client.close()

    def run(self):
        # Open socket
        server_sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        server_sock.bind(('0.0.0.0', PORT))
        server_sock.listen(CONN_BACKLOG)

        # Wait for connections
        while True:
            # Accept connection
            client_sock, client_address = server_sock.accept()
            print("Incoming connection")
            # Send connection to other thread
            handshake_thread = Thread(target = self.handshake,
                args = (client_sock,))
            handshake_thread.start()
