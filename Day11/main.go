package main

import (
	"fmt"
	"math"
)

const gridSerialNumber = 9445 // this is the input
const gridSize = 300

var fuelGrid = [gridSize][gridSize]int{}

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

func getCombinedFuelLevel(x int, y int) int {
	sum := 0

	for offX := 0; offX < 3; offX++ {
		for offY := 0; offY < 3; offY++ {
			sum += fuelGrid[x+offX-1][y+offY-1]
		}
	}

	return sum
}

func main() {
	for x := 1; x <= gridSize; x++ {
		for y := 1; y <= gridSize; y++ {
			fuelGrid[x-1][y-1] = calcFuelLevel(x, y)
		}
	}

	biggestX := -1
	biggestY := -1
	biggestLevel := math.MinInt32
	for x := 1; x <= gridSize-2; x++ {
		for y := 1; y <= gridSize-2; y++ {
			combLevel := getCombinedFuelLevel(x, y)
			if combLevel > biggestLevel {
				biggestLevel = combLevel
				biggestX = x
				biggestY = y
			}
		}
	}

	fmt.Printf("Solution: %d,%d = %d\n", biggestX, biggestY, biggestLevel)
}
