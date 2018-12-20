package xyz.mattishub.adventofcode2018.day6

import kotlin.math.abs

private val names = ('A'..'Z').union('a'..'z').toList()

data class LocationList(
    val centers: List<AreaCenter>,
    val sizeX: Int,
    val sizeY: Int
)

data class AreaCenter(
    val x: Int,
    val y: Int,
    val name: Char,
    var size: Int = 0,
    var isAreaInfinite: Boolean = false
) {
    fun distanceTo(other: AreaCenter) = distanceTo(other.x, other.y)

    fun distanceTo(otherX: Int, otherY: Int): Int {
        val dx = abs(x - otherX)
        val dy = abs(y - otherY)
        return dx + dy
    }

    override fun toString() = "[$name @ ($x | $y), A=${if (isAreaInfinite) "Infty" else size}]"
}

fun parseInput(lines: List<String>): LocationList {
    var biggestX = 0
    var biggestY = 0
    return LocationList(
        lines.mapIndexed { index, s ->
            val xStr = s.substring(0 until s.indexOf(','))
            val x = xStr.toInt()
            val y = s.substring(xStr.length + 2 until s.length).toInt()

            if (x > biggestX) biggestX = x
            if (y > biggestY) biggestY = y

            AreaCenter(x, y, names[index])
        },
        biggestX,
        biggestY
    )
}