import helper


def calc_node_value(node: helper.Node) -> int:
    if node.childCount == 0:
        return sum(node.metadata)
    # has child nodes
    value = 0
    for i in node.metadata:
        if 0 <= i <= node.childCount:
            child = node.children[i-1]
            value = value + calc_node_value(child)

    return value


root = helper.get_nodes_from_input()
root_node_value = calc_node_value(root)
print(root_node_value)
