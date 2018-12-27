package main

import (
	"fmt"
	"math"
)

type empty struct{}
type semaphore chan empty

const gridSerialNumber = 9445 // this is the input
const gridSize = 300

var fuelGrid = [gridSize][gridSize]int{}
var combinedLevels = [gridSize][gridSize][gridSize]int{}

func calcFuelLevel(x int, y int) int {
	rackId := x + 10
	level := rackId * y
	level += gridSerialNumber
	level *= rackId
	level = getHundreds(level)
	level -= 5

	return level
}

func getHundreds(i int) int {
	if i < 100 {
		return 0
	}

	return i / 100 % 10
}

func getCombinedFuelLevel(x int, y int, size int) int {
	sum := 0

	for offX := 0; offX < size; offX++ {
		for offY := 0; offY < size; offY++ {
			sum += fuelGrid[x+offX-1][y+offY-1]
		}
	}

	return sum
}

func main() {
	// calc fuel levels for grid
	for x := 1; x <= gridSize; x++ {
		for y := 1; y <= gridSize; y++ {
			fuelGrid[x-1][y-1] = calcFuelLevel(x, y)
		}
	}

	// calculate fuel levels (one goroutine for each x,y)
	sem := make(semaphore, gridSize^2)
	for x := 1; x <= gridSize; x++ {
		for y := 1; y <= gridSize; y++ {

			go func(x int, y int) {
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

	threeX, threeY, threeLevel := -1, -1, math.MinInt32
	biggestX, biggestY, biggestSize, biggestLevel := -1, -1, -1, math.MinInt32
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

	fmt.Printf("Part I:  %d,%d \t\t= %d\n", threeX, threeY, threeLevel)
	fmt.Printf("Part II: %d,%d,%d \t= %d\n", biggestX, biggestY, biggestSize, biggestLevel)
}
