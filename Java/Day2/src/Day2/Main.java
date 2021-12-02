package Day2;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.AbstractMap.SimpleEntry;
import java.util.ArrayList;

public class Main {
	public static void main(String[] args) {
		var input = readInput();
		System.out.printf("[Part 1]: %d\n", calculalatePart1(input));
		System.out.printf("[Part 2]: %d\n", calculalatePart2(input));
	}
	
	private static ArrayList<SimpleEntry<String, Integer>> readInput() {
		var input = new ArrayList<SimpleEntry<String, Integer>>();
		
		try (BufferedReader br = new BufferedReader(new FileReader("../inputs/day2.txt"))) {
			String line;
			while ((line = br.readLine()) != null) {
				var values = line.split(" ");
				var entry = new SimpleEntry<>(values[0], Integer.parseInt(values[1]));
				input.add(entry);
			}
		} catch (FileNotFoundException e) {
			System.out.println("Input file was not found in the inputs folder.");
		} catch (IOException e) {
			System.out.println("An IO error occurred during reading the input file.");
		}
		
		return input;
	}
	
	private static int calculalatePart1(final ArrayList<SimpleEntry<String, Integer>> data) {
		int position = 0;
		int depth = 0;
		
		for (final var entry : data) {
			switch (entry.getKey()) {
				case "forward" -> position += entry.getValue();
				case "down" -> depth += entry.getValue();
				case "up" -> depth -= entry.getValue();
			}
		}
		
		return position * depth;
	}
	
	private static int calculalatePart2(final ArrayList<SimpleEntry<String, Integer>> data) {
		int position = 0;
		int depth = 0;
		int aim = 0;
		
		for (final var entry : data) {
			switch (entry.getKey()) {
				case "forward" -> {
					position += entry.getValue();
					depth += aim * entry.getValue();
				}
				case "down" -> aim += entry.getValue();
				case "up" -> aim -= entry.getValue();
			}
		}
		
		return position * depth;
	}
}
