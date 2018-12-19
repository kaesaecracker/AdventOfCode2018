import java.io.File
import java.lang.IllegalArgumentException
import kotlin.math.min

data class Star(var position: Pair<Long, Long>, val velocity: Pair<Long, Long>)

fun parsePair(input: String): Pair<Long, Long> {
    if (input.first() != '<' || input.last() != '>') throw IllegalArgumentException()

    val commaIndex = input.indexOf(',')
    return Pair(
        input.substring(1 until commaIndex).trim().toLong(),
        input.substring(commaIndex + 1 until input.indexOf('>')).trim().toLong()
    )
}

fun getInput(): List<Star> {
    val result = mutableListOf<Star>()
    for (l in File("input.txt").readLines()) {
        val firstPart = l.substring(l.indexOf('<'), l.indexOf('>') + 1)
        val secondPart = l.substring(l.lastIndexOf('<'))

        result.add(
            Star(
                parsePair(firstPart),
                parsePair(secondPart)
            )
        )
    }

    return result
}

fun List<Star>.move() {
    for (s in this) {
        s.position = s.position + s.velocity
    }
}

fun List<Star>.spreadSize(): Long {
    val positions = this.map { it.position }.distinct()
    val minX = positions.minBy { it.first }!!.first
    val maxX = positions.maxBy { it.first }!!.first
    val minY = positions.minBy { it.second }!!.second
    val maxY = positions.maxBy { it.second }!!.second

    return maxX - minX + maxY - minY
}

fun List<Star>.print() {
    val positions = this.map { it.position }.distinct()
    val minX = positions.minBy { it.first }!!.first
    val maxX = positions.maxBy { it.first }!!.first
    val minY = positions.minBy { it.second }!!.second
    val maxY = positions.maxBy { it.second }!!.second

    for (y in minY..maxY) {
        for (x in minX..maxX)
            print(
                if (positions.contains(Pair(x, y))) '#'
                else '~'
            )

        println()
    }
}

private operator fun Pair<Long, Long>.plus(second: Pair<Long, Long>): Pair<Long, Long> = Pair(
    this.first + second.first,
    this.second + second.second
)

fun main(args: Array<String>) {
    val stars = getInput()

    var minSize = 100L
    var secondsPassed = 0L
    while (true) {
        stars.move()
        secondsPassed++

        val newSize = stars.spreadSize()

        if (minSize > newSize) {
            println(secondsPassed)
            minSize = newSize
            stars.print()
        }
    }
}