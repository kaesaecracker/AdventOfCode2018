package main

import (
	"fmt"
	"math"
)

const gridSerialNumber = 9445 // this is the input
const gridSize = 300

// fuel levels for each cell in grid
var fuelGrid = [gridSize][gridSize]int{}
// combined levels for [x][y][size]
var combinedLevels = [gridSize][gridSize][gridSize]int{}

// returns the fuel level of the cell
func calcFuelLevel(x int, y int) int {
	rackId := x + 10
	level := rackId * y
	level += gridSerialNumber
	level *= rackId
	level = getHundreds(level)
	level -= 5

	return level
}

// returns the hundreds place of the number (0 <= x <= 9) or 0 if there is none
func getHundreds(i int) int {
	if i < 100 {
		return 0
	}

	return i / 100 % 10
}

// Given the top left corner and the size of the square it returns the sum of the levels. This does *not* check the indices accessed, so it has to be called carefully.
func getCombinedFuelLevel(x int, y int, size int) int {
	sum := 0

	for offX := 0; offX < size; offX++ {
		for offY := 0; offY < size; offY++ {
			sum += fuelGrid[x+offX-1][y+offY-1]
		}
	}

	return sum
}

// calculate the fuel levels for every cell in the grid
func populateFuelGrid() {
	for x := 1; x <= gridSize; x++ {
		for y := 1; y <= gridSize; y++ {
			fuelGrid[x-1][y-1] = calcFuelLevel(x, y)
		}
	}
}

// calculate fuel levels (one goroutine for each x,y)
func populateCombinedLevels() {
	// semaphore pattern
	type empty struct{}
	type semaphore chan empty
	sem := make(semaphore, gridSize^2)

	// for each cell
	for x := 1; x <= gridSize; x++ {
		for y := 1; y <= gridSize; y++ {

			// asynchronously call anonymous func that calculates the levels for possible sizes
			go func(x int, y int) {
				//             make sure (x+size | y+size) is in array
				for size := 1; (x+size < gridSize) && (y+size < gridSize); size++ {
					combinedLevels[x-1][y-1][size-1] = getCombinedFuelLevel(x, y, size)
				}

				sem <- empty{}
			}(x, y)

		}
	}

	// wait for the goroutines to finish
	for i := 0; i < gridSize^2; i++ {
		<-sem
	}
}

func main() {
	populateFuelGrid()
	populateCombinedLevels()

	// vars to remember solution values
	threeX, threeY, threeLevel := -1, -1, math.MinInt32
	biggestX, biggestY, biggestSize, biggestLevel := -1, -1, -1, math.MinInt32

	// find solution values in calculated levels
	for x := 1; x <= gridSize; x++ {
		for y := 1; y <= gridSize; y++ {
			for size := 1; size <= gridSize; size++ {
				level := combinedLevels[x-1][y-1][size-1]

				if size == 3 && threeLevel < level {
					threeX, threeY, threeLevel = x, y, level
				}

				if biggestLevel < level {
					biggestX, biggestY, biggestSize, biggestLevel = x, y, size, level
				}

			}
		}
	}

	// output (duh)
	fmt.Printf("Part I:  %d,%d \t\t= %d\n", threeX, threeY, threeLevel)
	fmt.Printf("Part II: %d,%d,%d \t= %d\n", biggestX, biggestY, biggestSize, biggestLevel)
}
