package Day7;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.Arrays;
import java.util.List;

public class Main {
	public static void main(String[] args) {
		var input = readInput();
		var results = calculateSolutions(input);
		System.out.printf("[Part 1]: %d\n", results[0]);
		System.out.printf("[Part 2]: %d\n", results[1]);
	}
	
	private static List<Integer> readInput() {
		try (BufferedReader br = new BufferedReader(new FileReader("../inputs/day7.txt"))) {
			return Arrays.stream(br.readLine().split(","))
				.mapToInt(Integer::parseInt)
				.boxed()
				.toList();
		} catch (FileNotFoundException e) {
			System.out.println("Input file was not found in the inputs folder.");
		} catch (IOException e) {
			System.out.println("An IO error occurred during reading the input file.");
		}
		
		throw new RuntimeException("Could not read input.");
	}
	
	private static int[] calculateSolutions(List<Integer> data) {
		int[] costs = {Integer.MAX_VALUE, Integer.MAX_VALUE};
		int min = data.stream().min(Integer::compareTo).get();
		int max = data.stream().max(Integer::compareTo).get();
		
		for (int i = min; i < max; i++) {
			final var commonPosition = i;
			
			int sumFirstPart = data.stream().reduce(0, (total, element) -> total + Math.abs(commonPosition - element));
			if (sumFirstPart < costs[0])
				costs[0] = sumFirstPart;
			
			int sumSecondPart = data.stream().reduce(0, (total, element) -> {
				int diff = Math.abs(commonPosition - element);
				return total + ((diff * diff + diff) / 2);
			});
			if (sumSecondPart < costs[1])
				costs[1] = sumSecondPart;
		}
		
		return costs;
	}
}
