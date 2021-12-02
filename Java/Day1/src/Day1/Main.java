package Day1;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class Main {
	public static void main(String[] args) {
		var input = readInput();
		System.out.printf("[Part 1]: %d\n", calculalatePart1(input));
		System.out.printf("[Part 2]: %d\n", calculalatePart2(input));
	}
	
	private static int calculalatePart1(final ArrayList<Integer> data) {
		int counter = 0;
		
		for (int i = 1; i < data.size(); i++) {
			if (data.get(i - 1) < data.get(i)) {
				counter++;
			}
		}
		
		return counter;
	}
	
	private static int calculalatePart2(final ArrayList<Integer> data) {
		int counter = 0;
		
		for (int i = 0; i < data.size() - 3; i++) {
			var firstWindow = data.get(i) + data.get(i + 1) + data.get(i + 2);
			var secondWindow = data.get(i + 1) + data.get(i + 2) + data.get(i + 3);
			
			if (firstWindow < secondWindow) {
				counter++;
			}
		}
		
		return counter;
	}
	
	private static ArrayList<Integer> readInput() {
		ArrayList<Integer> input = new ArrayList<>();
		
		try (BufferedReader br = new BufferedReader(new FileReader("../inputs/day1.txt"))) {
			String line;
			while ((line = br.readLine()) != null) {
				input.add(Integer.parseInt(line));
			}
		} catch (FileNotFoundException e) {
			System.out.println("Input file was not found in the inputs folder.");
		} catch (IOException e) {
			System.out.println("An IO error occurred during reading the input file.");
		}
		
		return input;
	}
}