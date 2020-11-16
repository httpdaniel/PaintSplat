from enum import Enum

class ClientPacket(Enum):
    CREATE_GAME = 0
    JOIN_GAME = 1
    NAME_INFO = 2

    BAD_PACKET = 255

    def _missing_(_):
        return ClientPacket.BAD_PACKET
