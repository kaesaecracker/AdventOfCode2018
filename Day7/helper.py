from typing import Dict, List

stepsType = Dict[str, List[str]]


class Steps:
    remaining_steps: stepsType
    done_steps: List[str] = list()

    def __init__(self):
        with open('input.txt') as f:
            input_lines = f.readlines()
        input_lines = [l.strip() for l in input_lines]

        steps: stepsType = dict()
        for l in input_lines:
            parts = l.split(' ')
            (first, second) = (parts[1][0], parts[7][0])

            if first not in steps:
                steps[first] = list()
            if second not in steps:
                steps[second] = list()

            steps[second].append(first)

        self.remaining_steps = steps

    def dependencies_done(self, step: str):
        for dependency in self.remaining_steps[step]:
            if dependency not in self.done_steps:
                return False
        return True

    def get_next_steps(self) -> List[str]:
        next_steps = list(filter(self.dependencies_done, self.remaining_steps))
        next_steps.sort()
        return next_steps
