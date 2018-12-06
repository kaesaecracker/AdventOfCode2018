package xyz.mattishub.adventofcode.day5

import java.io.File

fun main() {
    val input = File("input.txt").readText().trim()
    val collapsed = collapse(input)
    print("Length: ${collapsed.length}; Polymer: $collapsed")
}

fun collapse(polymer : String) :String{
    var remaining = polymer
    while (true) {
        val lastLen = remaining.length

        var index = 0
        while (index < remaining.length - 1) {
            val curr = remaining[index]
            val next = remaining[index + 1]

            if (curr != next && curr.toLowerCase() == next.toLowerCase()) {
                remaining = remaining.removeRange(index, index + 2)
            } else {
                index++
            }
        }

        if (lastLen == remaining.length) {
            break
        }
    }
    
    return remaining
}