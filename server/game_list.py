from multiprocessing import Manager
from threading import Lock

# Shared object which keeps record of open hubs
class GameList:
    def __init__(self):
        # Create and start Manager
        self.manager = Manager()

        # Create dict and Lock
        self.games = self.manager.dict()
        self.lock = Lock()

    def register(self, code, hub):
        with self.lock:
            if code in self.games:
                return False
            self.games[code] = hub
        return True

    def unregister(self, code):
        with self.lock:
            del self.games[code]

    def find(self, code):
        with self.lock:
            if code in self.games:
                return self.games[code]
            return None
