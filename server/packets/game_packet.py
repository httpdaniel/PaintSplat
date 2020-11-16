from .packet_ids import PacketID, ID_LENGTH
from config import CODE_LEN, ENDIANNESS
from socket import MSG_WAITALL

class GamePacket:
    def __init__(self):
        self.id = PacketID.BAD_PACKET

    def from_id(id):
        return ID_CLASS.get(PacketID(id), ID_CLASS[PacketID.BAD_PACKET])

    def get_id(self):
        return int(CLASS_ID.get(type(self), DEFAULT_ID))

    def recv(socket):
        # Receive byte identifier
        id_byte = socket.recv(1)
        # Check connection reset
        if not id_byte:
            raise ConnectionResetError()

        print("First byte received %02x" % id_byte[0])
        return GamePacket.from_id(int.from_bytes(id_byte, ENDIANNESS))(socket)

### Client packets
class ClientPacket(GamePacket):
    def __init__(self, socket):
        # Store socket
        self.socket = socket

class BadPacket(ClientPacket):
    pass

class CreateGamePacket(ClientPacket):
    def __init__(self, socket):
        super().__init__(socket)
        pass # TODO: get args

class JoinGamePacket(ClientPacket):
    def __init__(self, socket):
        super().__init__(socket)
        self.code = socket.recv(CODE_LEN, MSG_WAITALL)

class SendUsernamePacket(ClientPacket):
    def __init__(self, socket):
        super().__init__(socket)
        pass # TODO: get args



### Server packets
class ServerPacket(GamePacket):
    def send(self, socket):
        socket.send(self.get_id().to_bytes(ID_LENGTH, ENDIANNESS))

class RoomCodePacket(ServerPacket):
    def __init__(self, code):
        super().__init__()
        self.code = code

    def send(self, socket):
        super().send(socket)
        socket.send(self.code.encode('ascii'))

class RoomOkPacket(ServerPacket):
    pass

class RoomFullPacket(ServerPacket):
    pass

class RoomFailPacket(ServerPacket):
    pass

class RoomNotFoundPacket(ServerPacket):
    pass



### ID References
DEFAULT_ID = 255
ID_CLASS = {
    PacketID.CL_CREATE_GAME:    CreateGamePacket,
    PacketID.CL_JOIN_GAME:      JoinGamePacket,
    PacketID.SE_ROOM_CODE:      RoomCodePacket,
    PacketID.SE_ROOM_NOT_FOUND: RoomNotFoundPacket,
    PacketID.CL_SEND_USERNAME:  SendUsernamePacket,
    PacketID.SE_ROOM_OK:        RoomOkPacket,
    PacketID.SE_ROOM_FULL:      RoomFullPacket,
    PacketID.SE_FAIL_ROOM:      RoomFailPacket,
    PacketID.BAD_PACKET:        BadPacket
}
CLASS_ID = {v:k  for k,v in ID_CLASS.items()}
