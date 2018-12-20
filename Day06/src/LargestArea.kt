package xyz.mattishub.adventofcode2018.day6

import java.io.File
import kotlin.math.abs

fun main() {
    val lines = File("input.txt").readLines()
    val input = parseInput(lines)

    for (x in 0..input.sizeX) {
        for (y in 0..input.sizeY) {
            val closest = findClosestTo(x, y, input)
            if (closest == null) {
                print('.')
                continue
            }

            closest.size++

            if (x == 0 || y == 0 || x == input.sizeX || y == input.sizeY) {
                closest.isAreaInfinite = true
                print('#') // border
            } else if (closest.x == x && closest.y == y) {
                print('=') // center of area
            } else {
                print(closest.name) // area
            }
        }

        println()
    }

    val biggest = input.centers
        .filter { !it.isAreaInfinite } // ignore infinitely large areas
        .sortedByDescending { it.size }
        .first()
    print("Biggest area: ${biggest.size}")
}

private fun findClosestTo(x: Int, y: Int, centers: LocationList): AreaCenter? {
    val sorted = centers.centers
        .map { Pair(it, it.distanceTo(x, y)) }
        .sortedBy { it.second }

    val closest = sorted[0]
    val secondClosest = sorted[1]
    return if (closest.second != secondClosest.second)
        closest.first
    else null
}
