from enum import IntEnum

ID_LENGTH = 1 # Length of the identifier in bytes

class PacketID(IntEnum):
    CL_CREATE_GAME      = 0 # Sent by the player who creates the game
    CL_JOIN_GAME        = 1 # Sent by a player who wants to join a game
    SE_ROOM_CODE        = 2 # Sent by the server to the player who creates the game
    SE_ROOM_NOT_FOUND   = 3 # Sent by the server to a joining player when the code
                            # he sent is not found
    CL_SEND_USERNAME    = 4 # Sent by a player creating or joining a game
    SE_ROOM_OK          = 5 # Sent by the server when the player could join a game
    SE_ROOM_FULL        = 6 # Sent by the server when trying to join a full room
    SE_FAIL_ROOM        = 7 # Sent by the server when failed to create a room
    SE_PLAYER_JOIN      = 8 # Sent by the server when a player joins to all other 
                            # players
    SE_PLAYER_SYNC      = 9 # Sent by the server to the joining player
    SE_START_REQUEST    = 10# Sent by the server when starting the game
    CL_PAINT_HIT_REQ    = 11# Sent by the client when the target hits the canvas
    SE_PAINT_HIT_OK     = 12# Server confirms paintball hit
    SE_PAINT_HIT_FAIL   = 13# Server denies paintball hit
    SE_PAINT_BALL_SYNC  = 14# Sent by the server when other player hit the canvas

    BAD_PACKET = 255

    def _missing_(_):
        return PacketID.BAD_PACKET
