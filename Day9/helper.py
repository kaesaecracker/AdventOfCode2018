from collections import deque
from typing import List


class MarbleMania:
    circle: deque
    marble_number: int = 0
    marble_index: int = 0
    last_marble_worth: int = 0
    last_marble_bonus: bool
    points: List[int]
    players: int

    def __init__(self, num_players: int, last_marble_worth: int):
        self.circle = deque(list())
        self.points = [0] * num_players

        self.players = num_players
        self.last_marble_worth = last_marble_worth

        self.circle.append(0)

    def game_ended(self):
        return self.marble_number > self.last_marble_worth

    def current_player(self) -> int:
        return self.marble_number % len(self.points)

    def norm_circle_index(self, index) -> int:
        if index < 0:
            return index + len(self.circle)
        if index >= len(self.circle):
            return index - len(self.circle)
        return index

    def place_next_marble(self):
        new_marble_number = self.marble_number + 1

        if new_marble_number % 23 != 0:
            self.circle.rotate(-2)
            self.circle.appendleft(new_marble_number)
        else:
            self.points[self.current_player()] += new_marble_number
            self.circle.rotate(+7)
            self.points[self.current_player()] += self.circle.popleft()

        self.marble_number += 1
