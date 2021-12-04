package Day3;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.function.BiFunction;
import java.util.function.Function;

public class Main {
	public static void main(String[] args) {
		var input = readInput();
		System.out.printf("[Part 1]: %d\n", calculalatePart1(input));
		System.out.printf("[Part 2]: %d\n", calculalatePart2(input));
	}
	
	private static ArrayList<ArrayList<Digit>> readInput() {
		var input = new ArrayList<ArrayList<Digit>>();
		
		try (BufferedReader br = new BufferedReader(new FileReader("../inputs/day3.txt"))) {
			String line;
			while ((line = br.readLine()) != null) {
				var row = new ArrayList<Digit>(line.length());
				for (int i = 0; i < line.length(); i++) {
					row.add(new Digit(line.charAt(i), i));
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
	
	private static int calculalatePart1(final ArrayList<ArrayList<Digit>> data) {
		StringBuilder buffer = new StringBuilder(12);
		
		BiFunction<Character, Integer, Long> countDigit = (digit, position) -> data.stream()
			.filter(row -> row.get(position).bit() == digit)
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
	
	private static int calculalatePart2(final ArrayList<ArrayList<Digit>> data) {
		return -1;
	}
}
