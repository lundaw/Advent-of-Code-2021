#include <iostream>
#include <fstream>
#include <vector>

#pragma region Helper
auto readInput(const char* inputFile) {
	if (inputFile == nullptr) {
		throw std::invalid_argument("Input file path was nullpointer.");
	}

	std::ifstream inputStream{ inputFile };
	if (!inputStream) {
		throw std::runtime_error("Failed to open input file.");
	}

	auto input = std::vector<unsigned int>();
	unsigned int value;

	while (inputStream >> value) {
		input.push_back(value);
	}

	inputStream.close();

	return input;
}
#pragma endregion

#pragma region Part 1
auto calculatePart1(const std::vector<unsigned int>& data) {
	auto counter = 0ul;

	for (auto i = 1ul; i < data.size(); i++) {
		if (data[i - 1] < data[i]) {
			counter++;
		}
	}

	return counter;
}
#pragma endregion

#pragma region Part 2
auto calculatePart2(const std::vector<unsigned int>& data) {
	auto counter = 0ul;

	for (auto i = 0ul; i < data.size() - 3; i++) {
		auto firstWindow = data[i] + data[i + 1] + data[i + 2];
		auto secondWindow = data[i + 1] + data[i + 2] + data[i + 3];

		if (firstWindow < secondWindow) {
			counter++;
		}
	}

	return counter;
}
#pragma endregion

int main() {
	auto input = readInput("../../inputs/day1.txt");
	std::cout << "[Part 1]: " << calculatePart1(input) << "\n"
		<< "[Part 2]: " << calculatePart2(input);

	return 0;
}