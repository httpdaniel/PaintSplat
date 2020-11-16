from multiprocessing import Pipe
from random import choice
from uuid import uuid4
import selectors
from packets.game_packet import *

from config import CODE_LEN

class GameHub:
    def __init__(self, game_list, packet):
        # TODO: use game options

        # Get Game creator from packet
        creator = packet.socket
        # Assign UUID
        self.uuid = uuid4()
        # Open Queue
        self.queue, self.queue_send = Pipe(False)
        # Generate code
        while True:
            self.code = ''.join([chr(choice(range(ord('A'), ord('Z'))))
                for _ in range(CODE_LEN)])
            if game_list.register(self.code, self):
                break
        # Initialize connection list (actually a dictionary)
        self.conn_list = {}
        self.conn_list[creator] = None
        self.creator = creator

        # Send game code
        RoomCodePacket(self.code).send(creator)

    def lobby(self):
        # Register IO selectors
        io = selectors.DefaultSelector()
        io.register(self.queue, selectors.EVENT_READ, self.queue)
        io.register(self.creator, selectors.EVENT_READ, self.creator)

        # Wait for event
        while True:
            events = io.select() # Wait for IO event

            # Check IO event
            for event, _ in events:
                if event.data == self.queue: # Queue event
                    self.player_joined()
                else: # Socket event
                    print("%s: Got new message" % str(self.uuid))
                    # Check disconnection with try-except
                    try:
                        packet = GamePacket.recv(event.data)

                        # Check packet
                    except ConnectionResetError:
                        player_left(self)

    def player_joined(self):
        print("%s: Got new player" % str(self.uuid))
        new_player = self.queue.recv()
        io.register(new_player, selectors.EVENT_READ, new_player)

    def player_left(self):
        print("%s: Player left" % str(self.uuid))
        io.unregister(event.data)

    def run(self):
        print("%s: Now running" % str(self.uuid))

        self.lobby()

    def queue_join(self, client):
        self.queue_send.send(client)

    def get_code_len():
        return CODE_LEN
