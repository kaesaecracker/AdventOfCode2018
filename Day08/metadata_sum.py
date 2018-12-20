import helper, sys


def sum_metadata(curr_node: helper.Node) -> int:
    result = sum(curr_node.metadata)
    for c in curr_node.children:
        result = result + sum_metadata(c)

    return result


node = helper.get_nodes_from_input()
md_sum = sum_metadata(node)
print(md_sum)
