#include <iostream>
#include <fstream>
#include <map>
#include <sstream>
#include <numeric>

std::map<int, unsigned long long> readInput(const char* inputFile) {
	if (inputFile == nullptr) {
		throw std::invalid_argument("Input file path was nulltr.");
	}

	std::ifstream inputStream{ inputFile };
	if (!inputStream) {
		throw std::runtime_error("Failed to open input file.");
	}

	std::string input;
	inputStream >> input;
	inputStream.close();

	std::stringstream iss{ input };
	std::map<int, unsigned long long> lanternfish = std::map<int, unsigned long long>();

	while (iss.good()) {
		std::string num;
		std::getline(iss, num, ',');
		int numValue = std::stoi(num);
		if (lanternfish.count(numValue)) {
			lanternfish[numValue]++;
		}
		else {
			lanternfish[numValue] = 1;
		}
	}

	return lanternfish;
}

unsigned long long calculateForDays(const std::map<int, unsigned long long>& lanternfish, const int days = 80) {
	auto copy = std::map<int, unsigned long long>(lanternfish);

	for (auto day = 0; day < days; day++) {
		unsigned long long parents = copy[0];
		for (auto n = 1; n < 9; n++) {
			copy[n - 1] = copy[n];
		}

		copy[6] += parents;
		copy[8] = parents;
	}

	return std::accumulate(
		copy.cbegin(),
		copy.cend(),
		0ULL,
		[](const unsigned long long sum, const auto count) -> unsigned long long { return sum + count.second; }
	);
}

int main() {
	auto input = readInput("../../inputs/day6.txt");
	std::cout << "[Part 1]: " << calculateForDays(input) << "\n"
		<< "[Part 2]: " << calculateForDays(input, 256);
}