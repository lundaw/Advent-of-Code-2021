#include <iostream>
#include <vector>
#include <string>
#include <fstream>
#include <algorithm>

#pragma region Part 1
auto calculatePart1(const std::vector<std::pair<std::string, int>>& data) noexcept {
	int position = 0;
	int depth = 0;

	std::for_each(data.cbegin(), data.cend(), [&p = position, &d = depth](const auto& command) -> void {
		if (!command.first.compare("forward"))
			p += command.second;
		else if (!command.first.compare("down"))
			d += command.second;
		else if (!command.first.compare("up"))
			d -= command.second;
		});

	return position * depth;
}
#pragma endregion

#pragma region Part 2
auto calculatePart2(const std::vector<std::pair<std::string, int>>& data) noexcept {
	int position = 0;
	int depth = 0;
	int aim = 0;

	std::for_each(data.cbegin(), data.cend(), [&p = position, &d = depth, &a = aim](const auto& command) {
		if (!command.first.compare("forward")) {
			p += command.second;
			d += a * command.second;
		}
		else if (!command.first.compare("down"))
			a += command.second;
		else if (!command.first.compare("up"))
			a -= command.second;
		});

	return position * depth;
}
#pragma endregion

#pragma region Helpers
void readInput(const char* inputFile, std::vector<std::pair<std::string, int>>& input) {
	if (inputFile == nullptr) {
		throw std::invalid_argument("Input file path was nullpointer.");
	}

	std::ifstream inputStream{ inputFile };
	if (!inputStream) {
		throw std::runtime_error("Failed to open input file.");
	}

	std::string command;
	int units;

	while (inputStream >> command >> units) {
		input.emplace_back(std::make_pair(command, units));
	}

	inputStream.close();
}
#pragma endregion

int main() {
	auto inputData = std::vector<std::pair<std::string, int>>();
	readInput("../../inputs/day2.txt", inputData);
	std::cout << "[Part 1]: " << calculatePart1(inputData) << "\n"
		<< "[Part 2]: " << calculatePart2(inputData);
}