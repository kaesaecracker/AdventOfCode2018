from helper import *

# Input
LAST_MARBLE_WORTH = 70918
PLAYERS = 464


def print_winner_score(p: int, lmw: int):
    mm = MarbleMania(p, lmw)
    while not mm.game_ended():
        mm.place_next_marble()

    print(f"Winning elf's score: {max(mm.points)}")


print_winner_score(PLAYERS, LAST_MARBLE_WORTH)
print_winner_score(PLAYERS, LAST_MARBLE_WORTH * 100)
