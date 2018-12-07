package xyz.mattishub.adventofcode2018.day6

import java.io.File

fun main() {
    val input = parseInput(File("input.txt").readLines())

    var size = 0
    for (x in 0..input.sizeX)
        for (y in 0..input.sizeY) {
            val sumDist = input.centers
                .map { it.distanceTo(x, y) }
                .sum()

            if (sumDist < 10000) size++
        }

    print(size)
}