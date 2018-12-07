from string import ascii_uppercase
from helper import *


class Worker:
    current_step: str = ''
    remaining_time: int = -1

    def step(self):
        if self.current_step == '':
            return

        self.remaining_time -= 1
        if self.remaining_time == -1:
            steps.done_steps.append(self.current_step)
            self.current_step = ''

    def __str__(self):
        if self.current_step == '':
            return '- (-)'
        return f'{self.current_step} ({self.remaining_time})'


def get_idle_worker():
    for w in workers:
        if w.current_step == '':
            return w
    return None


def all_workers_idle():
    for w in workers:
        if w.current_step != '':
            return False
    return True


def all_workers_have_work():
    for w in workers:
        if w.current_step == '':
            return False
    return True


steps = Steps()
workers: List[Worker] = list()
secsPassed: int = 0
for i in range(0, 5):
    workers.append(Worker())

while len(steps.remaining_steps) > 0 or not all_workers_idle():
    nextSteps = steps.get_next_steps()

    idleW = get_idle_worker()
    while idleW is not None and len(nextSteps) > 0:
        enqueueStep = nextSteps.pop(0)
        steps.remaining_steps.pop(enqueueStep)

        idleW.current_step = enqueueStep
        idleW.remaining_time = 60 + ascii_uppercase.index(enqueueStep)

        idleW = get_idle_worker()

    [w.step() for w in workers]
    secsPassed += 1

    print(secsPassed, workers[0], workers[1], workers[2], workers[3], workers[4], sep='\t|\t')

print()
print(secsPassed)
