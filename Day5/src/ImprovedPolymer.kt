package xyz.mattishub.adventofcode.day5

import java.io.File

fun main() {
    val input = File("input.txt").readText().trim()
    val types = input.toLowerCase().toList().distinct()

    var best = Pair(Int.MAX_VALUE, ' ')
    for (type in types) {
        val collapsedLen = collapse(
            input.replace(type.toString(), "", true)
        ).length

        if (collapsedLen < best.first)
            best = Pair(collapsedLen, type)
    }

    print("Type '${best.second}', Length ${best.first}")
}