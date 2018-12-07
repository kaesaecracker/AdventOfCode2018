from helper import *

steps = Steps()
while len(steps.remaining_steps) > 0:
    nextSteps = steps.get_next_steps()

    print(nextSteps[0], end='')
    steps.remaining_steps.pop(nextSteps[0])
    steps.done_steps.append(nextSteps[0])
