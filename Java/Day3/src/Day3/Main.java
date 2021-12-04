package Day3;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.function.BiFunction;
import java.util.function.Function;

public class Main {
	public static void main(String[] args) {
		var input = readInput();
		System.out.printf("[Part 1]: %d\n", calculalatePart1(input));
		System.out.printf("[Part 2]: %d\n", calculalatePart2(input));
	}
	
	private static ArrayList<ArrayList<Character>> readInput() {
		var input = new ArrayList<ArrayList<Character>>();
		
		try (BufferedReader br = new BufferedReader(new FileReader("../inputs/day3.txt"))) {
			String line;
			while ((line = br.readLine()) != null) {
				var row = new ArrayList<Character>(line.length());
				for (int i = 0; i < line.length(); i++) {
					row.add(line.charAt(i));
				}
				
				input.add(row);
			}
		} catch (FileNotFoundException e) {
			System.out.println("Input file was not found in the inputs folder.");
		} catch (IOException e) {
			System.out.println("An IO error occurred during reading the input file.");
		}
		
		return input;
	}
	
	private static int calculalatePart1(final ArrayList<ArrayList<Character>> data) {
		StringBuilder buffer = new StringBuilder(12);
		
		BiFunction<Character, Integer, Long> countDigit = (digit, position) -> data.stream()
			.filter(row -> row.get(position) == digit)
			.count();
		
		for (int i = 0; i < 12; i++) {
			buffer.append(
				countDigit.apply('0', i) < countDigit.apply('1', i) ? '1' : '0'
			);
		}
		
		var mostCommon = Integer.parseInt(buffer.toString(), 2);
		var leastCommon = ~mostCommon & 0xFFF;
		
		return mostCommon * leastCommon;
	}
	
	private static int calculalatePart2(final ArrayList<ArrayList<Character>> data) {
		List<List<Character>> oxygen = new ArrayList<>(data);
		List<List<Character>> scrubber = new ArrayList<>(data);
		
		for (int i = 0; i < data.get(0).size(); i++) {
			final var idx = i;
			
			final var oxygenZeros = countDigit(oxygen, i, '0');
			final var oxygenOnes = oxygen.size() - oxygenZeros;
			oxygen = oxygen.stream()
				.filter(row -> row.get(idx) == (oxygenZeros <= oxygenOnes ? '1' : '0'))
				.toList();
			
			if (scrubber.size() > 1) {
				final var scrubberZeros = countDigit(scrubber, i, '0');
				final var scrubberOnes = scrubber.size() - scrubberZeros;
				scrubber = scrubber.stream()
					.filter(row -> row.get(idx) == (scrubberZeros <= scrubberOnes ? '0' : '1'))
					.toList();
			}
		}
		
		var oxygenRate = Integer.parseInt(
			oxygen.get(0).stream()
				.reduce("", (binaryString, digit) -> binaryString + digit, String::concat),
			2
		);
		var scrubberRate = Integer.parseInt(
			scrubber.get(0).stream()
				.reduce("", (binaryString, digit) -> binaryString + digit, String::concat),
			2
		);
		
		return oxygenRate * scrubberRate;
	}
	
	private static long countDigit(final List<List<Character>> data, final int position, final char bit) {
		return data.stream()
			.filter(row -> row.get(position) == bit)
			.count();
	}
}
