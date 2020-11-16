from game_server import GameServer


try:
    
    print("Starting gameserver")
    gs = GameServer()
    gs.run()
    
except KeyboardInterrupt:
    
    print("Keyboard interupt")
    try:
        gs.server_sock.shutdown()
    except: pass
    try:
        gs.server_sock.close()
    except: pass
    print("Server shutdown")
    
