build: chronal_calibration.f08
	gfortran -o build chronal_calibration.f08

clean:
	rm build day1.mod || true

run: build
	./build

prettify: chronal_calibration.f08
	fprettify chronal_calibration.f08
