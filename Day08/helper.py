from typing import List


class Node:
    childCount: int = 0
    metadataCount: int = 0
    children: List
    metadata: List[int]

    def __init__(self):
        self.children = []  # type: List[Node]
        self.metadata = []  # type: List[int]

    def __str__(self):
        return f'[C={self.childCount}/{len(self.children)}, M={self.metadataCount}/{len(self.metadata)}]'


class InputParser:
    remaining_nums: List[int]
    current_depth: int = 0
    max_depth: int = 0

    def __init__(self, nums: List[int]):
        self.remaining_nums = nums

    def parse_node(self) -> Node:
        self.current_depth = self.current_depth + 1
        if self.current_depth > self.max_depth:
            self.max_depth = self.current_depth

        elem = Node()
        elem.childCount = self.remaining_nums.pop(0)
        elem.metadataCount = self.remaining_nums.pop(0)

        for i in range(0, elem.childCount):
            elem.children.append(self.parse_node())
        for i in range(0, elem.metadataCount):
            elem.metadata.append(self.remaining_nums.pop(0))

        self.current_depth = self.current_depth - 1
        return elem


def get_nodes_from_input() -> Node:
    with open('input.txt') as file:
        input_lines = file.readlines()
    input_nums = [int(e) for e in input_lines[0].strip().split(' ')]

    parser = InputParser(input_nums)
    result = parser.parse_node()
    print(f'Element depth: {parser.max_depth}')

    return result
